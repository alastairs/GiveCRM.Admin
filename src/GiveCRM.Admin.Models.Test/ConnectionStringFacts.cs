namespace GiveCRM.Admin.Models.Test
{
    using System;
    using NUnit.Framework;

    public class ConnectionStringFacts
    {
        [TestFixture]
        public class ConstructorShould
        {
            [Test]
            public void ThrowAnArgumentNullException_WhenTheConnectionStringIsNull()
            {
                Assert.That(() => new ConnectionString(null), Throws.InstanceOf<ArgumentNullException>());
            }

            [Test]
            public void ThrowAnArgumentNullException_WhenTheConnectionStringIsTheEmptyString()
            {
                Assert.That(() => new ConnectionString(string.Empty), Throws.InstanceOf<ArgumentNullException>());
            }

            [Test]
            public void ThrowAnArgumentNullException_WhenTheConnectionStringIsAWhitespaceString()
            {
                Assert.That(() => new ConnectionString("    "), Throws.InstanceOf<ArgumentNullException>());
            }

            [Test]
            public void SetTheDatabaseProperty_WhenTheConnectionStringDefinesADatabase()
            {
                var connectionString = new ConnectionString("Database=foo");
                
                Assert.That(connectionString.Database, Is.EqualTo("foo"));
            }
        }
    }
}
