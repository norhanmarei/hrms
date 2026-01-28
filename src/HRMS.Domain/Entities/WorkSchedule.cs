using System;
using HRMS.Domain.Common;
namespace HRMS.Domain.Entities
{
    public class WorkSchedule : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public ICollection<WorkScheduleDay> Days { get; set; } = new List<WorkScheduleDay>();
    }
}