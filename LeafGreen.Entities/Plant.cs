using System;
using System.Security.Cryptography;

namespace LeafGreen.Entities
{
    public class Plant
    {
        public string Symbol { get; set; }
        public string ScientificName { get; set; }
        public string Author { get; set; }
        public string CommonName { get; set; }
        public string Family { get; set; }
        public string PlantHash { get; set; }

        public string ComputePlantHash()
        {
            var plantString = $"{Author}{CommonName}{Family}{ScientificName}{Symbol}";
            var stringBytes = System.Text.Encoding.ASCII.GetBytes(plantString);
            return System.Text.Encoding.ASCII.GetString(SHA256.Create().ComputeHash(stringBytes));
        }
    }
}
