using System.Collections.Generic;

namespace GenRepo.Client.Model
{
    public class Brand
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LogoPath { get; set; }

        public List<Product> Products { get; set; } 
    }
}