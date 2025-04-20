using Microsoft.AspNetCore.Mvc;
using HealthCareBL.Models;
using HealthCareBL.Services.Interfaces;

namespace HealthCareBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthcareController : ControllerBase
    {
        private readonly IHealthCareService _healthCareService;

        public HealthcareController(IHealthCareService healthCareService)
        {
            _healthCareService = healthCareService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _healthCareService.GetAllHealthCareRecords();
            return Ok(records);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var record = await _healthCareService.GetHealthCareById(id);
                return Ok(record);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HealthCareDto dto)
        {
            try
            {
                var id = await _healthCareService.AddHealthCareRecord(dto);
                return CreatedAtAction(nameof(GetById), new { id }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] HealthCareDto dto)
        {
            try
            {
                await _healthCareService.UpdateHealthCareRecord(id, dto);
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
                await _healthCareService.DeleteHealthCareRecord(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("pet/{petId}")]
        public async Task<IActionResult> GetByPet(Guid petId)
        {
            var records = await _healthCareService.GetHealthCareRecordsByPet(petId);
            return Ok(records);
        }

        [HttpGet("vendor/{vendorId}")]
        public async Task<IActionResult> GetByVendor(Guid vendorId)
        {
            var records = await _healthCareService.GetHealthCareRecordsByVendor(vendorId);
            return Ok(records);
        }

        [HttpGet("expiring")]
        public async Task<IActionResult> GetExpiring()
        {
            var records = await _healthCareService.GetExpiringHealthCareRecords();
            return Ok(records);
        }
    }
}