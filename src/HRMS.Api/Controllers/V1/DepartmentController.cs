using System.ComponentModel;
using HRMS.Application.DTOs.V1.Department;
using HRMS.Application.Enums;
using HRMS.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRMS.Api.Controllers.V1
{
    [ApiController]
    [Route("api/v1/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService service) => _service = service;
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentDto>> GetByIdAsync(int id, CancellationToken token = default)
        {
            if (id < 1) return BadRequest($"Id Must Be Greater Than 0.");
            var dto = await _service.GetByIdAsync(id, token);
            if (dto == null) return NotFound($"Department With Id [{id}] Is Not Found.");
            return Ok(dto);
        }


        [HttpGet("{name:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartmentDto>> GetByNameAsync(string name, CancellationToken token = default)
        {
            if (string.IsNullOrEmpty(name)) return BadRequest("Name Should Not Be Null Or Empty.");
            var dto = await _service.GetByNameAsync(name, token);
            if (dto == null) return NotFound($"Department With Name : [{name}] Is Not Found.");
            return Ok(dto);
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<DepartmentDto>>> GetAllAsync(CancellationToken token = default)
        {
            var list = await _service.GetAllAsync(token);
            if (!list.Any()) return NotFound("No Departments Were Found.");
            return Ok(list);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DepartmentDto>> CreateAsync(CreateDepartmentDto dto, CancellationToken token = default)
        {
            var (res, dept) = await _service.CreateAsync(dto, token);
            return res switch
            {
                DepartmentActionResult.NameAlreadyExists => Conflict("Department Name Must Be Unique."),
                DepartmentActionResult.Success => Ok(dept),
                _ => BadRequest("Failed To Create Department.")
            };
        }


        [HttpPatch("rename")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RenameAsync(RenameDepartmentDto dto, CancellationToken token = default)
        {
            if (dto.Id < 1) return BadRequest("Id Must Be Greater Than 0.");
            var result = await _service.RenameAsync(dto, token);
            return result switch
            {
                DepartmentActionResult.NotFound => NotFound($"Department With Id [{dto.Id}] Was Not Found."),
                DepartmentActionResult.NameAlreadyExists => Conflict("Department Name Must Be Unique."),
                DepartmentActionResult.Failure => BadRequest("Rename Failed."),
                DepartmentActionResult.Success => Ok("Rename Succeeded."),
                _ => BadRequest("Rename Failed.")
            };
        }

        [HttpPatch("activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ActivateAsync(int id, CancellationToken token = default)
        {
            if (id < 1) return BadRequest("Id Must Be Greater Than 0.");
            var res = await _service.ActivateAsync(id, token);
            return res switch
            {
                DepartmentActionResult.NotFound => NotFound("Department Not Found."),
                DepartmentActionResult.AlreadyActive => BadRequest("Department Is Already Active."),
                DepartmentActionResult.Success => Ok("Department Is Now Active."),
                _ => BadRequest("Activation Failed.")
            };
        }
    
    
        [HttpPatch("deactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeactivateAsync(int id, CancellationToken token = default)
        {
            if (id < 1) return BadRequest("Id Must Be Greater Than 0.");
            var res = await _service.DeactivateAsync(id, token);
            return res switch
            {
                DepartmentActionResult.NotFound => NotFound("Department Not Found."),
                DepartmentActionResult.AlreadyNotActive => BadRequest("Department Is Already Deactivated."),
                DepartmentActionResult.Success => Ok("Department Was Deactivated Successfuly."),
                _ => BadRequest("Deactivation Failed.")
            };
        }
    }
}