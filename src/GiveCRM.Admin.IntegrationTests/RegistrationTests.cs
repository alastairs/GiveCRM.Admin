namespace GiveCRM.IntegrationTests
{
    using System.Web.Security;
    using GiveCRM.Admin.BusinessLogic;
    using GiveCRM.Admin.DataAccess;
    using GiveCRM.Admin.TestUtils;
    using GiveCRM.Admin.Web.Controllers;
    using GiveCRM.Admin.Web.Services;
    using GiveCRM.Admin.Web.ViewModels.SignUp;
    using MvcContrib.TestHelper;
    using NUnit.Framework;

    public class RegistrationTests
    {
        [TestFixture]
        public class RegistrationShould : IntegrationTest
        {
            private IMembershipService membershipService;
            private ICharityMembershipService charityMembershipService;

            protected override void TestSetup()
            {
                base.TestSetup();

                this.membershipService = new AspMembershipService(Membership.Provider);
                this.charityMembershipService = new CharityMembershipService(new Charities(this.DatabaseConnectionProvider),
                                                                             new CharitiesMemberships(this.DatabaseConnectionProvider));
            }

            [Test]
            public void RedirectToCompleteAction_OnSuccessfulRegistration()
            {
                var signUpController = new SignUpController(new WebConfigConfiguration(), 
                                                            new SignupService(this.membershipService, this.charityMembershipService));

                var requiredInfo = new RequiredInfoViewModel
                                       {
                                           CharityName = "RSPCA",
                                           Password = "foobar-2000",
                                           TermsAccepted = true,
                                           UserIdentifier = "foo@bar.com"
                                       };

                var result = signUpController.SignUp(requiredInfo);

                result.AssertActionRedirect().ToAction("Complete");
            }
        }
    }
}
