namespace GiveCRM.Admin.BusinessLogic.Tests
{
    using GiveCRM.Admin.BusinessLogic;
    using GiveCRM.Admin.Models;
    using NSubstitute;
    using NUnit.Framework;

    [TestFixture]
    public class SignupServiceFacts
    {
        [TestFixture]
        public class RegisterUserShould
        {
            [Test]
            public void ReturnSuccessResult_WhenThereAreNoProblemsRegisteringTheUser()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.RegisterUser(new RegistrationInfo
                {
                    CharityName = "Charity",
                    EmailAddress = "foo@charity.org",
                    Password = "Char.1ty",
                    SubDomain = "charity",
                    TermsAccepted = true
                });
                
                Assert.That(result, Is.EqualTo(UserCreationResult.Success));
            }

            [Test]
            public void ReturnInvalidPasswordResult_WhenThePasswordContainsNoDigits()
            {
                var membershipService = Substitute.For<IMembershipService>();
                membershipService.CreateUser(string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(UserCreationResult.InvalidPassword);
                
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.RegisterUser(new RegistrationInfo
                {
                    CharityName = "Charity",
                    EmailAddress = "foo@charity.org",
                    Password = "Char.ity",
                    SubDomain = "charity",
                    TermsAccepted = true
                });

                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidPassword));
            }

            [Test]
            public void ReturnInvalidPasswordResult_WhenThePasswordContainsNoCapitalLetters()
            {
                var membershipService = Substitute.For<IMembershipService>();
                membershipService.CreateUser(string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(UserCreationResult.InvalidPassword);

                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.RegisterUser(new RegistrationInfo
                {
                    CharityName = "Charity",
                    EmailAddress = "foo@charity.org",
                    Password = "char.1ty",
                    SubDomain = "charity",
                    TermsAccepted = true
                });

                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidPassword));
            }

            [Test]
            public void ReturnInvalidPasswordResult_WhenThePasswordContainsNoSymbols()
            {
                var membershipService = Substitute.For<IMembershipService>();
                membershipService.CreateUser(string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(UserCreationResult.InvalidPassword);

                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.RegisterUser(new RegistrationInfo
                {
                    CharityName = "Charity",
                    EmailAddress = "foo@charity.org",
                    Password = "Char1ty",
                    SubDomain = "charity",
                    TermsAccepted = true
                });

                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidPassword));
            }

            [Test]
            public void ReturnInvalidEmailResult_WhenTheEmailContainsNoAtSymbol()
            {
                var membershipService = Substitute.For<IMembershipService>();
                membershipService.CreateUser(string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(UserCreationResult.InvalidEmail);

                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.RegisterUser(new RegistrationInfo
                {
                    CharityName = "Charity",
                    EmailAddress = "foocharity.org",
                    Password = "Char.1ty",
                    SubDomain = "charity",
                    TermsAccepted = true
                });

                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidEmail));
            }

            [Test]
            public void ReturnDuplicateEmailResult_WhenTheEmailHasAlreadyBeenRegistered()
            {
                var membershipService = Substitute.For<IMembershipService>();
                membershipService.CreateUser(string.Empty, string.Empty, string.Empty).ReturnsForAnyArgs(UserCreationResult.DuplicateEmail);

                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.RegisterUser(new RegistrationInfo
                {
                    CharityName = "Charity",
                    EmailAddress = "foo@charity.org",
                    Password = "Char.1ty",
                    SubDomain = "charity",
                    TermsAccepted = true
                });

                Assert.That(result, Is.EqualTo(UserCreationResult.DuplicateEmail));
            }
        }

        [TestFixture]
        public class RegisterCharityShould
        {
            [Test]
            public void ReturnSuccessResult_WhenThereAreNoProblemsRegistering()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.RegisterCharity(new RegistrationInfo
                {
                    CharityName = "Charity",
                    EmailAddress = "foo@charity.org",
                    Password = "Char.1ty",
                    SubDomain = "charity",
                    TermsAccepted = true
                });

                Assert.That(result, Is.EqualTo(CharityCreationResult.Success));
            }

            [Test]
            public void ReturnInvalidNameResult_WhenTheCharityNameContainsInvalidCharacters()
            {
                // Do we even want this?
            }

            [Test]
            public void ReturnInvalidSubdomainResult_WhenTheSubdomainContainsADotCharacter()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                charityMembershipService.RegisterCharityWithUser(null, null).ReturnsForAnyArgs(CharityCreationResult.InvalidSubdomain);
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.RegisterCharity(new RegistrationInfo
                {
                    CharityName = "Charity",
                    EmailAddress = "foo@charity.org",
                    Password = "Char.1ty",
                    SubDomain = "char.ity",
                    TermsAccepted = true
                });

                Assert.That(result, Is.EqualTo(CharityCreationResult.InvalidSubdomain));
            }

            [Test]
            public void ReturnInvalidSubdomainResult_WhenTheSubdomainContainsASlashCharacter()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                charityMembershipService.RegisterCharityWithUser(null, null).ReturnsForAnyArgs(CharityCreationResult.InvalidSubdomain);
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.RegisterCharity(new RegistrationInfo
                {
                    CharityName = "Charity",
                    EmailAddress = "foo@charity.org",
                    Password = "Char.1ty",
                    SubDomain = "char/ity",
                    TermsAccepted = true
                });

                Assert.That(result, Is.EqualTo(CharityCreationResult.InvalidSubdomain));
            }

            [Test]
            public void ReturnDuplicateSubdomainResult_WhenTheSubdomainHasAlreadyBeenRegistered()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                charityMembershipService.RegisterCharityWithUser(null, null).ReturnsForAnyArgs(CharityCreationResult.DuplicateSubdomain);
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.RegisterCharity(new RegistrationInfo
                {
                    CharityName = "Charity",
                    EmailAddress = "foo@charity.org",
                    Password = "Char.1ty",
                    SubDomain = "charity",
                    TermsAccepted = true
                });

                Assert.That(result, Is.EqualTo(CharityCreationResult.DuplicateSubdomain));
            }
        }

        [TestFixture]
        public class MiscFacts
        {
            [Test]
            public void ReturnInvalidUsernameResult_WhenTheUsernameContainsForbiddenCharacters()
            {
                // Needs fleshing out with individual test cases for each forbidden character
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidUsername));
            }

            [Test]
            public void ReturnDuplicateUsernameResult_WhenTheUsernameHasAlreadyBeenRegistered()
            {
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.DuplicateUsername));
            }

            [Test]
            public void ReturnInvalidCharityNumberResult_WhenTheRegisteredCharityNumberIsInvalid()
            {
                // We don't have a way to verify this yet, and it's not REQUIRED info, so isn't 
                // handled by RegisterCharity.
                CharityCreationResult result = CharityCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(CharityCreationResult.InvalidCharityNumber));
            }

            [Test]
            public void ReturnDuplicateCharityNumberResult_WhenTheCharityNumberIsAlreadyRegistered()
            {
                CharityCreationResult result = CharityCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(CharityCreationResult.DuplicateCharityNumber));
            }
        }
    }
}
