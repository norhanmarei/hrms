using System;
using HRMS.Domain.Common;
using HRMS.Domain.Enums;
namespace HRMS.Domain.Entities
{
    public class Employee: AuditableEntity
    {
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int JobTitleId { get; set; }
        public JobTitle? JobTitle { get; set; }
        public int WorkScheduleId { get; set; }
        public WorkSchedule? WorkSchedule { get; set; }
        public decimal Salary { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string? EmployeeNumber { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();
        public EmploymentType EmploymentType { get; set; }
    }
}