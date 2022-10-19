using EPharmacy.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPharmacy.Data.Entities.Products
{
    public class PrescriptionInformation : BaseEntity
    {
        #region Foreign keys, relations
        public virtual List<Product> Products{ get; set; }
        #endregion
    }
}
