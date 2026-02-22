using HRMS.Application.Interfaces.Repositories;
using HRMS.Infrastructure.Repositories.Mapping;
using HRMS.Domain.Entities;
using HRMS.Infrastructure.Database;
namespace HRMS.Infrastructure.Repositories.Ado
{
    public class DepartmentRepositoryADO : BaseRepository, IDepartmentRepository
    {
        public DepartmentRepositoryADO(IDbConnectionFactory factory) : base(factory) { }
        public async Task<Department?> GetByIdAsync(int id, CancellationToken token = default)
        {
            const string sql = "SELECT Id, Name, IsActive FROM Departments WHERE Id = @id";
            var parameters = new (string Name, object? Value)[]
            {
                ("@id", id)
            };
            var res = await ExecuteReaderAsync(sql, parameters, DepartmentDataMapper.MapReaderToDepartment, token);
            return res.FirstOrDefault();
        }
        public async Task<Department?> GetByNameAsync(string name, CancellationToken token = default)
        {
            const string sql = "SELECT Id, Name, IsActive FROM Departments WHERE Name=@name";
            var parameters = new (string Name, object? Value)[]
            {
                ("@name", name),
            };
            var res = await ExecuteReaderAsync(sql, parameters, DepartmentDataMapper.MapReaderToDepartment, token);
            return res.FirstOrDefault();
        }
        public async Task<IEnumerable<Department>> GetAllAsync(CancellationToken token = default)
        {
            const string sql = "SELECT Id, Name, IsActive FROM Departments";
            var parameters = new (string Name, object? Value)[]
            {

            };
            var result = await ExecuteReaderAsync(sql, parameters, DepartmentDataMapper.MapReaderToDepartment, token);
            return result;
        }
        public async Task<int> CreateAsync(Department department, CancellationToken token = default)
        {
            const string sql = @"INSERT INTO Departments
                                (Name, IsActive) 
                                VALUES
                                (@Name, @IsActive)
                                RETURNING Id;";
            var parameters = new (string Name, object? Value)[]
            {
                ("@Name", department.Name),
                ("@IsActive", department.IsActive)
            };
            var newId = await ExecuteScalarAsync<int>(sql, parameters, token);
            return newId;
        }
        public async Task<bool> ExistsByNameAsync(string name, CancellationToken token = default)
        {
            const string sql = "SELECT 1 FROM Departments WHERE Name = @name";
            var parameters = new (string Name, object? Value)[]
            {
                ("@name", name)
            };
            var result = await ExecuteScalarAsync<int>(sql, parameters, token);
            return result > 0;
        }
        public async Task<bool> ExistsByIdAsync(int id, CancellationToken token = default)
        {
            const string sql = "SELECT 1 FROM Departments WHERE Id = @id";
            var parameters = new (string Name, object? Value)[]
            {
                ("@id", id)
            };
            var result = await ExecuteScalarAsync<int>(sql, parameters, token);
            return result > 0;
        }
        
        public async Task<bool> RenameAsync(int id, string newName, CancellationToken token = default)
        {
            const string sql = @"UPDATE Departments 
                                SET Name=@newName
                                WHERE Id=@id";
            var parameters = new (string Name, object? Value)[]
            {
                ("@newName",newName),
                ("@id", id)
            };
            var res = await ExecuteNonQueryAsync(sql, parameters, token);
            return res > 0;
        }
        public async Task<bool> ActivateAsync(int id, CancellationToken token = default)
        {
            const string sql = "UPDATE Departments SET IsActive = true WHERE Id=@id";
            var parameters = new (string Name, object? Value)[]
            {
                ("@id", id)
            };
            var res = await ExecuteNonQueryAsync(sql, parameters, token);
            return res > 0;
        }
        public async Task<bool> DeactivateAsync(int id, CancellationToken token = default)
        {
            const string sql = "UPDATE Departments SET IsActive = false WHERE Id=@id";
            var parameters = new (string Name, object? Value)[]
            {
                ("@id", id)
            };
            var res = await ExecuteNonQueryAsync(sql, parameters, token);
            return res > 0;
        }
    }
}