using EPharmacy.Data.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EPharmacy.Data.Entities.Products
{
    public class Producer : BaseEntity
    {
        #region Foreign keys, relations
        public virtual List<Product> Products { get; set; }
        #endregion

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
