using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.BusinessLogic
{
    public interface ICharityMembershipService
    {
        CharityCreationResult RegisterCharity(RegistrationInfo registrationInfo);
        CharityCreationResult RegisterCharityWithUser(Charity charity, User user);
    }
}
