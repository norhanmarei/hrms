using System;
using HRMS.Domain.Entities;
namespace HRMS.Application.Abstractions.Repositories
{
    public interface IEmployeeRepository
    {
        Employee? GetById(int Id);
        IEnumerable<Employee> GetAll();
        int Add(Employee employee);
        bool Update(Employee employee);
        bool Delete(int Id);
    }
}