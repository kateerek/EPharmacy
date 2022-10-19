using System.Collections.Generic;
using System.Threading.Tasks;
using EPharmacy.ServerApp.Models.ActiveSubstance.Create;
using EPharmacy.ServerApp.Models.ActiveSubstance.GetAll;

namespace EPharmacy.ServerApp.Services.ActiveSubstance
{
    public interface IActiveSubstanceService
    {
        Task<bool> CreateActiveSubstance(ActiveSubstanceCreationRequest activeSubstanceCreationRequest);
        Task<bool> RemoveActiveSubstance(int activeSubstanceId);

        Task<List<ActiveSubstanceResponse>> GetAllActiveSubstances();
    }
}