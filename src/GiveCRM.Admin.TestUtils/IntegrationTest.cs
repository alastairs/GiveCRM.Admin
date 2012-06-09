namespace GiveCRM.Admin.TestUtils
{
    using GiveCRM.Admin.DataAccess;
    using NUnit.Framework;

    public class IntegrationTest
    {
        private readonly IDatabaseProvider databaseConnectionProvider;

        public IntegrationTest()
        {
            this.databaseConnectionProvider = new SimpleDataFileDatabaseProvider("IntegrationTestDB.sdf");
        }

        [SetUp]
        public void SetUp()
        {
            this.TestSetup();
        }

        [TearDown]
        public void TearDown()
        {
            
        }

        protected virtual void TestSetup()
        {
            this.ClearTables();
        }

        protected IDatabaseProvider DatabaseConnectionProvider
        {
            get { return this.databaseConnectionProvider; }
        }

        private void ClearTables()
        {
            var db = this.databaseConnectionProvider.GetDatabase();

            db.aspnet_Membership.DeleteAll();
            db.aspnet_Users.DeleteAll();
            db.aspnet_Applications.DeleteAll();

            db.Charity.DeleteAll();
            db.CharityMembership.DeleteAll();
        }
    }
}