using Microsoft.AspNetCore.Mvc;
using PetStoreBL.Models;
using PetStoreBL.Services.Interfaces;

namespace PetStoreBS.Controllers
{
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
            return Ok(stores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreById(Guid id)
        {
            try
            {
                var store = await _storeService.GetStoreById(id);
                return Ok(store);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(Guid id, [FromBody] StoreDto storeDto)
        {
            try
            {
                await _storeService.UpdateStore(id, storeDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{storeId}/pets")]
        public async Task<IActionResult> GetStorePets(Guid storeId)
        {
            var pets = await _storeService.GetStorePets(storeId);
            return Ok(pets);
        }

        [HttpGet("{storeId}/healthcare")]
        public async Task<IActionResult> GetStoreHealthcareRecords(Guid storeId)
        {
            var healthcareRecords = await _storeService.GetStoreHealthcareRecords(storeId);
            return Ok(healthcareRecords);
        }
    }
}