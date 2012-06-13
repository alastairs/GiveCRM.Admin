namespace GiveCRM.Admin.Models
{
    using System;

    public class ConnectionString
    {
        private readonly string database;
        private readonly string host;

        public ConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }

            string parameterName = connectionString.Substring(0, connectionString.IndexOf("="));

            if (parameterName == "Data Source")
            {
                this.host = connectionString.Substring(connectionString.IndexOf("=") + 1);
            }

            this.database = connectionString.Substring(connectionString.IndexOf("=") + 1);
        }

        public string Database
        {
            get { return this.database; }
        }

        public string Host
        {
            get { return this.host; }
        }

        public bool TrustedConnection { get; set; }
    }
}
