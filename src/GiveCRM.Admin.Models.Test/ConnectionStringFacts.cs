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

            [Test]
            public void SetTheHostProperty_WhenTheConnectionStringDefinesADataSource()
            {
                var connectionString = new ConnectionString("Data Source=foo");

                Assert.That(connectionString.Host, Is.EqualTo("foo"));
            }

            [Test]
            public void ParseAFullConnectionString()
            {
                var connectionString = new ConnectionString("Data Source=foo;Initial Catalog=bar;Integrated Security=SSPI;");

                Assert.That(connectionString.Host, Is.EqualTo("foo"));
                Assert.That(connectionString.Database, Is.EqualTo("bar"));
                Assert.That(connectionString.TrustedConnection, Is.True);
            }
        }
    }
}
