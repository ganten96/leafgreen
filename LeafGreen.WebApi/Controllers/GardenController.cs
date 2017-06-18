using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public async Task<Garden> AddGarden(Garden garden)
        {
            return await _repo.InsertGardenAsync(garden);
        }
    }
}