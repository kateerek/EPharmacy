using System.ComponentModel.DataAnnotations;

namespace EPharmacy.ServerApp.Models.Attributes.Create
{
    public class AttributeCreationRequest
    {
        public string Name;

        [StringLength(20, MinimumLength = 1)]
        [RegularExpression(@"[^\s]+")]
        public string InternalName;

        public string Description;
    }
}
