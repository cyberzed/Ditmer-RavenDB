using System.Linq;
using Academy_RavenDB.Models;
using Raven.Client.Indexes;

namespace Academy_RavenDB.Indexes
{
    public class ProductCountIndex : AbstractIndexCreationTask<Product, ProductCountIndex.ProductCountResult>
    {
        public ProductCountIndex()
        {
            Map = docs => from p in docs
                          select new
                          {
                              p.Category,
                              Count = 1
                          };

            Reduce = docs => from p in docs
                             group p by p.Category
                             into g
                             select new
                             {
                                 Category = g.Key,
                                 Count = g.Sum(x => x.Count)
                             };
        }

        public class ProductCountResult
        {
            public int Count { get; set; }
            public int Category { get; set; }
        }
    }
}