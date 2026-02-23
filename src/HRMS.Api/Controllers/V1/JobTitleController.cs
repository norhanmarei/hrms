using HRMS.Application.DTOs.V1.JobTitle;
using HRMS.Application.Enums;
using HRMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/jobtitles")]
    public class JobTitleController : ControllerBase
    {
        private readonly IJobTitleService _service;
        public JobTitleController(IJobTitleService service) => _service = service;
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JobTitleDto>> CreateAsync(CreateJobTitleDto dto, CancellationToken token = default)
        {
            var (status, job) = await _service.CreateAsync(dto, token);
            return status switch
            {
                JobTitleActionResult.NameAlreadyExists => Conflict("Job Title Already Exists."),
                JobTitleActionResult.Success => Ok(job),
                _ => BadRequest("Job Title Creation Failed.")
            };
        }
    }
}