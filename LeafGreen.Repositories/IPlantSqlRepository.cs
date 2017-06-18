using LeafGreen.Entities;
using LeafGreen.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeafGreen.Repositories
{
    public interface IPlantSqlRepository : IRepository
    {
        Task<int> InsertPlantsAsync(List<Plant> plants);
        Task<List<Plant>> InsertPlantListAsync(List<Plant> plants);
        Task<Garden> InsertGardenAsync(Garden garden);
        Task<IEnumerable<Garden>> SelectAllGardensAsync();
        Task<Garden> GetGardenByIdAsync(int gardenId);
    }
}
