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
            else if (parameterName == "Trusted Connection")
            {
                bool parsedValue;
                if (Boolean.TryParse(parameterValue, out parsedValue))
                {
                    this.TrustedConnection = parsedValue;
                }
                else
                {
                    throw new ArgumentException(
                        string.Format("Unsupported value for '{0}' parameter: '{1}'. Supported values are: {2}", parameterName, parameterValue,
                                      string.Join(", ", new[] { "True", "False" })));
                }
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
