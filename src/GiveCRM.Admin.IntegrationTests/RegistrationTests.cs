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

        private static void ClearTables()
        {
            var db = DatabaseConnectionProvider.GetDatabase();

            db.aspnet_Membership.Truncate();
            db.aspnet_Users.Truncate();
            db.aspnet_Applications.Truncate();

            db.Charity.Truncate();
            db.CharityMembership.Truncate();
        }

        [TestFixture]
        public class RegistrationShould
        {
            private IMembershipService membershipService;
            private ICharityMembershipService charityMembershipService;

            [SetUp]
            public void SetUp()
            {
                ClearTables();

                this.membershipService = new AspMembershipService(Membership.Provider);
                this.charityMembershipService = new CharityMembershipService(new Charities(DatabaseConnectionProvider),
                                                                             new CharitiesMemberships(DatabaseConnectionProvider));
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
