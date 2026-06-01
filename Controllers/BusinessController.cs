using BusinessManagement.DTOs;
using BusinessManagement.Supervisors;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BusinessController : ControllerBase
    {
        private readonly IBusinessSupervisor _businessSupervisor;

        public BusinessController(IBusinessSupervisor businessSupervisor)
        {
            _businessSupervisor = businessSupervisor;
        }

        // ─── POST api/business ───────────────────────────────────────────────────
        /// <summary>Add a new business</summary>
        [HttpPost]
        public async Task<IActionResult> AddBusiness([FromBody] AddBusinessRequest request)
        {
            var response = await _businessSupervisor.AddBusinessAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // ─── GET api/business ────────────────────────────────────────────────────
        /// <summary>Get all active businesses</summary>
        [HttpGet]
        public async Task<IActionResult> GetAllBusinesses()
        {
            var response = await _businessSupervisor.GetAllBusinessesAsync();
            return Ok(response);
        }

        // ─── GET api/business/{businessId} ───────────────────────────────────────
        /// <summary>Get a specific business by its ID</summary>
        [HttpGet("{businessId}")]
        public async Task<IActionResult> GetBusinessById([FromRoute] string businessId)
        {
            var response = await _businessSupervisor.GetBusinessByIdAsync(businessId);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // ─── PUT api/business/{businessId} ───────────────────────────────────────
        /// <summary>Edit an existing business</summary>
        [HttpPut("{businessId}")]
        public async Task<IActionResult> EditBusiness(
            [FromRoute] string businessId,
            [FromBody] EditBusinessRequest request)
        {
            var response = await _businessSupervisor.EditBusinessAsync(businessId, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // ─── DELETE api/business/{businessId} ────────────────────────────────────
        /// <summary>Soft-delete a business</summary>
        [HttpDelete("{businessId}")]
        public async Task<IActionResult> DeleteBusiness([FromRoute] string businessId)
        {
            var response = await _businessSupervisor.DeleteBusinessAsync(businessId);
            return response.Success ? Ok(response) : NotFound(response);
        }
    }
}
