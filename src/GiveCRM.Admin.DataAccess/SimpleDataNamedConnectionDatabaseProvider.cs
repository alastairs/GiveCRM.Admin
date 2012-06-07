using System;
using Simple.Data;

namespace GiveCRM.Admin.DataAccess
{
    public class SimpleDataNamedConnectionDatabaseProvider : IDatabaseProvider
    {
        private readonly string namedConnection;

        public SimpleDataNamedConnectionDatabaseProvider(string namedConnection)
        {
            if (namedConnection == null)
            {
                throw new ArgumentNullException("namedConnection");
            }

            this.namedConnection = namedConnection;
        }

        public dynamic GetDatabase()
        {
            return Database.OpenNamedConnection(this.namedConnection);
        }
    }
}