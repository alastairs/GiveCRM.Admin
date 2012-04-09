namespace GiveCRM.Admin.Web.Tests
{
    using GiveCRM.Admin.BusinessLogic;
    using GiveCRM.Admin.Models;
    using GiveCRM.Admin.Web.Controllers;
    using GiveCRM.Admin.Web.ViewModels.SignUp;

    using MvcContrib.TestHelper;
    using NSubstitute;
    using NUnit.Framework;
    using IConfiguration = GiveCRM.Admin.Web.Interfaces.IConfiguration;

    [TestFixture]
    public class SignUpControllerTests
    {
        private IConfiguration configuration;
        private ISignupService signupService;

        [SetUp]
        public void SetUp()
        {
            configuration = Substitute.For<IConfiguration>();
            this.signupService = Substitute.For<ISignupService>();
        }

        [Test]
        public void SignUp_RedirectsGetToHome()
        {
            var controller = new SignUpController(configuration, this.signupService);
            var result = controller.SignUp();

            result.AssertActionRedirect();
        }

        [Test]
        public void SignUp_WithSuccessfulStore_RedirectsToComplete()
        {
            var requiredInfoViewModel = new RequiredInfoViewModel
                                            {
                                                UserIdentifier = "A",
                                                Password = "B",
                                                CharityName = "C",
                                            };

            var controller = new SignUpController(configuration, this.signupService);
            signupService.RegisterCharity(null).ReturnsForAnyArgs(true);
            var result = controller.SignUp(requiredInfoViewModel);
            result.AssertActionRedirect().ToAction("Complete");
        }

        [Test]
        public void SignUp_WithUnsuccessfulUserCreation_RendersView()
        {
            var requiredInfoViewModel = new RequiredInfoViewModel
                                            {
                                                UserIdentifier = "A",
                                                Password = "B",
                                                CharityName = "C"
                                            };

            var controller = new SignUpController(configuration, this.signupService);
            signupService.RegisterUser(null).ReturnsForAnyArgs(UserCreationResult.DuplicateUsername);
            var result = controller.SignUp(requiredInfoViewModel);

            result.AssertViewRendered();
        }

        [Test]
        public void SignUp_WithDuplicateEmail_RendersView()
        {
            var requiredInfoViewModel = new RequiredInfoViewModel
                                            {
                                                UserIdentifier = "A",
                                                Password = "B",
                                                CharityName = "C"
                                            };

            var controller = new SignUpController(configuration, this.signupService);
            signupService.RegisterUser(null).ReturnsForAnyArgs(UserCreationResult.DuplicateEmail);
            var result = controller.SignUp(requiredInfoViewModel);

            result.AssertViewRendered();
        }

        [Test]
        public void SignUp_WithUnsuccessfulCharityCreation_RendersView()
        {
            var requiredInfoViewModel = new RequiredInfoViewModel
                                            {
                                                UserIdentifier = "A",
                                                Password = "B",
                                                CharityName = "C",
                                            };

            var controller = new SignUpController(configuration, this.signupService);
            signupService.RegisterCharity(null).ReturnsForAnyArgs(false);
            var result = controller.SignUp(requiredInfoViewModel);
            
            result.AssertViewRendered();
        }

        [Test]
        public void Complete_RendersView()
        {
            var controller = new SignUpController(configuration, this.signupService);
            var result = controller.Complete();
            result.AssertViewRendered().WithViewData<CompleteViewModel>();
        }

        [Test]
        public void StoreAdditionalInfo_RendersView()
        {
            var controller = new SignUpController(configuration, this.signupService);
            var result = controller.StoreAdditionalInfo(new CompleteViewModel());
            result.AssertViewRendered().WithViewData<CompleteViewModel>();
        }

        [Test]
        public void StartSite_RendersView()
        {
            var controller = new SignUpController(configuration, this.signupService);
            var result = controller.StartSite("");
            result.AssertViewRendered().WithViewData<StartSiteViewModel>();
        }
    }
}