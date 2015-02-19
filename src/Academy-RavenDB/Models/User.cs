using System;

namespace Academy_RavenDB.Models
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public DateTime Birthday { get; private set; }

        public User(string name, DateTime birthday)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
            Birthday = birthday;
        }
    }
}