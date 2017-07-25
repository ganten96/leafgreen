using LeafGreen.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
namespace LeafGreen.MobileApi
{
    public class GardenApi
    {
        private readonly string _deviceId;

        public GardenApi(string deviceId)
        {
            _deviceId = deviceId;
        }

        public async Task<Garden> AddGardenAsync(Garden garden)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://leafgreen-dev.azurewebsites.net");
                var json = JsonConvert.SerializeObject(garden);
                var content = new StringContent(json, Encoding.ASCII, "application/json");
                var response = await httpClient.PostAsync(new Uri("api/garden"), content);
                return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Garden>(await response.Content.ReadAsStringAsync()) : new Garden();
            }
        }

        public async Task<IEnumerable<Garden>> GetGardensByDeviceIdAsync(string deviceId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://leafgreen-dev.azurewebsites.net");
                var response = await httpClient.GetAsync(new Uri($"api/{deviceId}/gardens"));
                return response.IsSuccessStatusCode ? 
                    JsonConvert.DeserializeObject<IEnumerable<Garden>>(await response.Content.ReadAsStringAsync()) : new List<Garden>();
            }
        }

        public async Task<Garden> GetGardenByIdAsync(int gardenId)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://leafgreen-dev.azurewebsites.net");
                var response = await httpClient.GetAsync(new Uri($"api/gardens/{gardenId}"));
                return response.IsSuccessStatusCode ?
                    JsonConvert.DeserializeObject<Garden>(await response.Content.ReadAsStringAsync()) : new Garden();
            }
        }
    }
}
