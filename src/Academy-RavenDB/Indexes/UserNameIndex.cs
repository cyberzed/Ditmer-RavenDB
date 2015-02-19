using System.Linq;
using Academy_RavenDB.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Academy_RavenDB.Indexes
{
    public class UserNameIndex : AbstractIndexCreationTask<User>
    {
        public UserNameIndex()
        {
            Map = docs => from d in docs
                          select new {d.Name};

            Indexes.Add(d => d.Name, FieldIndexing.Analyzed);

            Suggestion(u => u.Name, new SuggestionOptions{Accuracy = 0.3f});
        }
    }
}