using System.Data.Common;

namespace HRMS.Infrastructure.Database
{
    public interface IDbConnectionFactory
    {
        Task<DbConnection> CreateConnectionAsync(CancellationToken cancellationToken = default);
    }
}