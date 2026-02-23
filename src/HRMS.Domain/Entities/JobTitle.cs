using System;
using HRMS.Domain.Common;

namespace HRMS.Domain.Entities
{
    public class JobTitle
    {
        public int Id{ get; set; }
        public string Name { get; set; } 
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public JobTitle() { }
        public JobTitle(string name, string? description)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
                IsActive = true;
            }
            if(!string.IsNullOrWhiteSpace(description)) Description = description;
        }
    }
}
