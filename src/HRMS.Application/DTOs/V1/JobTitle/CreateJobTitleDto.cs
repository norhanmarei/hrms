using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace HRMS.Application.DTOs.V1.JobTitle
{
    public class CreateJobTitleDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string? Description{ get; set; }
    }
}