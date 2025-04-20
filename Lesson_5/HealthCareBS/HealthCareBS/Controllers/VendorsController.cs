using Microsoft.AspNetCore.Mvc;
using HealthCareBL.Models;
using HealthCareBL.Services.Interfaces;
using DAL_Core.Enums;

namespace HealthCareBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendors = await _vendorService.GetAllVendors();
            return Ok(vendors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var vendor = await _vendorService.GetVendorById(id);
                return Ok(vendor);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VendorDto dto)
        {
            try
            {
                var id = await _vendorService.AddVendor(dto);
                return CreatedAtAction(nameof(GetById), new { id }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] VendorDto dto)
        {
            try
            {
                await _vendorService.UpdateVendor(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _vendorService.DeleteVendor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("contract/{type}")]
        public async Task<IActionResult> GetByContractType(ContractType type)
        {
            var vendors = await _vendorService.GetVendorsByContractType(type);
            return Ok(vendors);
        }

        [HttpGet("{vendorId}/healthcare")]
        public async Task<IActionResult> GetVendorHealthcare(Guid vendorId)
        {
            var records = await _vendorService.GetVendorHealthCareRecords(vendorId);
            return Ok(records);
        }
    }
}