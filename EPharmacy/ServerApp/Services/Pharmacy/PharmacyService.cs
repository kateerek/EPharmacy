using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data;
using EPharmacy.Data.Entities.SalesOrders;
using EPharmacy.ServerApp.Models.Pharmacy.AddLocation;
using EPharmacy.ServerApp.Models.Pharmacy.Common;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.ServerApp.Services.Pharmacy
{
    public class PharmacyService : IPharmacyService
    {
        private readonly EPharmacyContext _context;
        private readonly IMapper _mapper;

        public PharmacyService(EPharmacyContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<PharmacyLocationModel>> GetAllPharmacies()
        {
            var entities = await _context.PharmacyLocations.ToListAsync();
            return _mapper.Map<List<PharmacyLocationModel>>(entities);
        }

        public async Task AddPharmacyLocation(PharmacyLocationRequest pharmacyLocationRequest)
        {
            var entity = _mapper.Map<PharmacyLocation>(pharmacyLocationRequest);
            await _context.PharmacyLocations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> EditPharmacyLocation(PharmacyLocationModel pharmacyLocationModel)
        {
            var entity = await _context.PharmacyLocations.FirstOrDefaultAsync(x => x.Id == pharmacyLocationModel.Id);
            _mapper.Map(pharmacyLocationModel, entity);
            _context.PharmacyLocations.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemovePharmacyLocation(int id)
        {
            var entity =await _context.PharmacyLocations.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return false;
            _context.PharmacyLocations.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}