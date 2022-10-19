using System.ComponentModel.DataAnnotations.Schema;
using EPharmacy.Data.Entities.Attributes;
using EPharmacy.Data.Entities.Common;

namespace EPharmacy.Data.Entities.Products
{
    public class ProductAttributeValue : BaseEntity
    {
        #region Constructors
        public ProductAttributeValue()
        { }

        public ProductAttributeValue(Product product, AttributeValue attributeValue)
        {
            this.Product = product;
            this.AttributeValue = attributeValue;
            this.IsActive = false;
        }
        #endregion
    
        #region Foreign keys, relations
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        [ForeignKey("AttributeValueId")]
        public int AttributeValueId { get; set; }        
        public virtual AttributeValue AttributeValue { get; set; }
        #endregion

        public bool IsActive { get; set; }
    }
}
