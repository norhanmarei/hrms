using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Interfaces.Services;
using HRMS.Application.DTOs.V1.Department;
using HRMS.Application.Common;
using Microsoft.Extensions.Logging;
namespace HRMS.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        private readonly ILogger<DepartmentService> _logger;
        public DepartmentService(IDepartmentRepository repo, ILogger<DepartmentService> logger) 
        {
            _repo = repo;
            _logger = logger;
        }
        public async Task<Result<DepartmentDto>> GetByIdAsync(int id, CancellationToken token = default)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Invalid Department Id {id}. Id Must Be Greater Than 0.", id);
                return Result<DepartmentDto>.Failure(Error.BadRequest("Id Must Be Greater Than 0."));
            } 
            var dept = await _repo.GetByIdAsync(id, token);
            if (dept == null)
            {
                _logger.LogWarning("Department With Id {id} Was Not Found.", id);
                return Result<DepartmentDto>.Failure(Error.NotFound($"Department With Id [{id}] Was Not Found."));   
            }
            var dto = Mapping.DepartmentMapping.ToDto(dept);
            return Result<DepartmentDto>.Success(dto);
        }
        public async Task<Result<DepartmentDto>> GetByNameAsync(string name, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                _logger.LogWarning("Invalid Department Name. Name Must Not Be Null Or Whitespace.");
                return Result<DepartmentDto>.Failure(Error.BadRequest($"Invalid Department Name. Name Must Not Be Null Or Whitespace."));
            }
            name = name.Trim();
            var department = await _repo.GetByNameAsync(name, token);
            if (department == null)
            {
                _logger.LogWarning("Department With Name {name} Was Not Found.", name);
                return Result<DepartmentDto>.Failure(Error.NotFound($"Department With Name {name} Was Not Found."));
            }
            var dto = Mapping.DepartmentMapping.ToDto(department);
            return Result<DepartmentDto>.Success(dto);
        }
       
    }
}