namespace GiveCRM.Admin.TestUtils
{
    using System;
    using GiveCRM.Admin.DataAccess;
    using Simple.Data;

    public class SimpleDataFileDatabaseProvider : IDatabaseProvider
    {
        private readonly string databaseFilePath;

        public SimpleDataFileDatabaseProvider(string databaseFilePath)
        {
            if (string.IsNullOrWhiteSpace(databaseFilePath))
            {
                throw new ArgumentNullException("databaseFilePath");
            }

            this.databaseFilePath = databaseFilePath;
        }

        public dynamic GetDatabase()
        {
            return Database.OpenFile(databaseFilePath);
        }
    }
}
