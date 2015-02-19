using System;

namespace Academy_RavenDB.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }

        public int Category { get; set; }
    }
}