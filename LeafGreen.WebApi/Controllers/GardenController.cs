using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LeafGreen.Repositories;
using LeafGreen.Entities;
using System;

namespace LeafGreen.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/garden")]
    public class GardenController : Controller
    {
        private readonly IPlantSqlRepository _repo;
        public GardenController(IPlantSqlRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllGardens()
        {
            try
            {
                return Ok(await _repo.SelectAllGardensAsync());
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddGarden([FromBody]Garden garden)
        {
            try
            {
                return Ok(await _repo.InsertGardenAsync(garden));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{gardenId:int}")]
        public async Task<IActionResult> GetGardenById(int gardenId)
        {
            try
            {
                return Ok(await _repo.GetGardenByIdAsync(gardenId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{deviceId}/gardens")]
        public async Task<IActionResult> GetGardensByDeviceId(string deviceId)
        {
            try
            {
                return Ok(await _repo.SelectAllGardensByDeviceIdAsync(deviceId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}