namespace GiveCRM.Admin.Models.Test
{
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
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.Success));
            }

            [Test]
            public void ReturnInvalidUsernameResult_WhenTheUsernameContainsForbiddenCharacters()
            {
                // Needs fleshing out with individual test cases for each forbidden character
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidUsername));
            }

            [Test]
            public void ReturnInvalidPasswordResult_WhenThePasswordContainsNoDigits()
            {
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidPassword));
            }

            [Test]
            public void ReturnInvalidPasswordResult_WhenThePasswordContainsNoCapitalLetters()
            {
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidPassword));
            }

            [Test]
            public void ReturnInvalidPasswordResult_WhenThePasswordContainsNoSymbols()
            {
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidPassword));
            }

            [Test]
            public void ReturnInvalidEmailResult_WhenTheEmailContainsNoAtSymbol()
            {
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.InvalidEmail));
            }

            [Test]
            public void ReturnDuplicateUsernameResult_WhenTheUsernameHasAlreadyBeenRegistered()
            {
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.DuplicateUsername));
            }

            [Test]
            public void ReturnDuplicateEmailResult_WhenTheEmailHasAlreadyBeenRegistered()
            {
                UserCreationResult result = UserCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(UserCreationResult.DuplicateEmail));
            }
        }

        [TestFixture]
        public class RegisterCharityShould
        {
            [Test]
            public void ReturnSuccessResult_WhenThereAreNoProblemsRegistering()
            {
                CharityCreationResult result = CharityCreationResult.UnexpectedFailure;
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
                CharityCreationResult result = CharityCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(CharityCreationResult.InvalidSubdomain));
            }

            [Test]
            public void ReturnInvalidSubdomainResult_WhenTheSubdomainContainsASlashCharacter()
            {
                CharityCreationResult result = CharityCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(CharityCreationResult.InvalidSubdomain));
            }

            [Test]
            public void ReturnDuplicateSubdomainResult_WhenTheSubdomainHasAlreadyBeenRegistered()
            {
                CharityCreationResult result = CharityCreationResult.UnexpectedFailure;
                Assert.That(result, Is.EqualTo(CharityCreationResult.DuplicateSubdomain));
            }
        }

        [TestFixture]
        public class MiscFacts
        {
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
