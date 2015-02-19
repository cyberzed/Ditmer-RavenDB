using System.Collections.Generic;
using System.Linq;
using Ploeh.AutoFixture.Xunit;
using Raven.Abstractions.Extensions;
using Raven.Client;
using Xunit;
using Xunit.Extensions;

namespace Academy_RavenDB
{
    public class Reduce : IUseFixture<RavenDBFixture>
    {
        private IDocumentStore documentStore;

        [Theory, AutoData]
        public void CountProducts(IEnumerable<Product> products)
        {
            using (var session = documentStore.OpenSession())
            {
                products.ForEach(session.Store);
                session.SaveChanges();
            }

            using (var session = documentStore.OpenSession())
            {
                var productCount = session
                    .Query<ProductCountIndex.ProductCountResult, ProductCountIndex>()
                    .ToList();

                Assert.NotEmpty(productCount);
            }
        }

        [Theory, AutoData]
        public void AveragePrice(List<Product> products)
        {
            using (var session = documentStore.OpenSession())
            {
                products.ForEach(session.Store);
                session.SaveChanges();
            }

            using (var session = documentStore.OpenSession())
            {
                var averagePrices = session
                    .Query<ProductAveragePriceIndex.ProductAverageResult, ProductAveragePriceIndex>()
                    .ToList();

                Assert.NotEmpty(averagePrices);
            }
        }

        public void SetFixture(RavenDBFixture data)
        {
            documentStore = data.DocumentStore;
        }
    }
}