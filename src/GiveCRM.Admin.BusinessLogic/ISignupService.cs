namespace GiveCRM.Admin.BusinessLogic
{
    using System;
    using GiveCRM.Admin.Models;

    public interface ISignupService
    {
        CharityProvisioningResult ProvisionCharity(RegistrationInfo registrationInfo, Guid activationToken);
        UserCreationResult RegisterUser(RegistrationInfo registrationInfo);
        CharityCreationResult RegisterCharity(RegistrationInfo registrationInfo);
        string GetSubDomainFromCharityName(string charityName);
        string GetSubDomainFromActivationToken(string activationToken);
        OptionalSignupInformationValidationResult SaveOptionalSignupInformation();
    }
}
