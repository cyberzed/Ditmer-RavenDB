using Raven.Client;
using Raven.Client.Listeners;

namespace Academy_RavenDB.RavenExtensions
{
    public class NonStateQueryListener : IDocumentQueryListener
    {
        public void BeforeQueryExecuted(IDocumentQueryCustomization queryCustomization)
        {
            queryCustomization.WaitForNonStaleResults();
        }
    }
}