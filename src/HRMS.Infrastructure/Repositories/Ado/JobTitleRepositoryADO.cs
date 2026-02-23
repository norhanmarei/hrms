using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Database;

namespace HRMS.Infrastructure.Repositories.Ado
{
    public class JobTitleRepository : BaseRepository, IJobTitleRepository
    {
        public JobTitleRepository(IDbConnectionFactory factory) : base(factory) { }
         public async Task<bool> ExistsByNameAsync(string name, CancellationToken token = default)
        {
            const string sql = "SELECT 1 FROM JobTitles WHERE Name=@name";
            var parameters = new (string Name, object? Value)[]
            {
                ("@name", name)
            };
            var res = await ExecuteScalarAsync<int>(sql, parameters, token);
            return res > 0;
        }
        public async Task<int> CreateAsync(JobTitle job, CancellationToken token = default)
        {
            const string sql = @"INSERT INTO JobTitles 
                                (Name, Description, IsActive)
                                VALUES
                                (@Name, @Description, @IsActive)
                                RETURNING Id;";
            var parameters = new (string Name, object? Value)[]
            {
                ("@Name", job.Name),
                ("@Description", job.Description),
                ("@IsActive", job.IsActive),
            };
            var res = await ExecuteScalarAsync<int>(sql, parameters, token);
            return res;
        }
    }
}