using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.BusinessLogic
{
    public interface ICharityMembershipService
    {
        CharityCreationResult RegisterCharityWithUser(RegistrationInfo registrationInfo, User user);
    }
}
