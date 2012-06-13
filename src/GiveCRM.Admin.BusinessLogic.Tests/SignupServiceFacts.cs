namespace GiveCRM.Admin.BusinessLogic.Tests
{
    using System;
    using System.Linq;
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
            public void ReturnInvalidSubdomainResult_WhenTheSubdomainContainsADotCharacter()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                charityMembershipService.RegisterCharity(null).ReturnsForAnyArgs(CharityCreationResult.InvalidSubdomain);
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
                charityMembershipService.RegisterCharity(null).ReturnsForAnyArgs(CharityCreationResult.InvalidSubdomain);
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
                charityMembershipService.RegisterCharity(null).ReturnsForAnyArgs(CharityCreationResult.DuplicateSubdomain);
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
        public class ProvisionCharityShould
        {
            [Test]
            public void ReturnSuccessResult_WhenThereAreNoProblemsProvisioning()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var result = signupService.ProvisionCharity(new RegistrationInfo
                {
                    CharityName = "Foo",
                    EmailAddress = "foo@bar.com",
                    Password = "password",
                    SubDomain = "foo",
                    TermsAccepted = true
                }, Guid.NewGuid());

                Assert.That(result, Is.EqualTo(CharityProvisioningResult.Success));
            }
        }

        [TestFixture]
        public class GetSubdomainFromCharityNameShould
        {
            [Test]
            public void AllowLetters()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("foobar");

                Assert.That(subdomain, Is.EqualTo("foobar"));
            }

            [Test]
            public void AllowNumbers()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("1234");

                Assert.That(subdomain, Is.EqualTo("1234"));
            }

            [Test]
            public void AllowHyphens()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("Foo-Bar");

                Assert.That(subdomain, Is.EqualTo("foo-bar"));
            }

            [Test]
            public void ConvertTheCharityNameToLowerCase()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("FUBAR");

                Assert.That(subdomain, Is.EqualTo("fubar"));
            }

            [Test]
            public void ReturnASubdomainWithAtMost63Characters()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName(new string(Enumerable.Repeat('a', 64).ToArray()));

                Assert.That(subdomain.Length, Is.EqualTo(63));
            }

            [Test]
            public void ReplaceSpacesWithHyphens()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("Foo Bar");

                Assert.That(subdomain, Is.EqualTo("foo-bar"));
            }

            [Test]
            public void ReplaceTabsWithHyphens()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("Foo\tBar");

                Assert.That(subdomain, Is.EqualTo("foo-bar"));
            }

            [Test]
            public void ReplaceLineFeedsWithHyphens()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("Foo\nBar");

                Assert.That(subdomain, Is.EqualTo("foo-bar"));
            }

            [Test]
            public void ReplaceCarriageReturnsWithHyphens()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("Foo\rBar");

                Assert.That(subdomain, Is.EqualTo("foo-bar"));
            }

            [Test]
            public void ReplaceSequentialWhitespaceWithASingleHyphen()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("Foo \r\nBar");

                Assert.That(subdomain, Is.EqualTo("foo-bar"));
            }

            [Test]
            public void RemoveLeadingWhitespace()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("   foobar");

                Assert.That(subdomain, Is.EqualTo("foobar"));
            }

            [Test]
            public void RemoveTrailingWhitespace()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("foobar  ");

                Assert.That(subdomain, Is.EqualTo("foobar"));
            }

            [Test]
            public void RemoveInvalidCharacters()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("f@u-\"b\'a\\r");

                Assert.That(subdomain, Is.EqualTo("fu-bar"));
            }

            [Test]
            public void RemoveSequentialInvalidCharacters()
            {
                var membershipService = Substitute.For<IMembershipService>();
                var charityMembershipService = Substitute.For<ICharityMembershipService>();
                var signupService = new SignupService(membershipService, charityMembershipService);

                var subdomain = signupService.GetSubDomainFromCharityName("fu@\"\\bar");

                Assert.That(subdomain, Is.EqualTo("fubar"));
            }
        }

        [TestFixture]
        [Ignore("We don't have a way of implementing charity number validation at the moment.")]
        public class SaveOptionalSignupInformationShould
        {
            [Test]
            public void ReturnSuccessResult_WhenTheUsernameAndCharityNumberAreOk()
            {
                // Needs fleshing out with individual test cases for each forbidden character
                var signupService = new SignupService(Substitute.For<IMembershipService>(), Substitute.For<ICharityMembershipService>());
                var optionalSignupInformation = new OptionalSignupInformation {  };

                OptionalSignupInformationValidationResult result = signupService.SaveOptionalSignupInformation();

                Assert.That(result, Is.EqualTo(OptionalSignupInformationValidationResult.Success));
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

        [TestFixture]
        [Ignore("These tests don't currently have a home.")]
        public class MiscFacts
        {
            [Test]
            public void ReturnInvalidUsernameResult_WhenTheUsernameContainsForbiddenCharacters()
            {
                // Needs fleshing out with individual test cases for each forbidden character
                var signupService = new SignupService(Substitute.For<IMembershipService>(), Substitute.For<ICharityMembershipService>());

                OptionalSignupInformationValidationResult result = signupService.SaveOptionalSignupInformation();

                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidUsername));
            }

            [Test]
            public void ReturnDuplicateUsernameResult_WhenTheUsernameHasAlreadyBeenRegistered()
            {
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.DuplicateUsername));
            }
        }
    }
}
