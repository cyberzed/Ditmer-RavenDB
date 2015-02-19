using System;
using System.Collections.Generic;
using System.Linq;
using Academy_RavenDB.Indexes;
using Academy_RavenDB.Models;
using Academy_RavenDB.TestInfrastructure;
using Raven.Client;
using Raven.Client.Linq;
using Xunit;

namespace Academy_RavenDB
{
    public class Suggestion : IUseFixture<RavenDBFixture>
    {
        private IDocumentStore documentStore;

        [Fact]
        public void Suggest()
        {
            var users = new List<User>
            {
                new User("Svend", new DateTime(1980, 1, 14)),
                new User("Svea", new DateTime(1970, 4, 11)),
                new User("Svetalana", new DateTime(1960, 7, 1))
            };

            using (var session = documentStore.OpenSession())
            {
                users.ForEach(session.Store);
                session.SaveChanges();
            }

            using (var session = documentStore.OpenSession())
            {
                var result = session
                    .Query<User, UserNameIndex>()
                    .Where(u => u.Name == "sven");

                if (result.Any())
                {
                    return;
                }

                RavenQueryStatistics stats;
                var suggestion = result.Statistics(out stats).Suggest();

                Assert.NotEmpty(suggestion.Suggestions);
            }
        }

        public void SetFixture(RavenDBFixture data)
        {
            documentStore = data.DocumentStore;
        }
    }
}