namespace GiveCRM.Admin.BusinessLogic
{
    using System;

    using GiveCRM.Admin.Models;

    public interface ISignupService
    {
        void ProvisionCharity(RegistrationInfo registrationInfo, Guid activationToken);
        UserCreationResult RegisterUser(RegistrationInfo registrationInfo);
        bool RegisterCharity(RegistrationInfo registrationInfo);
        string GetSubDomainFromCharityName(string charityName);
        string GetSubDomainFromActivationToken(string activationToken);
    }
}
