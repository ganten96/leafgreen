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
        Task<IEnumerable<Garden>> SelectAllGardensByDeviceIdAsync(string deviceId);
        Task<IEnumerable<Plant>> GetAllPlantsForGardenByGardenId(int gardenId);
        Task<Plant> InsertGardenPlantAsync(Plant plant, int gardenId);
        Task<bool> DeletePlantFromGardenAsync(int plantId, int gardenId);
    }
}
