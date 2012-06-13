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

            connectionString = connectionString.TrimEnd(';');

            var parameters = connectionString.Split(';');
            foreach (var parameter in parameters)
            {
                var parameterDetails = parameter.Split('=');
                string parameterName = parameterDetails[0];
                var parameterValue = parameterDetails[1];

                if (parameterName == "Data Source")
                {
                    this.host = parameterValue;
                }
                else if (parameterName == "Trusted Connection")
                {
                    this.trustedConnection = this.ParseBool(parameterValue, parameterName);
                }
                else if (parameterName == "Database" || parameterName == "Initial Catalog")
                {
                    this.database = parameterValue;
                } 
                else if (parameterName == "Integrated Security")
                {
                    this.trustedConnection = parameterValue == "SSPI";
                }
            }
        }

        private bool ParseBool(string parameterValue, string parameterName)
        {
            bool parsedValue;
            if (Boolean.TryParse(parameterValue, out parsedValue))
            {
                return parsedValue;
            }
            
            throw new ArgumentException(
                string.Format("Unsupported value for '{0}' parameter: '{1}'. Supported values are: {2}", parameterName, parameterValue,
                              string.Join(", ", new[] { "True", "False" })));
        }

        public string Database
        {
            get { return this.database; }
        }

        public string Host
        {
            get { return this.host; }
        }

        private bool trustedConnection;
        public bool TrustedConnection
        {
            get { return this.trustedConnection; }
        }
    }
}
