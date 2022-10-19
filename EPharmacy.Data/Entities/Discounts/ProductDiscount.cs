using EPharmacy.Data.Entities.Products;

namespace EPharmacy.Data.Entities.Discounts
{
    public class ProductDiscount
    {
        #region Foreign keys, relations
        public virtual Product Product { get; set; }
        public virtual int ProductId { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual int DiscountId { get; set; }
        #endregion
    }
}
