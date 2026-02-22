using HRMS.Application.Interfaces.Repositories;
using HRMS.Application.Interfaces.Services;
using HRMS.Application.DTOs.V1.Department;
using HRMS.Domain.Entities;
using HRMS.Application.Enums;
namespace HRMS.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;
        public DepartmentService(IDepartmentRepository repo) => _repo = repo;
        public async Task<DepartmentDto?> GetByIdAsync(int id, CancellationToken token = default)
        {
            var dept = await _repo.GetByIdAsync(id, token);
            if (dept == null) return null;
            var dto = Mapping.DepartmentMapping.ToDto(dept);
            return dto;
        }
        public async Task<DepartmentDto?> GetByNameAsync(string name, CancellationToken token = default)
        {
            var department = await _repo.GetByNameAsync(name, token);
            if (department == null) return null;
            var dto = Mapping.DepartmentMapping.ToDto(department);
            return dto;
        }
        public async Task<IEnumerable<DepartmentDto>> GetAllAsync(CancellationToken token = default)
        {
            var list = await _repo.GetAllAsync(token);
            return list.Select(Mapping.DepartmentMapping.ToDto).ToList();
        }
        public async Task<(DepartmentActionResult result, DepartmentDto? department)> CreateAsync(CreateDepartmentDto dto, CancellationToken token = default)
        {
            if (await _repo.ExistsByNameAsync(dto.Name))
                return (DepartmentActionResult.NameAlreadyExists, null);
            var department = new Department(dto.Name);
            var id = await _repo.CreateAsync(department, token);
            department.Id = id;
            return (DepartmentActionResult.Success ,Mapping.DepartmentMapping.ToDto(department));
        }
        public async Task<DepartmentActionResult> RenameAsync(RenameDepartmentDto dto, CancellationToken token = default)
        {
            if (await _repo.ExistsByNameAsync(dto.NewName)) return DepartmentActionResult.NameAlreadyExists;
            var department = await _repo.GetByIdAsync(dto.Id, token);
            if (department == null) return DepartmentActionResult.NotFound;
            if (await _repo.RenameAsync(dto.Id, dto.NewName, token)) return DepartmentActionResult.Success;
            return DepartmentActionResult.Failure;
        }
        public async Task<DepartmentActionResult> ActivateAsync(int id, CancellationToken token = default)
        {
            var dept = await _repo.GetByIdAsync(id, token);
            if (dept == null) return DepartmentActionResult.NotFound;
            if (dept.IsActive == true) return DepartmentActionResult.AlreadyActive;
            if (await _repo.ActivateAsync(id, token)) return DepartmentActionResult.Success;
            return DepartmentActionResult.Failure;
        }
        public async Task<DepartmentActionResult> DeactivateAsync(int id, CancellationToken token = default)
        {
            var dept = await _repo.GetByIdAsync(id, token);
            if (dept == null) return DepartmentActionResult.NotFound;
            if (dept.IsActive == false) return DepartmentActionResult.AlreadyNotActive;
            if (await _repo.DeactivateAsync(id, token)) return DepartmentActionResult.Success;
            return DepartmentActionResult.Failure;
        }
    }
}