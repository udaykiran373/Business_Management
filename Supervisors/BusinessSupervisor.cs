using BusinessManagement.DTOs;
using BusinessManagement.Models;
using BusinessManagement.Repositories;

namespace BusinessManagement.Supervisors
{
    public class BusinessSupervisor : IBusinessSupervisor
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessSupervisor(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        // Add Business 

        public async Task<ApiResponse<BusinessResponse>> AddBusinessAsync(AddBusinessRequest request)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(request.BusinessName))
                return ApiResponse<BusinessResponse>.Fail("Business name is required.");

            if (string.IsNullOrWhiteSpace(request.CreatorId))
                return ApiResponse<BusinessResponse>.Fail("Creator ID is required.");

            if (string.IsNullOrWhiteSpace(request.CreatorName))
                return ApiResponse<BusinessResponse>.Fail("Creator name is required.");

            // Build the document
            var newBusiness = new Business
            {
                BusinessId = Guid.NewGuid().ToString(),
                BusinessName = request.BusinessName.Trim(),
                CreatorId = request.CreatorId.Trim(),
                CreatorName = request.CreatorName.Trim(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            var insertedBusiness = await _businessRepository.InsertBusinessAsync(newBusiness);
            return ApiResponse<BusinessResponse>.Ok(MapToResponse(insertedBusiness), "Business added successfully.");
        }

        // Get All Businesses 

        public async Task<ApiResponse<List<BusinessResponse>>> GetAllBusinessesAsync()
        {
            var businesses = await _businessRepository.FetchAllBusinessesAsync();

            if (!businesses.Any())
                return ApiResponse<List<BusinessResponse>>.Ok(new List<BusinessResponse>(), "No businesses found.");

            var responseList = businesses.Select(MapToResponse).ToList();
            return ApiResponse<List<BusinessResponse>>.Ok(responseList, $"{responseList.Count} business(es) retrieved successfully.");
        }

        // Get Business By Id 

        public async Task<ApiResponse<BusinessResponse>> GetBusinessByIdAsync(string businessId)
        {
            if (string.IsNullOrWhiteSpace(businessId))
                return ApiResponse<BusinessResponse>.Fail("Business ID is required.");

            var business = await _businessRepository.FetchBusinessByIdAsync(businessId);

            if (business is null)
                return ApiResponse<BusinessResponse>.Fail("Business not found.");

            return ApiResponse<BusinessResponse>.Ok(MapToResponse(business), "Business retrieved successfully.");
        }

        // Edit Business 
        public async Task<ApiResponse<BusinessResponse>> EditBusinessAsync(string businessId, EditBusinessRequest request)
        {
            if (string.IsNullOrWhiteSpace(businessId))
                return ApiResponse<BusinessResponse>.Fail("Business ID is required.");

            if (string.IsNullOrWhiteSpace(request.BusinessName))
                return ApiResponse<BusinessResponse>.Fail("Business name is required.");

            if (string.IsNullOrWhiteSpace(request.CreatorId))
                return ApiResponse<BusinessResponse>.Fail("Creator ID is required.");

            if (string.IsNullOrWhiteSpace(request.CreatorName))
                return ApiResponse<BusinessResponse>.Fail("Creator name is required.");

            bool exists = await _businessRepository.BusinessExistsAsync(businessId);
            if (!exists)
                return ApiResponse<BusinessResponse>.Fail("Business not found.");

            var updatedBusiness = new Business
            {
                BusinessName = request.BusinessName.Trim(),
                CreatorId = request.CreatorId.Trim(),
                CreatorName = request.CreatorName.Trim(),
                UpdatedDate = DateTime.UtcNow
            };

            bool isUpdated = await _businessRepository.UpdateBusinessAsync(businessId, updatedBusiness);

            if (!isUpdated)
                return ApiResponse<BusinessResponse>.Fail("Failed to update business. Please try again.");

            // Fetch updated record to return latest state
            var refreshedBusiness = await _businessRepository.FetchBusinessByIdAsync(businessId);
            return ApiResponse<BusinessResponse>.Ok(MapToResponse(refreshedBusiness!), "Business updated successfully.");
        }

        //  Delete Business (Soft Delete)

        public async Task<ApiResponse<string>> DeleteBusinessAsync(string businessId)
        {
            if (string.IsNullOrWhiteSpace(businessId))
                return ApiResponse<string>.Fail("Business ID is required.");

            bool exists = await _businessRepository.BusinessExistsAsync(businessId);
            if (!exists)
                return ApiResponse<string>.Fail("Business not found.");

            bool isDeleted = await _businessRepository.SoftDeleteBusinessAsync(businessId);

            if (!isDeleted)
                return ApiResponse<string>.Fail("Failed to delete business. Please try again.");

            return ApiResponse<string>.Ok(businessId, "Business deleted successfully.");
        }

        // Mapper

        private static BusinessResponse MapToResponse(Business business) =>
            new()
            {
                BusinessId = business.BusinessId,
                BusinessName = business.BusinessName,
                CreatorId = business.CreatorId,
                CreatorName = business.CreatorName,
                CreatedDate = business.CreatedDate,
                UpdatedDate = business.UpdatedDate,
                IsDeleted = business.IsDeleted
            };
    }
}
