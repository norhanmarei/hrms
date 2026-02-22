using System.Data;
using System.Data.Common;
using Npgsql;

namespace HRMS.Infrastructure.Database
{
    public class PostgresConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        public PostgresConnectionFactory(string ConnectionString) => _connectionString = ConnectionString;
        public async Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default)
        {
            var conn = new NpgsqlConnection(_connectionString);
            await conn.OpenAsync(cancellationToken);
            return conn;
        }
    }
}