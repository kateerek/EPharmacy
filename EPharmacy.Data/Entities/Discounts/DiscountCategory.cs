using EPharmacy.Data.Entities.Common;

namespace EPharmacy.Data.Entities.Discounts
{
    public class DiscountCategory : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
