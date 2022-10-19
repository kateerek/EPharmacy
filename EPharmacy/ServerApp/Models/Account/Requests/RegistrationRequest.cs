using System.ComponentModel.DataAnnotations;

namespace EPharmacy.ServerApp.Models.Account.Requests
{
    public class RegistrationRequest
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}