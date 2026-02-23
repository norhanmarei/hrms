using HRMS.Domain.Entities;

namespace HRMS.Application.Interfaces.Repositories
{
    public interface IJobTitleRepository
    {
        Task<bool> ExistsByNameAsync(string name, CancellationToken token = default);
        Task<int> CreateAsync(JobTitle job, CancellationToken token = default);
        //Task<bool> RenameAsync(string newName, CancellationToken token = default);
        //Task<bool> ActivateAsync(int id, CancellationToken token = default);
        //Task<bool> DeactivateAsync(int id, CancellationToken token = default);
        // update description
        //Task<IEnumerable<JobTitle>> GetAllAsync(CancellationToken token = default);
    }
}