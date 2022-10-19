using EPharmacy.ServerApp.Models.Discounts.Responses;

namespace EPharmacy.ServerApp.Models.Product.ProductsDetailsList
{
    public class ProductDetailsListModel
    {
        public int  Id { get; set; }
        
        public string Name { get; set; }
        
        public double ProductPrice { get; set; }

        public string ImageUrl { get; set; }

        public bool? IsFavourite { get; set; } = null;

        public ProductDiscountResponse ProductDiscounts { get; set; }
    }
}