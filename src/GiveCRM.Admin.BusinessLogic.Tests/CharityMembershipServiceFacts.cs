namespace GiveCRM.Admin.BusinessLogic.Tests
{
    using GiveCRM.Admin.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class CharityMembershipServiceFacts
    {
        [TestFixture]
        public class RegisterCharityWithUserShould
        {
            [Test]
            public void ReturnSuccessResult_WhenThereAreNoProblemsCreatingTheMemberCharityRelationship()
            {
                var charityRepository = Substitute.For<ICharityRepository>();
                var charitiesMembersRepository = Substitute.For<ICharitiesMembershipRepository>();
                charitiesMembersRepository.Save(null).ReturnsForAnyArgs(new CharityMembership
                {
                    Id = 1,
                    CharityId = 1,
                    UserName = "foo"
                });
                var charityMembershipService = new CharityMembershipService(charityRepository, charitiesMembersRepository);

                var result = charityMembershipService.RegisterCharityWithUser(
                    new Charity
                    {
                        Id = 1,
                        Name = "Foo",
                        SubDomain = "foo"
                    }, 
                    new User
                    {
                        Id = 1,
                        Email = "foo@bar.com",
                        Username = "foo"
                    });

                Assert.That(result, Is.EqualTo(CharityCreationResult.Success));
            }
        }
    }
}
