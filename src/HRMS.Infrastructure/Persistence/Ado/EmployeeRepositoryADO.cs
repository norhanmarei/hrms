using System;
using System.Collections.Generic;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities;
using Npgsql;

namespace HRMS.Infrastructure.Repositories
{
    public class EmployeeRepositoryADO : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }

public async Task<Employee?> GetByIdAsync(int id)
{
    const string query = @"
SELECT
    e.Id AS EmployeeId,
    e.PersonId,
    e.DepartmentId,
    e.JobTitleId,
    e.WorkScheduleId,
    e.Salary,
    e.StartDate,
    e.EndDate,
    e.IsActive,
    e.EmployeeNumber,
    e.ManagerId,
    e.EmploymentType,
    p.Id AS PersonId,
    p.FirstName,
    p.SecondName,
    p.ThirdName,
    p.LastName,
    p.Email,
    p.PhoneNumber
FROM employees e
INNER JOIN people p ON e.PersonId = p.Id
WHERE e.Id = @Id AND e.IsDeleted = false;";

    await using var conn = new NpgsqlConnection(_connectionString);
    await using var cmd = new NpgsqlCommand(query, conn);

    cmd.Parameters.AddWithValue("@Id", id);

    await conn.OpenAsync();
    await using var reader = await cmd.ExecuteReaderAsync();

    if (!await reader.ReadAsync())
        return null;

    return _MapReaderToEmployee(reader);
}


        public async Task<IEnumerable<Employee>> GetAllAsync()
{
    var employees = new List<Employee>();
    const string query = @"
SELECT
    e.Id AS EmployeeId,
    e.PersonId,
    e.DepartmentId,
    e.JobTitleId,
    e.WorkScheduleId,
    e.Salary,
    e.StartDate,
    e.EndDate,
    e.IsActive,
    e.EmployeeNumber,
    e.ManagerId,
    e.EmploymentType,
    p.Id AS PersonId,
    p.FirstName,
    p.SecondName,
    p.ThirdName,
    p.LastName,
    p.Email,
    p.PhoneNumber
FROM employees e
INNER JOIN people p ON e.PersonId = p.Id
WHERE e.IsDeleted = false;";

    await using var conn = new NpgsqlConnection(_connectionString);
    await using var cmd = new NpgsqlCommand(query, conn);

    await conn.OpenAsync();
    await using var reader = await cmd.ExecuteReaderAsync();

    while (await reader.ReadAsync())
    {
        employees.Add(_MapReaderToEmployee(reader));
    }

    return employees;
}

public async Task<int> AddAsync(Employee employee)
{
    const string query = @"
INSERT INTO Employees 
(PersonId, DepartmentId, JobTitleId, WorkScheduleId, Salary, StartDate, EndDate, IsActive, EmployeeNumber, ManagerId, EmploymentType)
VALUES
(@PersonId, @DepartmentId, @JobTitleId, @WorkScheduleId, @Salary, @StartDate, @EndDate, @IsActive, @EmployeeNumber, @ManagerId, @EmploymentType)
RETURNING Id;";

    await using var conn = new NpgsqlConnection(_connectionString);
    await using var cmd = new NpgsqlCommand(query, conn);

    cmd.Parameters.AddWithValue("@PersonId", employee.PersonId);
    cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
    cmd.Parameters.AddWithValue("@JobTitleId", employee.JobTitleId);
    cmd.Parameters.AddWithValue("@WorkScheduleId", employee.WorkScheduleId);
    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate.ToDateTime(TimeOnly.MinValue));
    cmd.Parameters.AddWithValue("@EndDate", employee.EndDate?.ToDateTime(TimeOnly.MinValue) ?? (object)DBNull.Value);
    cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
    cmd.Parameters.AddWithValue("@EmployeeNumber", employee.EmployeeNumber ?? (object)DBNull.Value);
    cmd.Parameters.AddWithValue("@ManagerId", employee.ManagerId ?? (object)DBNull.Value);
    cmd.Parameters.AddWithValue("@EmploymentType", (int)employee.EmploymentType);

    await conn.OpenAsync();
    var result = await cmd.ExecuteScalarAsync();
    return Convert.ToInt32(result);
}

