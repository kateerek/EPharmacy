using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPharmacy.ServerApp.Models.Common.Responses
{
    public class BadRequestResponse : Dictionary<string, string[]>
    {
        public BadRequestResponse(IDictionary<string, string[]> dictionary) : base(dictionary) { }
    }
}
