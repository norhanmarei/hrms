using System.Data.Common;
namespace HRMS.Infrastructure.Database
{
    public abstract class BaseRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        protected BaseRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;
        protected async Task<int> ExecuteNonQueryAsync(string sql, IEnumerable<(string Name, object? Value)> parameters, CancellationToken token = default)
        {
            await using var conn = await _connectionFactory.CreateConnectionAsync(token);
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            AddParameters(cmd, parameters);
            return await cmd.ExecuteNonQueryAsync(token);
        }
        protected async Task<T> ExecuteScalarAsync<T>(string sql, IEnumerable<(string Name, object? Value)> parameters, CancellationToken token = default)
        {
            await using var conn = await _connectionFactory.CreateConnectionAsync(token);
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            AddParameters(cmd, parameters);
            var result = await cmd.ExecuteScalarAsync(token);
            if (result == DBNull.Value || result is null)
                return default!;

            return (T)Convert.ChangeType(result, typeof(T));
        }
        protected async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string sql, IEnumerable<(string Name, object? Value)> parameters,
            Func<DbDataReader, T> mapFunc,
            CancellationToken token = default)
        {   
            await using var conn = await _connectionFactory.CreateConnectionAsync(token);
            await using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            AddParameters(cmd, parameters);

            var results = new List<T>();
            await using var reader = await cmd.ExecuteReaderAsync(token);
            while (await reader.ReadAsync(token))
            {
                results.Add(mapFunc(reader));
            }
            return results;
        }

        private static void AddParameters(DbCommand cmd, IEnumerable<(string Name, object? Value)> parameters)
        {
            foreach(var (name, value) in parameters)
            {
                var param = cmd.CreateParameter();
                param.ParameterName = name;
                param.Value = value ?? DBNull.Value;
                cmd.Parameters.Add(param);
            }
        }
    }
}