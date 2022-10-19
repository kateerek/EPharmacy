using System.Collections.Generic;

namespace EPharmacy.ServerApp.Models.Attributes.GetDetailsForAttributes
{
    public class AttributesDetailsListRequest
    {
        public IEnumerable<int> AttributeIds { get; set; }
    }
}
