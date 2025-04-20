using Microsoft.AspNetCore.Mvc;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sentinel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoresController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoresController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStores()
    {
        var stores = await _storeService.GetAllStores();
        if (stores == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "PetStore service is unavailable.");
        return Ok(stores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStoreById(Guid id)
    {
        var store = await _storeService.GetStoreById(id);
        if (store == null) return NotFound($"Store with id {id} not found or service unavailable.");
        return Ok(store);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStore(Guid id, [FromBody] StoreDto storeDto)
    {
        if (id != storeDto.Id)
        {
            return BadRequest("ID mismatch in route and body.");
        }
        var success = await _storeService.UpdateStore(id, storeDto);
        if (!success) return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Could not update store {id}. Service might be unavailable or store not found.");
        return NoContent();
    }

    [HttpGet("{storeId}/pets")]
    public async Task<IActionResult> GetStorePets(Guid storeId)
    {
        var pets = await _storeService.GetStorePets(storeId);
        if (pets == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "PetStore service is unavailable.");
        return Ok(pets);
    }

    [HttpGet("{storeId}/healthcare")]
    public async Task<IActionResult> GetStoreHealthcareRecords(Guid storeId)
    {
        var records = await _storeService.GetStoreHealthcareRecords(storeId);
        if (records == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "Dependent services might be unavailable.");
        return Ok(records);
    }
}