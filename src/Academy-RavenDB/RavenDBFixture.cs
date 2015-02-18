using System;
using Raven.Client;
using Raven.Client.Document;

namespace Academy_RavenDB
{
    public class RavenDBFixture : IDisposable
    {
        private readonly IDocumentStore documentStore;

        public RavenDBFixture()
        {
            documentStore = new DocumentStore {ConnectionStringName = "RavenDB-Local"};

            documentStore.Initialize();
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