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
            foreach(var plant in plants)
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
                if(!isInserted)
                {
                    uninsertedPlants.Add(plant);
                }
            }
            return uninsertedPlants;
        }

        public async Task<int> InsertPlantsAsync(List<Plant> plants)
        {
            //gotta do it the naive way because tvps are not yet supported....
            throw new NotImplementedException("This has not been implemented due to UDTs being unsupported.");
        }
    }
}
