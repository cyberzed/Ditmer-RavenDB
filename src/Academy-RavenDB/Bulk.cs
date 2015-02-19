using System.Collections.Generic;
using System.Linq;
using Academy_RavenDB.Models;
using Academy_RavenDB.TestInfrastructure;
using Ploeh.AutoFixture.Xunit;
using Raven.Client;
using Xunit;
using Xunit.Extensions;

namespace Academy_RavenDB
{
    public class Bulk : IUseFixture<RavenDBFixture>
    {
        private IDocumentStore documentStore;

        [Theory, AutoData]
        public void Bulking(List<Product> products)
        {
            using (var bulkInsert = documentStore.BulkInsert())
            {
                foreach (var product in products)
                {
                    bulkInsert.Store(product);
                }
            }

            using (var session = documentStore.OpenSession())
            {
                var actualProducts = session.Query<Product>().ToList();

                Assert.NotEmpty(actualProducts);
            }
        }

        public void SetFixture(RavenDBFixture data)
        {
            documentStore = data.DocumentStore;
        }
    }
}