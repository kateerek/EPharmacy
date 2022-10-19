using System.Collections.Generic;
using System.Threading.Tasks;
using EPharmacy.ServerApp.Models.Producer.Common;
using EPharmacy.ServerApp.Models.Producer.ProducerCreation;

namespace EPharmacy.ServerApp.Services.Producer
{
    public interface IProducerService
    {
        Task<bool> CreateProducer(ProducerCreationRequestModel productCreationRequest);
        Task<Data.Entities.Products.Producer> GetProducerById(int producerId);
        Task<IList<ProducerModel>> GetAllProducers();
        Task<bool> EditProducer(ProducerModel model);
        Task<bool> RemoveProducer(int id);
    }
}