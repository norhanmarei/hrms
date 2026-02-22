using HRMS.Domain.Entities;
namespace HRMS.Application.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetByIdAsync(int id, CancellationToken token = default);
        Task<Department?> GetByNameAsync(string name, CancellationToken token = default);
        Task<bool> ExistsByNameAsync(string name, CancellationToken token = default);
        Task<bool> ExistsByIdAsync(int id, CancellationToken token = default);
        Task<IEnumerable<Department>> GetAllAsync(CancellationToken token = default);
        Task<int> CreateAsync(Department department, CancellationToken token = default);
        //Task SaveAsync(Department department, CancellationToken token = default); 
        Task<bool> RenameAsync(int id, string newName, CancellationToken token = default);
        Task<bool> ActivateAsync(int id, CancellationToken token = default);
        Task<bool> DeactivateAsync(int id, CancellationToken token = default);
    }
}