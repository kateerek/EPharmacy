using System.Collections.Generic;
using EPharmacy.Data.Entities.Products;
using Microsoft.AspNetCore.Identity;

namespace EPharmacy.Data.Entities.Users
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual List<ApplicationUserRole> UserRoles { get; set; }
        public virtual List<FavouriteProduct> FavouriteProducts{ get; set; }
    }
}