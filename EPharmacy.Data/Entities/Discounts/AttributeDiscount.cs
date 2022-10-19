using EPharmacy.Data.Entities.Attributes;

namespace EPharmacy.Data.Entities.Discounts
{
    public class AttributeDiscount
    {
        #region Foreign keys, relations
        public virtual Attribute Attribute { get; set; }
        public virtual int AttributeId { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual int DiscountId { get; set; }
        #endregion
    }
}
