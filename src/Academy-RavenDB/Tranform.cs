using System.Collections.Generic;
using System.Linq;
using Academy_RavenDB.Models;
using Academy_RavenDB.TestInfrastructure;
using Academy_RavenDB.Transforms;
using Ploeh.AutoFixture.Xunit;
using Raven.Abstractions.Extensions;
using Raven.Client;
using Xunit;
using Xunit.Extensions;

namespace Academy_RavenDB
{
    public class Tranform : IUseFixture<RavenDBFixture>
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
                var pricetags = session
                    .Query<Product>()
                    .TransformWith<PriceTagTransform, PriceTagTransform.PriceTagResult>()
                    .ToList();

                Assert.NotEmpty(pricetags);
            }
        }

        public void SetFixture(RavenDBFixture data)
        {
            documentStore = data.DocumentStore;
        }
    }
}