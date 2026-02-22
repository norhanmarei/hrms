using System.ComponentModel.DataAnnotations;

namespace HRMS.Application.DTOs.V1.Department
{
    public class RenameDepartmentDto
    {
        [Required]
        public int Id { get; set;}
        [Required]
         [StringLength(100, MinimumLength = 2)]
        public string NewName { get; set; }
    }
}