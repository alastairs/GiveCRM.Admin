namespace GiveCRM.IntegrationTests
{
    using System.Web.Security;
    using GiveCRM.Admin.BusinessLogic;
    using GiveCRM.Admin.DataAccess;
    using GiveCRM.Admin.Web.Controllers;
    using GiveCRM.Admin.Web.Interfaces;
    using GiveCRM.Admin.Web.Services;
    using NSubstitute;
    using NUnit.Framework;

    public class RegistrationTests
    {
        private static readonly SimpleDataNamedConnectionDatabaseProvider DatabaseConnectionProvider;

        static RegistrationTests()
        {
            DatabaseConnectionProvider = new SimpleDataNamedConnectionDatabaseProvider("");
        }

        [TestFixture]
        public class RegistrationShould
        {
            [Test]
            public void RedirectToCompleteAction_OnSuccessfulRegistration()
            {
                var signUpController = new SignUpController(Substitute.For<IConfiguration>(), 
                                                            new SignupService(new AspMembershipService(new SqlMembershipProvider()), 
                                                                              new CharityMembershipService(new Charities(DatabaseConnectionProvider), 
                                                                                                           new CharitiesMemberships(DatabaseConnectionProvider))));
            }
        }
    }
}
