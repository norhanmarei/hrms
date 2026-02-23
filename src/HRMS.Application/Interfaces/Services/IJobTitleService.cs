using HRMS.Application.DTOs.V1.JobTitle;
using HRMS.Application.Enums;

namespace HRMS.Application.Interfaces.Services
{
    public interface IJobTitleService
    {
        Task<(JobTitleActionResult result ,JobTitleDto? jobDto)> CreateAsync(CreateJobTitleDto dto, CancellationToken token = default);
    }
}