public async Task<bool> UpdateAsync(Employee employee)
{
    const string query = @"
UPDATE Employees
SET DepartmentId = @DepartmentId,
    JobTitleId = @JobTitleId,
    WorkScheduleId = @WorkScheduleId,
    Salary = @Salary,
    StartDate = @StartDate,
    EndDate = @EndDate,
    IsActive = @IsActive,
    EmployeeNumber = @EmployeeNumber,
    ManagerId = @ManagerId,
    EmploymentType = @EmploymentType
WHERE Id = @EmployeeId AND IsDeleted = false;";

    await using var conn = new NpgsqlConnection(_connectionString);
    await using var cmd = new NpgsqlCommand(query, conn);

    cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
    cmd.Parameters.AddWithValue("@JobTitleId", employee.JobTitleId);
    cmd.Parameters.AddWithValue("@WorkScheduleId", employee.WorkScheduleId);
    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate.ToDateTime(TimeOnly.MinValue));
    cmd.Parameters.AddWithValue("@EndDate", employee.EndDate?.ToDateTime(TimeOnly.MinValue) ?? (object)DBNull.Value);
    cmd.Parameters.AddWithValue("@IsActive", employee.IsActive);
    cmd.Parameters.AddWithValue("@EmployeeNumber", employee.EmployeeNumber ?? (object)DBNull.Value);
    cmd.Parameters.AddWithValue("@ManagerId", employee.ManagerId ?? (object)DBNull.Value);
    cmd.Parameters.AddWithValue("@EmploymentType", (int)employee.EmploymentType);
    cmd.Parameters.AddWithValue("@EmployeeId", employee.Id);

    await conn.OpenAsync();
    var rowsAffected = await cmd.ExecuteNonQueryAsync();
    return rowsAffected > 0;
}

public async Task<bool> DeleteAsync(int id)
{
    const string query = @"UPDATE Employees SET IsDeleted = true WHERE Id = @Id;";

    await using var conn = new NpgsqlConnection(_connectionString);
    await using var cmd = new NpgsqlCommand(query, conn);

    cmd.Parameters.AddWithValue("@Id", id);

    await conn.OpenAsync();
    var rowsAffected = await cmd.ExecuteNonQueryAsync();
    return rowsAffected > 0;
}


        private static Employee _MapReaderToEmployee(NpgsqlDataReader reader)
        {
            return new Employee
            {
                Id = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                PersonId = reader.GetInt32(reader.GetOrdinal("PersonId")),
                Person = new Person
                {
                    Id = reader.GetInt32(reader.GetOrdinal("PersonId")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    SecondName = reader.GetString(reader.GetOrdinal("SecondName")),
                    ThirdName = reader.IsDBNull(reader.GetOrdinal("ThirdName")) ? null : reader.GetString(reader.GetOrdinal("ThirdName")),
                    LastName = reader.GetString(reader.GetOrdinal("LastName")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"))
                },
                DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                JobTitleId = reader.GetInt32(reader.GetOrdinal("JobTitleId")),
                WorkScheduleId = reader.GetInt32(reader.GetOrdinal("WorkScheduleId")),
                Salary = reader.GetDecimal(reader.GetOrdinal("Salary")),
                StartDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("StartDate"))),
                EndDate = reader.IsDBNull(reader.GetOrdinal("EndDate")) ? null : DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("EndDate"))),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                EmployeeNumber = reader.IsDBNull(reader.GetOrdinal("EmployeeNumber")) ? string.Empty : reader.GetString(reader.GetOrdinal("EmployeeNumber")),
                ManagerId = reader.IsDBNull(reader.GetOrdinal("ManagerId")) ? null : reader.GetInt32(reader.GetOrdinal("ManagerId")),
                EmploymentType = (Domain.Enums.EmploymentType)reader.GetInt32(reader.GetOrdinal("EmploymentType"))
            };
        }
    }
}
