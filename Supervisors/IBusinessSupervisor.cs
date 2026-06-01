using BusinessManagement.DTOs;

namespace BusinessManagement.Supervisors
{
    public interface IBusinessSupervisor
    {
        Task<ApiResponse<BusinessResponse>> AddBusinessAsync(AddBusinessRequest request);
        Task<ApiResponse<List<BusinessResponse>>> GetAllBusinessesAsync();
        Task<ApiResponse<BusinessResponse>> GetBusinessByIdAsync(string businessId);
        Task<ApiResponse<BusinessResponse>> EditBusinessAsync(string businessId, EditBusinessRequest request);
        Task<ApiResponse<string>> DeleteBusinessAsync(string businessId);
    }
}
