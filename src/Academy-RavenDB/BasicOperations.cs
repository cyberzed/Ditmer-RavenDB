using Raven.Client;
using Xunit;

namespace Academy_RavenDB
{
    public class BasicOperations : IUseFixture<RavenDBFixture>
    {
        private IDocumentStore documentStore;

        [Fact]
        public void ConnectToRaven()
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(new User("Stefan"));
                session.SaveChanges();
            }
        }

        public void SetFixture(RavenDBFixture data)
        {
            documentStore = data.DocumentStore;
        }
    }
}