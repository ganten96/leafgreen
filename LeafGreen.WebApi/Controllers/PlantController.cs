using System.Collections.Generic;
using System.Threading.Tasks;
using LeafGreen.Entities;
using LeafGreen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LeafGreen.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/plants")]
    public class PlantController : Controller
    {
        private readonly IPlantSqlRepository _repo;

        public PlantController(IPlantSqlRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("garden/{gardenId:int}/add")]
        public async Task<IActionResult> AddPlant([FromBody] Plant plant, int gardenId)
        {
            var garden = _repo.GetGardenByIdAsync(gardenId);
            if (garden == null)
                return NotFound(new {message = $"No gardens found for gardenId {gardenId}"});
            if (plant == null)
                return BadRequest("No plants were sent.");
            var inserted = await _repo.InsertGardenPlantAsync(plant, gardenId);
            if (inserted.PlantId == 0)
                return BadRequest("No plants were inserted.");
            return Ok();
        }

        [HttpDelete("garden/{gardenId:int}/remove/{plantId:int}")]
        public async Task<IActionResult> RemovePlantsFromGarden(int plantId, int gardenId)
        {
            if (gardenId == 0 || plantId == 0)
            {
                return BadRequest("GardenId or PlantId not supplied.");
            }
            var isPlantDeleted = await _repo.DeletePlantFromGardenAsync(plantId, gardenId);
            if (isPlantDeleted)
                return Ok();
            return NotFound();
        }
    }
}