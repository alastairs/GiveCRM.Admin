using System;
using GiveCRM.Admin.Models;

namespace GiveCRM.Admin.BusinessLogic
{
    public class CharityMembershipService : ICharityMembershipService
    {
        private readonly ICharityRepository charityRepository;
        private readonly ICharitiesMembershipRepository charitiesMembershipRepository;

        public CharityMembershipService(ICharityRepository charityRepository, ICharitiesMembershipRepository charitiesMembershipRepository)
        {
            if (charityRepository == null)
            {
                throw new ArgumentNullException("charityRepository");
            }

            if (charitiesMembershipRepository == null)
            {
                throw new ArgumentNullException("charitiesMembershipRepository");
            }

            this.charityRepository = charityRepository;
            this.charitiesMembershipRepository = charitiesMembershipRepository;
        }

        public CharityCreationResult RegisterCharity(RegistrationInfo registrationInfo)
        {
            if (registrationInfo == null)
            {
                throw new ArgumentNullException("registrationInfo");
            }

            var charity = new Charity
                              {
                                  Name = registrationInfo.CharityName,
                                  SubDomain = registrationInfo.SubDomain
                              };

            var newCharity = this.charityRepository.Save(charity);
            if (newCharity == null)
            {
                return CharityCreationResult.UnexpectedFailure;
            }

            return CharityCreationResult.Success;
        }

        public CharityCreationResult RegisterCharityWithUser(Charity charity, User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            var newCharityMembership = this.charitiesMembershipRepository.Save(new CharityMembership
            {
                CharityId = charity.Id,
                UserName = user.Email
            });

            if (newCharityMembership != null)
            {
                return CharityCreationResult.Success;
            }

            return CharityCreationResult.UnexpectedFailure;
        }
    }
}
