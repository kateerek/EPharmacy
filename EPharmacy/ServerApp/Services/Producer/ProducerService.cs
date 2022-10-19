using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data;
using EPharmacy.ServerApp.Models.Producer.Common;
using EPharmacy.ServerApp.Models.Producer.ProducerCreation;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.ServerApp.Services.Producer
{
    public class ProducerService : IProducerService
    {
        private readonly IMapper _mapper;
        private readonly EPharmacyContext _context;

        public ProducerService(EPharmacyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateProducer(ProducerCreationRequestModel productCreationRequest)
        {
            var newProducer = _mapper.Map<ProducerCreationRequestModel, Data.Entities.Products.Producer>(productCreationRequest);

            await _context.Producers.AddAsync(newProducer);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Data.Entities.Products.Producer> GetProducerById(int producerId)
        {
            return await _context.Producers.FirstOrDefaultAsync(p => p.Id == producerId);
        }

        public async Task<IList<ProducerModel>> GetAllProducers()
        {
            return _mapper.Map<List<ProducerModel>>(await _context.Producers.ToListAsync());
        }

        public async Task<bool> EditProducer(ProducerModel model)
        {
            var entity  = await _context.Producers.FirstOrDefaultAsync(x => x.Id == model.Id);
            _mapper.Map(model, entity);
            _context.Producers.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveProducer(int id)
        {
            var entity = await _context.Producers.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;
            _context.Producers.Remove(entity);
            return await _context.SaveChangesAsync() > 0;

        }
    }
}
