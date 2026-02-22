using System;
using HRMS.Domain.Common;
namespace HRMS.Domain.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } 
        public bool IsActive { get; set; }
        public Department() { }
        public Department(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name.Trim();
                IsActive = true;
            }
        }
    }
}