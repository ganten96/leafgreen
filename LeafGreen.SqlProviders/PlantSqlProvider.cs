using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeafGreen.Entities;
using LeafGreen.Repositories;
using Dapper;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace LeafGreen.SqlProviders
{
    public class PlantSqlProvider : IPlantSqlRepository
    {
        private SqlConnection _connection;

        public PlantSqlProvider(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task<List<Plant>> InsertPlantListAsync(List<Plant> plants)
        {
            var uninsertedPlants = new List<Plant>();
            foreach (var plant in plants)
            {
                var isInserted = (await _connection.QueryFirstAsync<int>("[plants].[usp_InsertPlant]", new
                {
                    plant.Symbol,
                    plant.ScientificName,
                    plant.Author,
                    plant.CommonName,
                    plant.Family,
                    PlantHash = plant.ComputePlantHash()
                }, commandType: System.Data.CommandType.StoredProcedure)) > 0;
                if (!isInserted)
                {
                    uninsertedPlants.Add(plant);
                }
            }
            return uninsertedPlants;
        }

        public async Task<Garden> InsertGardenAsync(Garden garden)
        {
            return await _connection.QueryFirstAsync<Garden>("[plants].[usp_InsertGarden]",
            new
            {
                garden.GardenName,
                IsArchived = false,
                garden.DateAdded,
                garden.Latitude,
                garden.Longitude,
                garden.DeviceId
            },
            commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        }

        public async Task<bool> InsertcGardenPlantAsync(Plant plant, int gardenId)
        {
            return await _connection.ExecuteAsync("[plants].[usp_InsertPlant]", new
            {
                plant.Symbol,
                plant.ScientificName,
                plant.Author,
                plant.CommonName,
                plant.Family,
                PlantHash = plant.ComputePlantHash()
            }, commandType: System.Data.CommandType.StoredProcedure) > 0;
        }

        public Task<int> InsertPlantsAsync(List<Plant> plants)
        {
           throw new NotImplementedException();
        }

        public async Task<IEnumerable<Garden>> SelectAllGardensAsync()
        {
            return await _connection
                .QueryAsync<Garden>("[plants].[usp_SelectAllGardens]", commandType: System.Data.CommandType.StoredProcedure)
                .ConfigureAwait(false);
        }

        public async Task<Garden> GetGardenByIdAsync(int gardenId)
        {
            return await _connection
                .QuerySingleAsync<Garden>("[plants].[usp_SelectGardenById]",  new
                {
                    GardenId = gardenId
                }, commandType: System.Data.CommandType.StoredProcedure)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Garden>> SelectAllGardensByDeviceIdAsync(string deviceId)
        {
            return await _connection.
                    QueryAsync<Garden>("[plants].[usp_SelectGardensByDeviceId]",
                    new { DeviceId = deviceId },
                    commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Plant>> GetAllPlantsForGardenByGardenId(int gardenId)
        {
            return await _connection
                .QueryAsync<Plant>("[plants].[usp_SelectGardenPlantsByGardenId]",
                new { GardenId = gardenId },
                commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
        }

        public async Task<Plant> InsertGardenPlantAsync(Plant plant, int gardenId)
        {
            var insertedId = await _connection.ExecuteScalarAsync<int>("[plants].[usp_InsertGardenPlant]",
                new
                {
                    plant.PlantName,
                    GardenId = gardenId
                });
            if (insertedId > 0)
            {
                plant.PlantId = insertedId;
            }
            return plant;
        }

        public async Task<bool> DeletePlantFromGardenAsync(int plantId, int gardenId)
        {
            return await _connection.ExecuteAsync("[plants].[usp_DeletePlantByPlantId]",
                new {PlantId = plantId, GardenId = gardenId},
                commandType: System.Data.CommandType.StoredProcedure)
                .ConfigureAwait(false) > 0;
        }
    }
}
