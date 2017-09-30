using System;
using System.Linq;
using System.Security.Cryptography;

namespace LeafGreen.Entities
{
    public class Plant
    {
        public int GardenPlantId { get; set; }
        public string PlantName { get; set; }
        public string Symbol { get; set; }
        public string Author { get; set; }
        public string ScientificName { get; set; }
        public string CommonName { get; set; }
        public string Family { get; set; }
        public string PlantHash { get; set; }
        public int PlantId { get; set; }

        public string ComputePlantHash()
        {
            var plantString = $"{Author}{CommonName}{Family}{ScientificName}{Symbol}";
            var stringBytes = System.Text.Encoding.ASCII.GetBytes(plantString);
            var bytes = SHA256.Create().ComputeHash(stringBytes);
            return bytes.Aggregate(string.Empty, (current, x) => current + String.Format("{0:x2}", x));
        }
    }
}
