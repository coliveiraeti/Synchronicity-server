using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Synchronicity.Server.Data.SQLite
{
    public abstract class Repository
    {
        const string CONNECTION_STRING_NAME = "SQLite";

        protected readonly string connectionString;
        protected SQLiteConnection connection;

        public Repository()
        {
            connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_NAME].ConnectionString;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        protected void OpenConnection()
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();
        }

        protected void CloseConnection()
        {
            if (connection == null || connection.State != ConnectionState.Open)
            {
                return;
            }
            connection.Close();
            connection.Dispose();
        }
    }
}
