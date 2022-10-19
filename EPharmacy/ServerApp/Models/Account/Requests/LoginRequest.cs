using System.ComponentModel.DataAnnotations;

namespace EPharmacy.ServerApp.Models.Account.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}