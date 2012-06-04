namespace GiveCRM.Admin.Web.Controllers
{
    using System;
    using System.Text.RegularExpressions;
    using System.Web.Mvc;

    using AutoMapper;
    
    using GiveCRM.Admin.BusinessLogic;
    using GiveCRM.Admin.Models;
    using GiveCRM.Admin.Web.Helpers;
    using GiveCRM.Admin.Web.Services;
    using GiveCRM.Admin.Web.ViewModels.SignUp;
    using IConfiguration = GiveCRM.Admin.Web.Interfaces.IConfiguration;

    public class SignUpController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ISignupService signupService;

        public SignUpController(IConfiguration configuration, ISignupService signupService)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (signupService == null)
            {
                throw new ArgumentNullException("signupService");
            }

            this.configuration = configuration;
            this.signupService = signupService;
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult SignUp(RequiredInfoViewModel requiredInfoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(requiredInfoViewModel);
            }
            
            var registrationInfo = GetRegistrationInfo(requiredInfoViewModel);
            registrationInfo.SubDomain = signupService.GetSubDomainFromCharityName(registrationInfo.CharityName);

            var userRegistrationStatus = this.signupService.RegisterUser(registrationInfo);
            if (userRegistrationStatus != UserCreationResult.Success)
            {
                return this.HandleUserRegistrationError(requiredInfoViewModel, userRegistrationStatus);
            }

            if (userRegistrationStatus == UserCreationResult.Success)
            {
                var charityRegistrationStatus = this.signupService.RegisterCharity(registrationInfo);
                if (charityRegistrationStatus == CharityCreationResult.Success)
                {
                    this.signupService.ProvisionCharity(registrationInfo, TokenHelper.CreateRandomIdentifier());
                    this.TempData["SubDomain"] = this.signupService.GetSubDomainFromCharityName(requiredInfoViewModel.CharityName);

                    return this.RedirectToAction("Complete");
                }

                this.ModelState.AddModelError("", "Charity registration failed. Please contact support.");
                return this.View();
            }

            ModelState.AddModelError("", "User registration failed. Please contact support.");
            return View();
        }

        private ActionResult HandleUserRegistrationError(RequiredInfoViewModel requiredInfoViewModel, UserCreationResult userRegistrationStatus)
        {
            string modelErrorKey;
            string modelErrorMessage;

            switch (userRegistrationStatus)
            {
                case UserCreationResult.DuplicateEmail:
                    {
                        modelErrorKey = "EmailAddress";
                        modelErrorMessage = "You have already registered with GiveCRM.  Would you like to log in instead?";
                    }
                    break;
                case UserCreationResult.DuplicateUsername:
                    {
                        modelErrorKey = "UserIdentifier";
                        modelErrorMessage = "You have already registered with GiveCRM.  Would you like to log in instead?";
                    }
                    break;
                default:
                    throw new ArgumentException("Unknown value of UserCreationResult", "userRegistrationStatus");
            }

            this.ModelState.AddModelError(modelErrorKey, modelErrorMessage);
            return View("SignUp", requiredInfoViewModel);
        }

        private static RegistrationInfo GetRegistrationInfo(RequiredInfoViewModel requiredInfoViewModel)
        {
            var registrationInfo = new RegistrationInfo();
            Mapper.DynamicMap(requiredInfoViewModel, registrationInfo);
            Mapper.Map(requiredInfoViewModel, registrationInfo, options => registrationInfo.EmailAddress = requiredInfoViewModel.UserIdentifier);
            return registrationInfo;
        }

        [HttpGet]
        public ActionResult Complete()
        {
            var subDomain = TempData["SubDomain"] as string;

            var viewModel = new CompleteViewModel
                                {
                                    SubDomain = subDomain
                                }.WithConfig(configuration);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult StoreAdditionalInfo(CompleteViewModel completeViewModel)
        {
            var viewModel = completeViewModel.WithConfig(configuration);

            if (!ModelState.IsValid)
            {
                return View("Complete", viewModel);
            }

            return View("Complete", viewModel);
        }

        public ActionResult StartSite(string id)
        {
            var subDomain = this.signupService.GetSubDomainFromActivationToken(id);

            var viewModel = new StartSiteViewModel
                                {
                                    Url = configuration.CrmTestUrl,
                                    SubDomain = subDomain
                                };

            return View(viewModel);
        }
    }
}
