
using System;

namespace GeePoo.Client.Model
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public double Price { get; set; }

        public float AverageRatings { get; set; }

        public virtual Brand Brand { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}