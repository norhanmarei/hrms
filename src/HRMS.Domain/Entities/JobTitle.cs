using System;
using HRMS.Domain.Common;

namespace HRMS.Domain.Entities
{
    public class JobTitle : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } 
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
