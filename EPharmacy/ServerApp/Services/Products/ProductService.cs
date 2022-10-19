using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data;
using EPharmacy.Data.Entities.Products;
using EPharmacy.ServerApp.Models.Product.ActiveSubstances;
using EPharmacy.ServerApp.Models.Product.Attributes;
using EPharmacy.ServerApp.Models.Product.Create;
using EPharmacy.ServerApp.Models.Product.Details;
using EPharmacy.ServerApp.Models.Product.Edit;
using EPharmacy.ServerApp.Models.Product.ProductsDetailsList;
using EPharmacy.ServerApp.Models.Product.ProductType.Create;
using EPharmacy.ServerApp.Models.Product.ProductType.GetAll;
using EPharmacy.ServerApp.Models.Product.Tags;
using EPharmacy.ServerApp.Services.Attributes;
using EPharmacy.ServerApp.Services.Discount;
using EPharmacy.ServerApp.Services.Prescription;
using EPharmacy.ServerApp.Services.Producer;
using Microsoft.EntityFrameworkCore;
using Attribute = EPharmacy.Data.Entities.Attributes.Attribute;


namespace EPharmacy.ServerApp.Services.Products
{
    using ActiveSubstance = EPharmacy.Data.Entities.ActiveSubstances.ActiveSubstance;

    public class ProductService : IProductService
    {
        private readonly IAttributeService _attributeService;
        private readonly EPharmacyContext _context;
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        private readonly IPrescriptionService _prescriptionService;
        private readonly IProducerService _producerService;

        public ProductService(EPharmacyContext context, IMapper mapper, IProducerService producerService,
            IAttributeService attributeService, IPrescriptionService prescriptionService,
            IDiscountService discountService)
        {
            _context = context;
            _mapper = mapper;
            _producerService = producerService;
            _attributeService = attributeService;
            _prescriptionService = prescriptionService;
            _discountService = discountService;
        }

        public async Task<bool> CreateProduct(ProductCreationRequest productCreationRequest)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var newProduct = _mapper.Map<ProductCreationRequest, Product>(productCreationRequest);

                    var foundProductType = await GetProductTypeById(productCreationRequest.ProductTypeId);
                    if (foundProductType == null)
                        return false;
                    newProduct.ProductType = foundProductType;

                    var foundProducer = await _producerService.GetProducerById(productCreationRequest.ProducerId);
                    if (foundProducer == null)
                        return false;
                    newProduct.Producer = foundProducer;

                    //Current solution for prescriptions. If prescription is equal to null then product is not being sold with prescription.
                    //PrescriptionInformationId from request model can be sent as '0' or 'null' if product has no prescription.
                    newProduct.PrescriptionInformation = productCreationRequest.PrescriptionInformationId == null
                        ? null
                        : await _prescriptionService.GetPrescriptionInformationById
                            ((int) productCreationRequest.PrescriptionInformationId);

                    await _context.Products.AddAsync(newProduct);
                    if (!(await _context.SaveChangesAsync() > 0))
                        return false;

                    //Binding, activating, deactivating attributes. 
                    foreach (var productAttributeInformationModel in productCreationRequest.Attributes)
                    {
                        var processedAttribute = await _attributeService.GetAttributeById(
                            productAttributeInformationModel.AttributeId);

                        var attributeChangesSaveResult = await BindAttributeToProduct(newProduct, processedAttribute);
                        if (!attributeChangesSaveResult)
                            return false;

                        if (productAttributeInformationModel.IsActive)
                            attributeChangesSaveResult =
                                await SetAttributeActiveOnProduct(newProduct, processedAttribute);
                        else
                            attributeChangesSaveResult =
                                await SetAttributeInactiveOnProduct(newProduct, processedAttribute);

                        if (!attributeChangesSaveResult)
                            return false;
                    }

                    _attributeService.UpdateMainCattegory(newProduct);

