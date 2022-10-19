using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace EPharmacy.Data.Entities.Users
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {

        }

        public virtual List<ApplicationUserRole> RoleUsers { get; set; }
    }
}