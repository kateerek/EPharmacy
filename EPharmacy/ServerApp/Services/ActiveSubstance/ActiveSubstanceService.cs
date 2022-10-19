using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EPharmacy.Data;
using EPharmacy.ServerApp.Models.ActiveSubstance.Create;
using EPharmacy.ServerApp.Models.ActiveSubstance.GetAll;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.ServerApp.Services.ActiveSubstance
{
    using ActiveSubstance = Data.Entities.ActiveSubstances.ActiveSubstance;

    public class ActiveSubstanceService : IActiveSubstanceService
    {        
        private readonly IMapper _mapper;
        private readonly EPharmacyContext _context;

        public ActiveSubstanceService (EPharmacyContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<bool> CreateActiveSubstance(ActiveSubstanceCreationRequest activeSubstanceCreationRequest)
        {
            try
            {
                var newActiveSubstance = _mapper.Map
                    <ActiveSubstanceCreationRequest, ActiveSubstance>(activeSubstanceCreationRequest);

                await _context.ActiveSubstances.AddAsync(newActiveSubstance);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<bool> RemoveActiveSubstance(int activeSubstanceId)
        {
            try
            {
                var activeSubstanceToRemove =
                    await _context.ActiveSubstances.FirstOrDefaultAsync(a => a.Id == activeSubstanceId);
                if (activeSubstanceToRemove == null)
                    return true;

                _context.ActiveSubstances.Remove(activeSubstanceToRemove);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public async Task<List<ActiveSubstanceResponse>> GetAllActiveSubstances()
        {
            try
            {
                var activeSubstances = await _context.ActiveSubstances.ToListAsync();
                return _mapper.Map<List<ActiveSubstance>, List<ActiveSubstanceResponse>>
                    (activeSubstances);
            }
            catch (Exception exception)
            {
                return null;
            }
        }
    }
}
