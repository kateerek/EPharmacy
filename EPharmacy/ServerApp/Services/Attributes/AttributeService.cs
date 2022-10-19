using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using EPharmacy.Data;
using EPharmacy.Data.Entities.Attributes;
using EPharmacy.ServerApp.Models.Attributes.Create;
using EPharmacy.ServerApp.Models.Attributes.Edit;
using EPharmacy.ServerApp.Models.Attributes.GetAll;
using EPharmacy.ServerApp.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using EPharmacy.Data.Entities.Products;

namespace EPharmacy.ServerApp.Services.Attributes
{
    public class AttributeService : IAttributeService
    {
        private static int DefaultAttributeValueId { get; } = 8;
        private static IList<int> DefaultCategoriesIds { get; }
            = new List<int> {
                1, 2, 3, 4, 5, 6, 7, DefaultAttributeValueId
            };  
        private readonly IMapper _mapper;
        private readonly EPharmacyContext _context;

        public AttributeService(EPharmacyContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<bool> CreateAttribute(AttributeCreationRequest attributeCreationRequest)
        {
            var newAttribute = _mapper.Map<AttributeCreationRequest, Attribute>(attributeCreationRequest);

            await _context.Attributes.AddAsync(newAttribute);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> EditAttribute(AttributeEditionRequest attributeEditionRequest)
        {
            var attributeToEdit = await this.GetAttributeById(attributeEditionRequest.AttributeToEditId);
            this._mapper.Map(attributeEditionRequest, attributeToEdit);
            _context.Attributes.Update(attributeToEdit);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Attribute> GetAttributeById(int attributeId)
        {
            return await _context.Attributes.FirstOrDefaultAsync(a => a.Id == attributeId);
        }

        public async Task<IList<AttributeResponseModel>> GetAllAttributes()
        {
            return _mapper.Map<List<AttributeResponseModel>>(await _context.Attributes.ToListAsync());
        }

        public async Task<IList<AttributeResponseModel>> GetDetailsForAttributes(IEnumerable<int> attributeIds)
        {
            if (attributeIds == null)
            {
                return new List<AttributeResponseModel>();
            }
            var attributeIdsList = new List<int>(attributeIds);
            var attributes = await _context.Attributes.Where(attr => attributeIdsList.Contains(attr.Id)).ToListAsync();
            return _mapper.Map<List<AttributeResponseModel>>(attributes);
        }

        public void UpdateMainCattegory(Product product)
        {
            var productAttributes = product.AttributesValues.Select(x => x.AttributeValue.Attribute.Id).Distinct().ToList();
            var diffLenght = DefaultCategoriesIds.Except(productAttributes).Count();
            if (diffLenght == DefaultCategoriesIds.Count)
            {
                product.AttributesValues.Add(new ProductAttributeValue()
                {
                    AttributeValueId = DefaultAttributeValueId,
                    ProductId = product.Id,
                    IsActive = true
                });
            }
        }

        public async Task<bool> RemoveAttribute(int id)
        {
            if (DefaultCategoriesIds.Contains(id)) return false;
            var entity = await _context.Attributes.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;
            _context.Attributes.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IList<CategoryModel>> GetCategories()
        {
            var categoriesTasks = await _context.Attributes
                                    .Join(DefaultCategoriesIds, x => x.Id, x => x, (x, y) => x)
                                    .Select(x => CreateCategoryModelForAttribute(x)).ToListAsync();
            return await Task.WhenAll(categoriesTasks);
        }

        private async Task<CategoryModel> CreateCategoryModelForAttribute(Attribute attribute)
        {
            return new CategoryModel
            {
                Attribute = _mapper.Map<AttributeResponseModel>(attribute),
                SubAttributes = await _mapper.ProjectTo<AttributeResponseModel>(GetAssociatedAttributes(attribute)).ToListAsync()
            };
        }

        private IQueryable<Attribute> GetAssociatedAttributes(Attribute attribute)
        {
            return _context.Products
                    .Where(x => x.AttributesValues.Any(y => y.AttributeValueId == attribute.Id))
                    .SelectMany(x => x.AttributesValues.Select(y => y.AttributeValue.Attribute))
                    .Where(x => x != attribute && !DefaultCategoriesIds.Contains(x.Id))
                    .Distinct();
        }
    }
}
