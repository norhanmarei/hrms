using HRMS.Application.Interfaces.Services;
using HRMS.Application.DTOs.V1.JobTitle;
using HRMS.Application.Enums;
using HRMS.Application.Interfaces.Repositories;
using HRMS.Domain.Entities;
namespace HRMS.Application.Services
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository _repo;
        public JobTitleService(IJobTitleRepository repo) => _repo = repo;
        public async Task<(JobTitleActionResult result, JobTitleDto? jobDto)> CreateAsync(CreateJobTitleDto dto, CancellationToken token = default)
        {
            if (await _repo.ExistsByNameAsync(dto.Name))
                return (JobTitleActionResult.NameAlreadyExists, null);
            var job = new JobTitle(dto.Name, dto.Description);
            var res = await _repo.CreateAsync(job, token);
            var jobDto = new JobTitleDto
            {
                Id = res,
                Name = dto.Name,
                Description = dto.Description,
                IsActive = job.IsActive
            };          
            return (JobTitleActionResult.Success, jobDto);
        }
    }
}