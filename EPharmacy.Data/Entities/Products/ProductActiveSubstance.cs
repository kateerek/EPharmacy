using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using EPharmacy.Data.Entities.ActiveSubstances;
using EPharmacy.Data.Entities.Common;

namespace EPharmacy.Data.Entities.Products
{
    public class ProductActiveSubstance : BaseEntity
    {
        #region Constructors
        public ProductActiveSubstance()
        { }

        public ProductActiveSubstance(int productId, int activeSubstanceId
            , decimal amount)
        {
            this.ProductId = productId;
            this.ActiveSubstanceId = activeSubstanceId;
            this.Amount = amount;
        }

        public ProductActiveSubstance(Product product, ActiveSubstance activeSubstance
            , decimal amount)
        {
            this.Product = product;
            this.ActiveSubstance = activeSubstance;
            this.Amount = amount;
        }
        #endregion

        #region Foreign keys, relations
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public int ActiveSubstanceId { get; set; }
        [ForeignKey("ActiveSubstanceId")]
        public virtual ActiveSubstance ActiveSubstance { get; set; }
        #endregion
        
        #region Public properties
        public decimal Amount { get; set; }
        #endregion
    }
}
