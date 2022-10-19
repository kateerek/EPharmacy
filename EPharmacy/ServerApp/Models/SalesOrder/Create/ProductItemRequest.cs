namespace EPharmacy.ServerApp.Models.SalesOrder.Create
{
    public class ProductItemRequest
    {
        public int ProductId { get; set; }

        public int ItemCount { get; set; }

        public int? DiscountId { get; set; }
    }
}