using System;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Client.Indexes;

namespace Academy_RavenDB
{
    public class RavenDBFixture : IDisposable
    {
        private readonly IDocumentStore documentStore;

        public RavenDBFixture()
        {
            documentStore = new EmbeddableDocumentStore
            {
                RunInMemory = true
            };

            documentStore.Conventions.DefaultQueryingConsistency = ConsistencyOptions.AlwaysWaitForNonStaleResultsAsOfLastWrite;

            //documentStore = new DocumentStore { ConnectionStringName = "RavenDB-Local" };

            documentStore.Initialize();

            IndexCreation.CreateIndexes(typeof (RavenDBFixture).Assembly, documentStore);
        }

        public IDocumentStore DocumentStore
        {
            get { return documentStore; }
        }

        public void Dispose()
        {
            documentStore.Dispose();
        }
    }
}