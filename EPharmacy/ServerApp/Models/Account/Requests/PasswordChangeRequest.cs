namespace EPharmacy.ServerApp.Models.Account.Requests
{
    public class PasswordChangeRequest
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}