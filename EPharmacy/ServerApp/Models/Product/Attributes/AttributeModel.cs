namespace EPharmacy.ServerApp.Models.Product.Attributes
{
    public class AttributeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string InternalName { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }
    }
}