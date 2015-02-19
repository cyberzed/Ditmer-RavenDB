using System;
using Ploeh.AutoFixture.Xunit;
using Raven.Client;
using Xunit;
using Xunit.Extensions;

namespace Academy_RavenDB
{
    public class BasicOperations : IUseFixture<RavenDBFixture>
    {
        private IDocumentStore documentStore;

        [Theory, AutoData]
        public void InsertAndLoad(string name, DateTime birthday)
        {
            var expected = new User(name, birthday);

            var id = Guid.Empty;

            using (var session = documentStore.OpenSession())
            {
                session.Store(expected);
                session.SaveChanges();

                id = expected.Id;
            }

            using (var session = documentStore.OpenSession())
            {
                var actual = session.Load<User>(id);

                Assert.Equal(expected, actual);
            }
        }

        public void SetFixture(RavenDBFixture data)
        {
            documentStore = data.DocumentStore;
        }
    }
}