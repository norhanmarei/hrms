using System;
using HRMS.Domain.Entities;
namespace HRMS.Application.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(int Id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<int> AddAsync(Employee employee);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool> DeleteAsync(int Id);
    }
}