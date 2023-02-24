using Microsoft.Data.Sqlite;

namespace X.UnitTest.Infrastructures
{
    public class SQLLiteDBConnections
    {
        public readonly SqliteConnection xMemoryDbConnection = new SqliteConnection("Filename=:memory:");

        public void Dispose()
        {
            xMemoryDbConnection.Dispose();
        }
    }
}