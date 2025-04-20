using DAL_Core.Enums;
using Microsoft.AspNetCore.Mvc;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sentinel.Controllers;

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
        if (vendors == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "HealthCare service (Vendors) is unavailable.");
        return Ok(vendors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var vendor = await _vendorService.GetVendorById(id);
        if (vendor == null) return NotFound($"Vendor {id} not found or service unavailable.");
        return Ok(vendor);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VendorDto dto)
    {
        var success = await _vendorService.AddVendor(dto);
        if (!success) return StatusCode(StatusCodes.Status503ServiceUnavailable, "Could not create vendor. Service might be unavailable.");
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] VendorDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID mismatch in route and body.");
        }
        var success = await _vendorService.UpdateVendor(id, dto);
        if (!success) return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Could not update vendor {id}. Service might be unavailable or vendor not found.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _vendorService.DeleteVendor(id);
        if (!success) return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Could not delete vendor {id}. Service might be unavailable or vendor not found.");
        return NoContent();
    }

    [HttpGet("contract/{type}")]
    public async Task<IActionResult> GetByContractType(ContractType type)
    {
        var vendors = await _vendorService.GetVendorsByContractType(type);
        if (vendors == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "HealthCare service (Vendors) is unavailable.");
        return Ok(vendors);
    }

    [HttpGet("{vendorId}/healthcare")]
    public async Task<IActionResult> GetVendorHealthcare(Guid vendorId)
    {
        var records = await _vendorService.GetVendorHealthCareRecords(vendorId);
        if (records == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "HealthCare service is unavailable.");
        return Ok(records);
    }
}