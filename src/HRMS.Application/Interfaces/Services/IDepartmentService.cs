using HRMS.Application.DTOs.V1.Department;
using HRMS.Application.Enums;

namespace HRMS.Application.Interfaces.Services
{
    public interface IDepartmentService
    {
        Task<DepartmentDto?> GetByNameAsync(string name, CancellationToken token = default);
        Task<DepartmentDto?> GetByIdAsync(int id, CancellationToken token = default);
        Task<IEnumerable<DepartmentDto>> GetAllAsync(CancellationToken token = default);
        
        Task<(DepartmentActionResult result,DepartmentDto? department)> CreateAsync(CreateDepartmentDto dto, CancellationToken token = default);
        Task<DepartmentActionResult> RenameAsync(RenameDepartmentDto dto, CancellationToken token = default);
        Task<DepartmentActionResult> ActivateAsync(int id, CancellationToken token = default);
        Task<DepartmentActionResult> DeactivateAsync(int id, CancellationToken token = default);
    }
}