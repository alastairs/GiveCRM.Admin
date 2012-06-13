namespace GiveCRM.Admin.BusinessLogic
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;
    using GiveCRM.Admin.Models;

    public class SignupService : ISignupService
    {
        private readonly IMembershipService membershipService;
        private readonly ICharityMembershipService charityMembershipService;

        public SignupService(IMembershipService membershipService, ICharityMembershipService charityMembershipService)
        {
            if (membershipService == null)
            {
                throw new ArgumentNullException("membershipService");
            }

            if (charityMembershipService == null)
            {
                throw new ArgumentNullException("charityMembershipService");
            }

            this.membershipService = membershipService;
            this.charityMembershipService = charityMembershipService;
        }

        public CharityProvisioningResult ProvisionCharity(RegistrationInfo registrationInfo, Guid activationToken)
        {
            return CharityProvisioningResult.Success;
        }

        public UserCreationResult RegisterUser(RegistrationInfo registrationInfo)
        {
            string userIdentifier = registrationInfo.EmailAddress;
            var userRegistrationStatus = this.membershipService.CreateUser(userIdentifier, registrationInfo.Password, userIdentifier);
            return userRegistrationStatus;
        }

        public CharityCreationResult RegisterCharity(RegistrationInfo registrationInfo)
        {
            var membershipUser = this.membershipService.GetUser(registrationInfo.EmailAddress);
            if (membershipUser != null)
            {
                return this.charityMembershipService.RegisterCharity(registrationInfo);
            }

            return CharityCreationResult.UnexpectedFailure;
        }

        public string GetSubDomainFromCharityName(string charityName)
        {
            var subdomain = charityName.Trim();
            
            subdomain = Regex.Replace(subdomain, @"[\s]+", "-");
            subdomain = Regex.Replace(subdomain, @"[^\w-]", string.Empty);

            subdomain = subdomain.ToLower();
            
            return new string(subdomain.Take(63).ToArray());
        }

        public string GetSubDomainFromActivationToken(string activationToken)
        {
            //TODO get this from database
            var token = "";
            return token == null ? "" : token.ToString();
        }

        public OptionalSignupInformationValidationResult SaveOptionalSignupInformation()
        {
            return OptionalSignupInformationValidationResult.Success;
        }
    }
}
