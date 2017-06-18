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
                garden.Longitude
            },
            commandType: System.Data.CommandType.StoredProcedure).ConfigureAwait(false);
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
    }
}
