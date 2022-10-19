using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPharmacy.Data;
using EPharmacy.Data.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace EPharmacy.ServerApp.Services.Prescription
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly EPharmacyContext _context;

        public PrescriptionService(EPharmacyContext context)
        {
            this._context = context;
        }

        public async Task<PrescriptionInformation> GetPrescriptionInformationById(int prescriptionInformationId)
        {
            return await this._context.PrescriptionInformation.FirstOrDefaultAsync(
                pi => pi.Id == prescriptionInformationId);
        }
    }
}
