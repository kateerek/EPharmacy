using System.ComponentModel.DataAnnotations.Schema;
using EPharmacy.Data.Entities.Common;
using EPharmacy.Data.Entities.Discounts;
using EPharmacy.Data.Entities.Products;

namespace EPharmacy.Data.Entities.SalesOrders
{
    public class ProductItem : BaseEntity
    {
        public int SalesOrderId { get; set; }

        public virtual SalesOrder SalesOrder { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product{ get; set; }

        public decimal? PriceWithDiscount { get; set; }

        public int? DiscountCategoryId { get; set; }

        public int? DiscountId { get; set; }

        public virtual Discount Discount { get; set; }

        public virtual DiscountCategory DiscountCategory{ get; set; }

        public int ItemCount { get; set; }
    }
}