                    if (productCreationRequest.PrescriptionDiscounts != null)
                    {
                        await _discountService.ModifyPrescriptionDiscountsForProduct(newProduct, productCreationRequest.PrescriptionDiscounts);
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> EditProduct(ProductEditionRequest productEditionRequest)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var productToEdit = await GetProductById(productEditionRequest.Id);
                    if (productToEdit == null)
                        return false;

                    _mapper.Map(productEditionRequest, productToEdit);

                    if (productEditionRequest.ProductTypeId != productToEdit.ProductType.Id)
                    {
                        var foundProductType = await GetProductTypeById(productEditionRequest.ProductTypeId);
                        if (foundProductType == null)
                            return false;
                        productToEdit.ProductType = foundProductType;
                    }

                    if (productEditionRequest.ProducerId != productToEdit.Producer.Id)
                    {
                        var foundProducer = await _producerService.GetProducerById(productEditionRequest.ProducerId);
                        if (foundProducer == null)
                            return false;
                        productToEdit.Producer = foundProducer;
                    }

                    //Current solution for prescriptions. If prescription is equal to null then product is not being sold with prescription. 
                    //PrescriptionInformationId from request model can be sent as '0' or 'null' if product has no prescription.
                    if (productToEdit.PrescriptionInformation?.Id != productEditionRequest.PrescriptionInformationId)
                        productToEdit.PrescriptionInformation =
                            productEditionRequest.PrescriptionInformationId == null ||
                            productEditionRequest.PrescriptionInformationId == 0
                                ? null
                                : await _prescriptionService.GetPrescriptionInformationById
                                    ((int) productEditionRequest.PrescriptionInformationId);

                    //Active substances
                    var productActiveSubstancesToRemove = productToEdit.ProductActiveSubstances
                        .Where(pas => productEditionRequest.ProductActiveSubstances
                                .All(a => a.ActiveSubstanceId != pas.ActiveSubstanceId)).ToList();

                    //Removing active substances
                    foreach (var productActiveSubstanceToRemove in productActiveSubstancesToRemove)
                    {
                        productToEdit.ProductActiveSubstances.Remove(productActiveSubstanceToRemove);
                    }

                    //Adding new active substances, editing existing
                    foreach (var processedProductActiveSubstance in productEditionRequest.ProductActiveSubstances)
                    {
                        var activeSubstanceFromProduct = productToEdit.ProductActiveSubstances
                                                                      .FirstOrDefault(pas =>
                                                                            pas.ActiveSubstanceId == processedProductActiveSubstance.ActiveSubstanceId);
                        if (activeSubstanceFromProduct == null)
                        {
                            productToEdit.ProductActiveSubstances.Add
                            (new ProductActiveSubstance(productToEdit.Id,
                                processedProductActiveSubstance.ActiveSubstanceId,
                                processedProductActiveSubstance.Amount));
                        }
                        else
                        {
                            activeSubstanceFromProduct.Amount = processedProductActiveSubstance.Amount;
                        }
                    }

                    //Initial product save
                    _context.Products.Update(productToEdit);
                    await _context.SaveChangesAsync();
                
                    var productToEditCurrentAttributes = await GetProductAttributes(productToEdit.Id);
                    var productAttributesToRemove = productToEditCurrentAttributes
                        .Where(pca =>
                            productEditionRequest.Attributes.All(a => a.AttributeId != pca.Id)
                        );

                    //Removing attributes.
                    foreach (var attributeToRemove in productAttributesToRemove)
                    {
                        var attributeChangesSaveResult =
                            await RemoveAttributeFromProduct(productToEdit, attributeToRemove);
                        if (!attributeChangesSaveResult)
                            return false;
                    }

                    //Binding, activating, deactivating attributes. 
                    foreach (var productAttributeInformationModel in productEditionRequest.Attributes)
                    {
                        var processedAttribute = await _attributeService.GetAttributeById(
                            productAttributeInformationModel.AttributeId);

                        var attributeChangesSaveResult =
                            await BindAttributeToProduct(productToEdit, processedAttribute);
                        if (!attributeChangesSaveResult)
                            return false;

                        if (productAttributeInformationModel.IsActive)
                            attributeChangesSaveResult =
                                await SetAttributeActiveOnProduct(productToEdit, processedAttribute);
                        else
                            attributeChangesSaveResult =
                                await SetAttributeInactiveOnProduct(productToEdit, processedAttribute);

                        if (!attributeChangesSaveResult)
                            return false;
                    }

                    _attributeService.UpdateMainCattegory(productToEdit);

                    if (productEditionRequest.PrescriptionDiscounts != null)
                    {
                        await _discountService.ModifyPrescriptionDiscountsForProduct(productToEdit, productEditionRequest.PrescriptionDiscounts);
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<bool> RemoveProduct(int productId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var productToRemove = await GetProductById(productId);
                    if (!await RemoveAllAttributesFromProduct(productToRemove))
                        return false;

                    await _discountService.RemovePrescriptionDiscountsForProduct(productToRemove);

                    _context.Products.Remove(productToRemove);
                    if (!(await _context.SaveChangesAsync() > 0))
                        return false;

                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public async Task<IList<ProductDetailsListModel>> GetAllProductsByAttributes(ProductDetailsListRequest model)
        {
            var productLists = new List<IEnumerable<Product>>();
            foreach (var attributeId in model.AttributeIds)
            {
                var productsWithAttribute = await _context.Products.Where(prod =>
                    prod.AttributesValues.Any(x => x.AttributeValueId == attributeId)).ToListAsync();
                if (productsWithAttribute.Any())
                    productLists.Add(productsWithAttribute.OrderBy(x => x.Id));
            }

            if (!productLists.Any()) return null;
            var products = productLists.Aggregate((x,y) => x.Intersect(y));
            return await TransformProductsToProductDetailsList(products);
        }

        public async Task<IList<ProductDetailsListModel>> GetDetailsForProducts(IEnumerable<int> productIds)
        {
            if (productIds == null)
            {
                return new List<ProductDetailsListModel>();
            }
            var products = await _context.Products.Join(productIds, x => x.Id, x => x, (x, y) => x).ToListAsync();
            return await TransformProductsToProductDetailsList(products);
        }

        #region Favourite products. 
        public async Task<bool> ChangeIsFavouriteStatusForSingleProduct(int productId, string userId)
        {
            var entity =
                await _context.FavouriteProducts.FirstOrDefaultAsync(x =>
                    x.ProductId == productId && x.ApplicationUserId == userId);
            if (entity != null)
            {
                _context.FavouriteProducts.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }

            _context.FavouriteProducts.Add(new FavouriteProduct
            {
                ApplicationUserId = userId,
                ProductId = productId
            });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<ProductDetailsListModel>> FillFavouriteStatusForProducts(IList<ProductDetailsListModel> products,
            string userId)
        {
            var userFavouriteProducts = await _context.FavouriteProducts.Where(x => x.ApplicationUserId == userId).ToListAsync();            
            foreach (var product in products)
            {
                product.IsFavourite = userFavouriteProducts.Any(x => x.ProductId == product.Id);

            }
            return products;
        }

        public async Task<ProductDetailsModel> FillFavouriteStatusForSingleProduct(ProductDetailsModel result, string userId)
        {
            result.IsFavourite = await  _context.FavouriteProducts.AnyAsync(x => x.ApplicationUserId == userId && x.ProductId == result.Id);
            return result;
        }
        #endregion

        public async Task<ProductDetailsModel> GetProductDetailsModel(int productId)
        {
            var product = await GetProductById(productId);
            if (product == null) return null;
            var productDetailsModel = _mapper.Map<ProductDetailsModel>(product);
            productDetailsModel.ProductDiscounts = await _discountService.GetDiscountsForProduct(product);
            return productDetailsModel;

        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<bool> IfProductWithIdExists(int productId)
        {
            return await _context.Products.AnyAsync(p => p.Id == productId);
        }

        private async Task<IList<ProductDetailsListModel>> TransformProductsToProductDetailsList(
            IEnumerable<Product> products)
        {
            var productsToReturn = new List<ProductDetailsListModel>();

            foreach (var product in products)
            {
                var productDetailsListModel = _mapper.Map<ProductDetailsListModel>(product);
                productDetailsListModel.ProductDiscounts = await _discountService.GetDiscountsForProduct(product);
                productsToReturn.Add(productDetailsListModel);
            }

            return productsToReturn;
        }

        #region Attributes

        public async Task<IEnumerable<AttributeTagModel>> GetProductTags(int productId)
        {
            var productAttributesValues = (await GetAllProductProductAttributesValues(productId))
                .Where(pav => pav.IsActive);
            return _mapper.Map<List<AttributeTagModel>>(productAttributesValues);
        }

        public async Task<IEnumerable<AttributeModel>> GetProductAttributesModels(int productId)
        {
            var product = await _context.Products.Where(prod => prod.Id == productId).FirstOrDefaultAsync();
            return product == null ? null : _mapper.Map<List<AttributeModel>>(product.AttributesValues);
        }

        public async Task<IList<ProductTypeModel>> GetAllProductTypes()
        {
            var productTypes = await _context.ProductTypes.ToListAsync();
            return _mapper.Map<List<ProductTypeModel>>(productTypes);
        }

        public async Task<IEnumerable<Attribute>> GetProductAttributes(int productId)
        {
            var productAttributes = _context.Attributes.Where
            (a => _context.ProductAttributesValues
                .Any(pav => pav.ProductId == productId
                            && pav.AttributeValue.Attribute.Id == a.Id
                )
            ).AsQueryable();

            return await productAttributes.ToListAsync();
        }

        private async Task<IEnumerable<ProductAttributeValue>> GetAllProductProductAttributesValues(int productId)
        {
            var productAttributesValues = _context.ProductAttributesValues.Where(pav => pav.ProductId == productId)
                .AsQueryable();
            return await productAttributesValues.ToListAsync();
        }

        private async Task<bool> BindAttributeToProduct(Product product, Attribute attribute)
        {
            if (await IsAttributeBoundToProduct(product, attribute))
                return true;

            await _context.ProductAttributesValues.AddAsync(
                new ProductAttributeValue(product, attribute.AttributeValues.First()));
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> UnbindAttributeFromProduct(Product product, Attribute attribute)
        {
            if (!await IsAttributeBoundToProduct(product, attribute))
                return true;

            var productAttributeValueToRemove = _context.ProductAttributesValues.First(pav =>
                pav.Product.Id == product.Id && pav.AttributeValue.Attribute.Id == attribute.Id);
            _context.ProductAttributesValues.Remove(productAttributeValueToRemove);

            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> SetAttributeActiveOnProduct(Product product, Attribute attribute)
        {
            if (await IsAttributeActiveOnProduct(product, attribute))
                return true;
            if (!await IsAttributeBoundToProduct(product, attribute))
                return false;
            var productAttributesValueToChange = await _context.ProductAttributesValues.FirstAsync(pav =>
                pav.Product.Id == product.Id
                && pav.AttributeValue.Attribute.Id == attribute.Id);

            productAttributesValueToChange.IsActive = true;
            _context.ProductAttributesValues.Update(productAttributesValueToChange);

            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> SetAttributeInactiveOnProduct(Product product, Attribute attribute)
        {
            if (!await IsAttributeActiveOnProduct(product, attribute))
                return true;
            if (!await IsAttributeBoundToProduct(product, attribute))
                return false;
            var productAttributesValueToChange = await _context.ProductAttributesValues.FirstAsync(pav =>
                pav.Product.Id == product.Id
                && pav.AttributeValue.Attribute.Id == attribute.Id);

            productAttributesValueToChange.IsActive = false;
            _context.ProductAttributesValues.Update(productAttributesValueToChange);

            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> RemoveAttributeFromProduct(Product product, Attribute attribute)
        {
            var productAttributeValueToRemove = await _context.ProductAttributesValues
                .FirstOrDefaultAsync(pav => pav.ProductId == product.Id
                                            && pav.AttributeValue.Attribute.Id == attribute.Id);
            if (productAttributeValueToRemove == null)
                return true;
            _context.ProductAttributesValues.Remove(productAttributeValueToRemove);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> RemoveAllAttributesFromProduct(Product product)
        {
            var attributesToRemove = await GetProductAttributes(product.Id);
            foreach (var attributeToRemove in attributesToRemove)
            {
                var removalResult = await RemoveAttributeFromProduct(product, attributeToRemove);
                if (!removalResult)
                    return false;
            }

            return true;
        }

        private async Task<bool> IsAttributeBoundToProduct(Product product, Attribute attribute)
        {
            return await _context.ProductAttributesValues.AnyAsync(
                pav => pav.ProductId == product.Id && pav.AttributeValue.Attribute.Id == attribute.Id);
        }

        private async Task<bool> IsAttributeActiveOnProduct(Product product, Attribute attribute)
        {
            return await _context.ProductAttributesValues.AnyAsync(
                pav => pav.Product.Id == product.Id && pav.AttributeValue.Attribute.Id == attribute.Id
                                                    && pav.IsActive);
        }

        #endregion

        #region ProductType methods

        public async Task<bool> CreateProductType(ProductTypeCreationRequest productTypeCreationRequestModel)
        {
            var newProductType = _mapper.Map<ProductTypeCreationRequest, ProductType>(productTypeCreationRequestModel);

            await _context.ProductTypes.AddAsync(newProductType);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<ProductType> GetProductTypeById(int productTypeId)
        {
            return await _context.ProductTypes.FirstOrDefaultAsync(pt => pt.Id == productTypeId);
        }

        #endregion

        #region Active substances, substitutes
        public async Task<List<ProductActiveSubstanceResponse>> GetProductActiveSubstances(int id)
        {
                    var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
                return _mapper.Map<List<ProductActiveSubstance>,
                    List<ProductActiveSubstanceResponse>>(product?.ProductActiveSubstances);
        }

        public async Task<IList<ProductDetailsListModel>> GetProductSubstitutes(int productId)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            var substitutesIds = await _context.Products.Where(p => 
                (
                    p.ProductActiveSubstances.Any(pas => product.ProductActiveSubstances.Any(ppas =>
                    ppas.ActiveSubstanceId == pas.ActiveSubstanceId))
                ) && 
                (
                    p.Id != productId)
                )
                .Select(p => p.Id).ToListAsync();
            return await this.GetDetailsForProducts(substitutesIds);
        }
        #endregion
    }
}