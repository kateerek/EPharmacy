namespace EPharmacy.ServerApp.Services.MailSender
{
    public class SendGridOptions
    {
        public string ApiKey { get; set; }
        public string MailFrom { get; set; }
        public string NameFrom{ get; set; }
    }
}