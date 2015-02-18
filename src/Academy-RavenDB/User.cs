using System;

namespace Academy_RavenDB
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public User(string name)
        {
            Name = name;
        }
    }
}