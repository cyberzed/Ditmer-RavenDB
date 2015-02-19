using System;
using Academy_RavenDB.Models;
using Academy_RavenDB.TestInfrastructure;
using Ploeh.AutoFixture.Xunit;
using Raven.Abstractions.Data;
using Raven.Client;
using Xunit;
using Xunit.Extensions;

namespace Academy_RavenDB
{
    public class Patch : IUseFixture<RavenDBFixture>
    {
        private IDocumentStore documentStore;

        [Theory, AutoData]
        public void Patching(Product product)
        {
            var expected = product.Price + 10;
            var id = Guid.Empty;

            using (var session = documentStore.OpenSession())
            {
                session.Store(product);
                session.SaveChanges();

                id = product.Id;
            }

            documentStore.DatabaseCommands.Patch(
                string.Format("products/{0}", id),
                new[]
                {
                    new PatchRequest
                    {
                        Type = PatchCommandType.Inc,
                        Name = "Price",
                        Value = 10
                    }
                });

            using (var session = documentStore.OpenSession())
            {
                var actualProduct = session.Load<Product>(id);

                Assert.Equal(expected, actualProduct.Price);
            }
        }

        public void SetFixture(RavenDBFixture data)
        {
            documentStore = data.DocumentStore;
        }
    }
}