using BusinessManagement.Models;

namespace BusinessManagement.Repositories
{
    public interface IBusinessRepository
    {
        Task<Business> InsertBusinessAsync(Business business);
        Task<List<Business>> FetchAllBusinessesAsync();
        Task<Business?> FetchBusinessByIdAsync(string businessId);
        Task<bool> UpdateBusinessAsync(string businessId, Business updatedBusiness);
        Task<bool> SoftDeleteBusinessAsync(string businessId);
        Task<bool> BusinessExistsAsync(string businessId);
    }
}
