using System.Linq;
using Academy_RavenDB.Models;
using Raven.Client.Indexes;

namespace Academy_RavenDB.Indexes
{
    public class ProductAveragePriceIndex :
        AbstractIndexCreationTask<Product, ProductAveragePriceIndex.ProductAverageResult>
    {
        public ProductAveragePriceIndex()
        {
            Map = docs => from p in docs
                          select new
                          {
                              p.Category,
                              SumPrice = p.Price,
                              AveragePrice = 0,
                              ProductCount = 1
                          };

            Reduce = docs => from r in docs
                             group r by r.Category
                             into g
                             let productCount = g.Sum(x => x.ProductCount)
                             let priceSum = g.Sum(x => x.SumPrice)
                             select new
                             {
                                 Category = g.Key,
                                 SumPrice = priceSum,
                                 AveragePrice = priceSum/productCount,
                                 ProductCount = productCount
                             };
        }

        public class ProductAverageResult
        {
            public int Category { get; set; }
            public decimal SumPrice { get; set; }
            public decimal AveragePrice { get; set; }
            public int ProductCount { get; set; }
        }
    }
}