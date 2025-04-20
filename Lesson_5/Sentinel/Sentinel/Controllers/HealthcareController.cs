using Microsoft.AspNetCore.Mvc;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sentinel.Controllers;

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
        if (records == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "HealthCare service is unavailable.");
        return Ok(records);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var record = await _healthCareService.GetHealthCareById(id);
        if (record == null) return NotFound($"Healthcare record {id} not found or service unavailable.");
        return Ok(record);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HealthCareDto dto)
    {
        var success = await _healthCareService.AddHealthCareRecord(dto);
        if (!success) return StatusCode(StatusCodes.Status503ServiceUnavailable, "Could not create healthcare record. Service might be unavailable.");
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] HealthCareDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID mismatch in route and body.");
        }
        var success = await _healthCareService.UpdateHealthCareRecord(id, dto);
        if (!success) return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Could not update healthcare record {id}. Service might be unavailable or record not found.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _healthCareService.DeleteHealthCareRecord(id);
        if (!success) return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Could not delete healthcare record {id}. Service might be unavailable or record not found.");
        return NoContent();
    }

    [HttpGet("pet/{petId}")]
    public async Task<IActionResult> GetByPet(Guid petId)
    {
        var records = await _healthCareService.GetHealthCareRecordsByPet(petId);
        if (records == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "HealthCare service is unavailable.");
        return Ok(records);
    }

    [HttpGet("vendor/{vendorId}")]
    public async Task<IActionResult> GetByVendor(Guid vendorId)
    {
        var records = await _healthCareService.GetHealthCareRecordsByVendor(vendorId);
        if (records == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "HealthCare service is unavailable.");
        return Ok(records);
    }

    [HttpGet("expiring")]
    public async Task<IActionResult> GetExpiring()
    {
        var records = await _healthCareService.GetExpiringHealthCareRecords();
        if (records == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "HealthCare service is unavailable.");
        return Ok(records);
    }
}