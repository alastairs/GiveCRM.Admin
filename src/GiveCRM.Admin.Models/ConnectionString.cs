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

            var equalsPosition = connectionString.IndexOf("=");
            string parameterName = connectionString.Substring(0, equalsPosition);

            var parameterValue = connectionString.Substring(equalsPosition + 1);
            if (parameterName == "Data Source")
            {
                this.host = parameterValue;
            }

            this.database = parameterValue;
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
