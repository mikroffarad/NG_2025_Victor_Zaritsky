using Microsoft.AspNetCore.Mvc;
using PetStoreBL.Models;
using PetStoreBL.Services.Interfaces;
using DAL_Core.Enums;

namespace PetStoreBS.Controllers
{
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
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(Guid id)
        {
            try
            {
                var pet = await _petService.GetPetById(id);
                return Ok(pet);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddPet([FromBody] PetDto petDto)
        {
            try
            {
                var id = await _petService.AddPet(petDto);
                return CreatedAtAction(nameof(GetPetById), new { id }, null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePet(Guid id, [FromBody] PetDto petDto)
        {
            try
            {
                await _petService.UpdatePet(id, petDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetPetsByStore(Guid storeId)
        {
            var pets = await _petService.GetPetsByStore(storeId);
            return Ok(pets);
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetPetsByType(PetTypes type)
        {
            var pets = await _petService.GetPetsByType(type);
            return Ok(pets);
        }

        [HttpPost("adopt")]
        public async Task<IActionResult> AdoptPet([FromBody] AdoptionRequest request)
        {
            try
            {
                await _petService.AdoptPet(request.PetId, request.CustomerId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}