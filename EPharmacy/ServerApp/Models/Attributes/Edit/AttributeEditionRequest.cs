using System.ComponentModel.DataAnnotations;

namespace EPharmacy.ServerApp.Models.Attributes.Edit
{ 
    public class AttributeEditionRequest
    {
        public int AttributeToEditId;

        public string Name;

        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"[^\s]+")]
        public string InternalName;

        public string Description;
    }
}

