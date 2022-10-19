using System.Collections.Generic;
using System.Threading.Tasks;
using EPharmacy.Data.Entities.Attributes;
using EPharmacy.Data.Entities.Products;
using EPharmacy.ServerApp.Models.Attributes.Create;
using EPharmacy.ServerApp.Models.Attributes.Edit;
using EPharmacy.ServerApp.Models.Attributes.GetAll;

namespace EPharmacy.ServerApp.Services.Attributes
{
    public interface IAttributeService
    {
        Task<bool> CreateAttribute(AttributeCreationRequest attributeCreationRequest);
        Task<bool> EditAttribute(AttributeEditionRequest attributeEditionRequest);
        Task<Attribute> GetAttributeById(int attributeId);
        Task<IList<AttributeResponseModel>> GetAllAttributes();
        Task<IList<AttributeResponseModel>> GetDetailsForAttributes(IEnumerable<int> attributeIds);
        Task<IList<CategoryModel>> GetCategories();
        Task<bool> RemoveAttribute(int id);
        void UpdateMainCattegory(Product product);
    }
}