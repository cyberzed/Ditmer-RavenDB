﻿using System;
using System.Collections.Generic;
using System.Linq;
using Academy_RavenDB.Indexes;
using Academy_RavenDB.Models;
using Academy_RavenDB.TestInfrastructure;
using Raven.Client;
using Xunit;

namespace Academy_RavenDB
{
    public class QueryOperations : IUseFixture<RavenDBFixture>
    {
        private IDocumentStore documentStore;

        [Fact]
        public void Query()
        {
            var users = new List<User>
            {
                new User("Svend Petersen", new DateTime(1980, 1, 14)),
                new User("Jørgen Hansen", new DateTime(1970, 4, 11)),
                new User("Peter Nielsen", new DateTime(1960, 7, 1))
            };

            using (var session = documentStore.OpenSession())
            {
                users.ForEach(session.Store);
                session.SaveChanges();
            }

            using (var session = documentStore.OpenSession())
            {
                var cutoff = new DateTime(1980, 1, 1);

                var oldies = (from u in session.Query<User>()
                              where
                                  u.Birthday < cutoff
                              select u).ToList();

                Assert.NotEmpty(oldies);
            }
        }

        [Fact]
        public void StringQuery()
        {
            var user = new User("Stefan Daugaard Poulsen", new DateTime(1980, 3, 14));

            using (var session = documentStore.OpenSession())
            {
                session.Store(user);
                session.SaveChanges();
            }

            using (var session = documentStore.OpenSession())
            {
                var stefan = (from u in session.Query<User>()
                              where
                                  u.Name.Contains("Stefan")
                              select u).FirstOrDefault();

                Assert.NotNull(stefan);
            }
        }

        [Fact]
        public void StringQueryPartII()
        {
            var user = new User("Stefan Daugaard Poulsen", new DateTime(1980, 3, 14));

            using (var session = documentStore.OpenSession())
            {
                session.Store(user);
                session.SaveChanges();
            }

            using (var session = documentStore.OpenSession())
            {
                var stefan = session.Query<User, UserNameIndex>()
                    .Search(u => u.Name, "Daugaard")
                    .FirstOrDefault();

                Assert.NotNull(stefan);
            }
        }

        [Fact]
        public void StringQueryPartIII()
        {
            var user = new User("Kim Hansen", new DateTime(1970, 8, 22));

            using (var session = documentStore.OpenSession())
            {
                session.Store(user);
                session.SaveChanges();
            }

            using (var session = documentStore.OpenSession())
            {
                var stefan = session.Query<User, UserNameIndex>()
                    .Search(u => u.Name, "*ans*", escapeQueryOptions: EscapeQueryOptions.AllowAllWildcards)
                    .FirstOrDefault();

                Assert.NotNull(stefan);
            }
        }

        public void SetFixture(RavenDBFixture data)
        {
            documentStore = data.DocumentStore;
        }
    }
}