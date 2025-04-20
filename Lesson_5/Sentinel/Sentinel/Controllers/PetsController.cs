using DAL_Core.Enums;
using Microsoft.AspNetCore.Mvc;
using SentinelBusinessLayer.Models;
using SentinelBusinessLayer.Service.Interface;
using System;
using System.Threading.Tasks;

namespace Sentinel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PetsController : ControllerBase
{
    private readonly IPetService _petService;

    public PetsController(IPetService petService)
    {
        _petService = petService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPets()
    {
        var pets = await _petService.GetAllPets();
        if (pets == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "PetStore service is unavailable.");
        return Ok(pets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPetById(Guid id)
    {
        var pet = await _petService.GetPetById(id);
        if (pet == null) return NotFound($"Pet with id {id} not found or service unavailable.");
        return Ok(pet);
    }

    [HttpPost]
    public async Task<IActionResult> AddPet([FromBody] PetDto petDto)
    {
        var createdPet = await _petService.AddPet(petDto);
        if (createdPet == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "Could not create pet. Service might be unavailable.");
        return CreatedAtAction(nameof(GetPetById), new { id = createdPet.Id }, createdPet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePet(Guid id, [FromBody] PetDto petDto)
    {
        if (id != petDto.Id)
        {
            return BadRequest("ID mismatch in route and body.");
        }
        var success = await _petService.UpdatePet(id, petDto);
        if (!success) return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Could not update pet {id}. Service might be unavailable or pet not found.");
        return NoContent();
    }

    [HttpGet("store/{storeId}")]
    public async Task<IActionResult> GetPetsByStore(Guid storeId)
    {
        var pets = await _petService.GetPetsByStore(storeId);
        if (pets == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "PetStore service is unavailable.");
        return Ok(pets);
    }

    [HttpGet("type/{type}")]
    public async Task<IActionResult> GetPetsByType(PetTypes type)
    {
        var pets = await _petService.GetPetsByType(type);
        if (pets == null) return StatusCode(StatusCodes.Status503ServiceUnavailable, "PetStore service is unavailable.");
        return Ok(pets);
    }

    [HttpPost("adopt")]
    public async Task<IActionResult> AdoptPet([FromBody] AdoptionRequest request)
    {
        var success = await _petService.AdoptPet(request);
        if (!success) return StatusCode(StatusCodes.Status503ServiceUnavailable, $"Could not adopt pet {request.PetId}. Service might be unavailable or pet not found.");
        return Ok();
    }
}