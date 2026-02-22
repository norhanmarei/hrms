using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs.V1.Department
{
    public class CreateDepartmentDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}