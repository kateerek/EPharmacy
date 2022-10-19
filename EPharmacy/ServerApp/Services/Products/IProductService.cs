using System.Collections.Generic;
using System.Threading.Tasks;
using EPharmacy.Data.Entities.Attributes;
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

namespace EPharmacy.ServerApp.Services.Products
{
    public interface IProductService
    {
        Task<bool> CreateProduct(ProductCreationRequest productCreationRequest);
        Task<bool> EditProduct(ProductEditionRequest productEditionRequest);
        Task<Product> GetProductById(int productId);
        Task<IEnumerable<Attribute>> GetProductAttributes(int productId);
        Task<bool> RemoveProduct(int productId);
        Task<ProductDetailsModel> GetProductDetailsModel(int productId);
        Task<bool> IfProductWithIdExists(int productId);
        Task<bool> CreateProductType(ProductTypeCreationRequest productTypeCreationRequestModel);
        Task<ProductType> GetProductTypeById(int productTypeId);
        Task<IList<ProductDetailsListModel>> GetAllProductsByAttributes(ProductDetailsListRequest model);
        Task<IEnumerable<AttributeTagModel>> GetProductTags(int productId);
        Task<IEnumerable<AttributeModel>> GetProductAttributesModels(int productId);
        Task<IList<ProductTypeModel>> GetAllProductTypes();
        Task<IList<ProductDetailsListModel>> GetDetailsForProducts(IEnumerable<int> productIds);
        Task<bool> ChangeIsFavouriteStatusForSingleProduct(int productId, string userId);
        Task<IList<ProductDetailsListModel>> FillFavouriteStatusForProducts(IList<ProductDetailsListModel> products,
            string userId);
        Task<ProductDetailsModel> FillFavouriteStatusForSingleProduct(ProductDetailsModel result, string userId);
        Task<List<ProductActiveSubstanceResponse>> GetProductActiveSubstances(int id);
        Task<IList<ProductDetailsListModel>> GetProductSubstitutes(int productId);
    }
}
