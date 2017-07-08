using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LeafGreen.Repositories;
using LeafGreen.Entities;

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
        public async Task<IEnumerable<Garden>> GetAllGardens()
        {
            return await _repo.SelectAllGardensAsync();
        }

        [HttpPost]
        public async Task<Garden> AddGarden([FromBody]Garden garden)
        {
            return await _repo.InsertGardenAsync(garden);
        }

        [HttpGet("{gardenId:int}")]
        public async Task<Garden> GetGardenById(int gardenId)
        {
            return await _repo.GetGardenByIdAsync(gardenId);
        }

        [HttpGet("{deviceId}/gardens")]
        public async Task<IEnumerable<Garden>> GetGardensByDeviceId(string deviceId)
        {
            return await _repo.SelectAllGardensByDeviceIdAsync(deviceId);
        }


    }
}