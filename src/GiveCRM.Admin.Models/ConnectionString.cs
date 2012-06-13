namespace GiveCRM.Admin.Models
{
    using System;

    public class ConnectionString
    {
        public ConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
        }
    }
}