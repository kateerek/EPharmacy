using System;
using System.Collections.Generic;
using System.Text;
using EPharmacy.Data.Entities.Common;
using EPharmacy.Data.Entities.Users;

namespace EPharmacy.Data.Entities.Products
{
    public class FavouriteProduct 
    {
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
