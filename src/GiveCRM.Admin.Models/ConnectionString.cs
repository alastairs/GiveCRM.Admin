namespace GiveCRM.Admin.Models
{
    using System;

    public class ConnectionString
    {
        private string database;

        public ConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            this.database = connectionString.Substring(connectionString.IndexOf("=") + 1);
        }

        public string Database
        {
            get { return database; }
        }
    }
}