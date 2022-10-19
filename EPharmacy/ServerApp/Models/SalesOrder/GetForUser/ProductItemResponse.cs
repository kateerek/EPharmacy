using EPharmacy.ServerApp.Models.Discounts.Models;
using EPharmacy.ServerApp.Models.Product.ProductsDetailsList;

namespace EPharmacy.ServerApp.Models.SalesOrder.GetForUser
{
    public class ProductItemResponse
    {
        public ProductDetailsListModel Product { get; set; }

        public double? PriceWithDiscount { get; set; }

        public PrescriptionCategoryInfoModel PrescriptionCategoryInfoModel { get; set; }
        
        public int ItemCount { get; set; }
        
        
    }
}