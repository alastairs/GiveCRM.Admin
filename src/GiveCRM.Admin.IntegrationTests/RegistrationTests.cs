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
        private static readonly IDatabaseProvider DatabaseConnectionProvider;

        static RegistrationTests()
        {
            DatabaseConnectionProvider = new SimpleDataFileDatabaseProvider("IntegrationTestDB.sdf");
        }

        [TestFixture]
        public class RegistrationShould
        {
            [Test]
            public void RedirectToCompleteAction_OnSuccessfulRegistration()
            {
                var signUpController = new SignUpController(new WebConfigConfiguration(), 
                                                            new SignupService(new AspMembershipService(Membership.Provider), 
                                                                              new CharityMembershipService(new Charities(DatabaseConnectionProvider), 
                                                                                                           new CharitiesMemberships(DatabaseConnectionProvider))));

                var requiredInfo = new RequiredInfoViewModel
                                       {
                                           CharityName = "Royal Society for the Protection of Tests",
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
