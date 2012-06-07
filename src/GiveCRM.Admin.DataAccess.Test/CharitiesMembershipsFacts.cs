using System.Linq;
using GiveCRM.Admin.Models;
using NUnit.Framework;

namespace GiveCRM.Admin.DataAccess.Test
{
    using GiveCRM.Admin.TestUtils;

    [TestFixture]
    public class CharitiesMembershipsFacts
    {
        private const string DatabaseFilePath = "TestDB.sdf";
        
        [TestFixture]
        public class GetAllShould
        {
            private readonly IDatabaseProvider db = new SimpleDataFileDatabaseProvider(DatabaseFilePath);

            [SetUp]
            public void Setup()
            {
                dynamic dbConnection = db.GetDatabase();
                dbConnection.CharityMembership.DeleteAll();
            }

            [Test]
            public void ReturnAnEmptyCollection_WhenThereAreNoCharityMembershipsDefined()
            {
                var allCharitiesMemberships = new CharitiesMemberships(db).GetAll();
                Assert.That(allCharitiesMemberships, Is.Empty);
            }

            [Test]
            public void ReturnACollectionOfItems_WhenThereAreCharityMembershipsDefined()
            {
                var charityMembership = new CharityMembership
                                            {
                                                CharityId = 1,
                                                UserName = "test"
                                            };
                var charitiesMemberships = new CharitiesMemberships(db);
                charitiesMemberships.Save(charityMembership);

                var allCharitiesMemberships = charitiesMemberships.GetAll().ToList();
                Assert.That(allCharitiesMemberships.Single().CharityId, Is.EqualTo(charityMembership.CharityId));
                Assert.That(allCharitiesMemberships.Single().UserName, Is.EqualTo(charityMembership.UserName));
            }
        }

        [TestFixture]
        public class GetByIdShould
        {
            private readonly IDatabaseProvider db = new SimpleDataFileDatabaseProvider(DatabaseFilePath);

            [SetUp]
            public void Setup()
            {
                dynamic dbConnection = db.GetDatabase();
                dbConnection.CharityMembership.DeleteAll();
            }

            [Test]
            public void ReturnNull_WhenNoCharityMembershipExistsWithTheGivenId()
            {
                var charitiesMemberships = new CharitiesMemberships(db);
                var nonExistent = charitiesMemberships.GetById(4096);

                Assert.That(nonExistent, Is.Null);
            }

            [Test]
            public void ReturnTheCharityMembershipWithTheSpecifiedId_WhenACharityMembershipExistsWithTheGivenId()
            {
                var charityMembership = new CharityMembership
                                            {
                                                CharityId = 35,
                                                UserName = "test"
                                            };
                var charitiesMemberships = new CharitiesMemberships(db);

                var createdCharityMembership = charitiesMemberships.Save(charityMembership);
                int createdId = createdCharityMembership.Id;

                var foundCharityMembership = charitiesMemberships.GetById(createdId);
                Assert.That(foundCharityMembership.Id, Is.EqualTo(createdId));
            }
        }

        [TestFixture]
        public class SaveShould
        {
            private readonly IDatabaseProvider db = new SimpleDataFileDatabaseProvider(DatabaseFilePath);

            [SetUp]
            public void Setup()
            {
                dynamic dbConnection = db.GetDatabase();
                dbConnection.CharityMembership.DeleteAll();
            }

            [Test]
            public void ReturnTheCreatedCharityMembership_WhenTheSaveIsSuccessful()
            {
                var charityMembership = new CharityMembership
                                            {
                                                CharityId = 30,
                                                UserName = "test"
                                            };
                var charitiesMemberships = new CharitiesMemberships(db);
                
                var createdCharityMembership = charitiesMemberships.Save(charityMembership);
                
                Assert.That(createdCharityMembership.CharityId, Is.EqualTo(charityMembership.CharityId));
                Assert.That(createdCharityMembership.UserName, Is.EqualTo(charityMembership.UserName));
            }
        }

        [TestFixture]
        public class DeleteShould
        {
            private readonly IDatabaseProvider db = new SimpleDataFileDatabaseProvider(DatabaseFilePath);
            private CharityMembership charityMembership;
            
            [SetUp]
            public void SetUp()
            {
                dynamic dbConnection = db.GetDatabase();
                dbConnection.CharityMembership.DeleteAll(); 
                
                charityMembership = dbConnection.CharityMembership.Insert(new CharityMembership
                {
                    CharityId = 58,
                    UserName = "test"
                });
            }

            [Test]
            public void ReturnTrue_WhenTheDeletionIsSuccessful()
            {
                var charitiesMemberships = new CharitiesMemberships(db);
                Assert.That(charitiesMemberships.Delete(charityMembership), Is.True);
            }

            [Test]
            public void ReturnFalse_WhenTheDeletionFails()
            {
                var nonExistentCharityMembership = new CharityMembership
                                                       {
                                                           Id = 4096,
                                                           CharityId = 9612,
                                                           UserName = "test"
                                                       };
                var charitiesMemberships = new CharitiesMemberships(db);

                Assert.That(charitiesMemberships.Delete(nonExistentCharityMembership), Is.False);
            }
        }

        [TestFixture]
        public class DeleteByIdShould
        {
            private readonly IDatabaseProvider db = new SimpleDataFileDatabaseProvider(DatabaseFilePath);
            private CharityMembership charityMembership;

            [SetUp]
            public void SetUp()
            {
                dynamic dbConnection = db.GetDatabase();
                dbConnection.CharityMembership.DeleteAll();

                charityMembership = dbConnection.CharityMembership.Insert(new CharityMembership
                {
                    CharityId = 58,
                    UserName = "test"
                });
            }

            [Test]
            public void ReturnTrue_WhenTheDeletionIsSuccessful()
            {
                var charitiesMemberships = new CharitiesMemberships(db);
                Assert.That(charitiesMemberships.DeleteById(charityMembership.Id), Is.True);
            }

            [Test]
            public void ReturnFalse_WhenTheDeletionFails()
            {
                var charitiesMemberships = new CharitiesMemberships(db);
                Assert.That(charitiesMemberships.DeleteById(68), Is.False);
            }
        }
    }
}
