using HRMS.Application.DTOs.V1.Department;
using HRMS.Domain.Entities;

namespace HRMS.Application.Mapping
{
    public static class DepartmentMapping
    {
        public static DepartmentDto? ToDto(Department department)
        {
            if (department == null) return null;
            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                IsActive =department.IsActive
            };
        }
    }
}