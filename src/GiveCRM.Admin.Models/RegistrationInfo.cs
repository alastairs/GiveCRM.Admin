namespace GiveCRM.Admin.Models
{
    public class RegistrationInfo
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string CharityName { get; set; }
        public string SubDomain { get; set; }
        public bool TermsAccepted { get; set; }
    }
}
