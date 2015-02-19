using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Academy_RavenDB
{
    public class UserNameIndex : AbstractIndexCreationTask<User>
    {
        public UserNameIndex()
        {
            Map = docs => from d in docs
                          select new {d.Name};

            Indexes.Add(d => d.Name, FieldIndexing.Analyzed);
        }
    }
}