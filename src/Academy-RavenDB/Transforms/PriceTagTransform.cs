using System.Linq;
using Academy_RavenDB.Models;
using Raven.Client.Indexes;

namespace Academy_RavenDB.Transforms
{
    public class PriceTagTransform : AbstractTransformerCreationTask<Product>
    {
        public PriceTagTransform()
        {
            TransformResults = docs => from p in docs
                                       select new
                                       {
                                           PriceTag = string.Format("{0} ({1})", p.Name, p.Price)
                                       };
        }

        public class PriceTagResult
        {
            public string PriceTag { get; set; }
        }
    }
}