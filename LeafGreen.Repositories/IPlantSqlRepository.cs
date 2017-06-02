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
    }
}
