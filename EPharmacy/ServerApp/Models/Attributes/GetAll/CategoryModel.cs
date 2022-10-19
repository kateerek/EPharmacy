using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Attributes.GetAll
{
    public class CategoryModel
    {
        public AttributeResponseModel Attribute { get; set; }
        public IList<AttributeResponseModel> SubAttributes { get; set; }
    }
}
