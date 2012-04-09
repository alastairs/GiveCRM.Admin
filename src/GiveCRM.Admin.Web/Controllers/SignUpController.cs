using System;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using GiveCRM.Admin.BusinessLogic;
using GiveCRM.Admin.Models;
using GiveCRM.Admin.Web.Extensions;
using GiveCRM.Admin.Web.Helpers;
using GiveCRM.Admin.Web.Interfaces;
using GiveCRM.Admin.Web.Services;
using GiveCRM.Admin.Web.ViewModels.SignUp;
using IConfiguration = GiveCRM.Admin.Web.Interfaces.IConfiguration;

namespace GiveCRM.Admin.Web.Controllers
{
    public class SignUpController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ISignUpQueueingService signUpQueueingService;
        private readonly ICharityMembershipService charityMembershipService;
        private readonly IMembershipService membershipService;

        public SignUpController(IConfiguration configuration, ISignUpQueueingService signUpQueueingService, ICharityMembershipService charityMembershipService, IMembershipService membershipService)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException("configuration");
            }

            if (signUpQueueingService == null)
            {
                throw new ArgumentNullException("signUpQueueingService");
            }

            if (charityMembershipService == null)
            {
                throw new ArgumentNullException("charityMembershipService");
            }

            if (membershipService == null)
            {
                throw new ArgumentNullException("membershipService");
            }

            this.configuration = configuration;
            this.signUpQueueingService = signUpQueueingService;
            this.charityMembershipService = charityMembershipService;
            this.membershipService = membershipService;
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

            var userRegistrationStatus = this.RegisterUser(registrationInfo);
            if (userRegistrationStatus != UserCreationResult.Success)
            {
                return this.HandleUserRegistrationError(requiredInfoViewModel, userRegistrationStatus);
            }

            if (userRegistrationStatus == UserCreationResult.Success)
            {
                if (this.CreateCharity(registrationInfo))
                {
                    this.ProvisionService(requiredInfoViewModel, TokenHelper.CreateRandomIdentifier());
                    this.TempData["SubDomain"] = GetSubDomainFromCharityName(requiredInfoViewModel.CharityName);

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

        private UserCreationResult RegisterUser(RegistrationInfo registrationInfo)
        {
            string userIdentifier = registrationInfo.EmailAddress;
            var userRegistrationStatus = this.membershipService.CreateUser(userIdentifier, registrationInfo.Password, userIdentifier);
            return userRegistrationStatus;
        }

        private static RegistrationInfo GetRegistrationInfo(RequiredInfoViewModel requiredInfoViewModel)
        {
            var registrationInfo = new RegistrationInfo();
            Mapper.DynamicMap(requiredInfoViewModel, registrationInfo);
            return registrationInfo;
        }

        private void ProvisionService(RequiredInfoViewModel requiredInfoViewModel, Guid activationToken)
        {
            var emailViewModel = new EmailViewModel
                                     {
                                         To = requiredInfoViewModel.UserIdentifier,
                                         ActivationToken = activationToken.AsQueryString()
                                     };

            signUpQueueingService.QueueEmail(emailViewModel);
            signUpQueueingService.QueueProvisioning();
        }

        private bool CreateCharity(RegistrationInfo registrationInfo)
        {
            var membershipUser = membershipService.GetUser(registrationInfo.EmailAddress);
            if (membershipUser != null)
            {
                var user = new User {Email = membershipUser.Email, Username = membershipUser.UserName};
                return charityMembershipService.RegisterCharityWithUser(registrationInfo, user);
            }

            return false;
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

        private static string GetSubDomainFromCharityName(string charityName)
        {
            var result = Regex.Replace(charityName, @"[\s]", "-");
            return Regex.Replace(result, @"[^\w-]", "").ToLower();
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
            var subDomain = GetSubDomainFromActivationToken(id);

            var viewModel = new StartSiteViewModel
                                {
                                    Url = configuration.CrmTestUrl,
                                    SubDomain = subDomain
                                };

            return View(viewModel);
        }

        private string GetSubDomainFromActivationToken(string activationToken)
        {
            //TODO get this from database
            var token = "";
            return token == null ? "" : token.ToString();
        }
    }
}
