using System;
using HRMS.Domain.Common;
using HRMS.Domain.Enums;
namespace HRMS.Domain.Entities
{
    public class WorkScheduleDay: AuditableEntity
    {
        public int ScheduleId { get; set; }
        public WorkSchedule? Schedule { get; set; }
        public HRMS.Domain.Enums.DayOfWeek DayOfWeek { get; set; }
        public bool IsWorkingDay { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public byte BreakMinutes { get; set; }
    }
}