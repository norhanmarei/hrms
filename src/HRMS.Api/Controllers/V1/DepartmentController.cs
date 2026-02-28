using System.ComponentModel.DataAnnotations;
using FluentValidation;
using HRMS.Api.Common;
using HRMS.Application.Common;
using HRMS.Application.DTOs.V1.Department;
using HRMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/departments")]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService service) => _service = service;
        [HttpGet("by-id/{id:int}")]
        [ProducesResponseType(typeof(ApiResponse<DepartmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentDto>> GetByIdAsync(int id, CancellationToken token = default)
        {
            var result = await _service.GetByIdAsync(id, token);
            return HandleResult<DepartmentDto>(result);
        }


        [HttpGet("by-name/{name}")]
        [ProducesResponseType(typeof(ApiResponse<DepartmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentDto>> GetByNameAsync(string name, CancellationToken token = default)
        {
            var result = await _service.GetByNameAsync(name, token);
            return HandleResult<DepartmentDto>(result);
        }


    }
}