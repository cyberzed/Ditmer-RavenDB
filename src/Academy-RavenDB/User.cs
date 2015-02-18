using System;

namespace Academy_RavenDB
{
    public class User : IEquatable<User>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public User(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Name = name;
        }

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return Id.Equals(other.Id) && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode()*397) ^ Name.GetHashCode();
            }
        }

        public static bool operator ==(User left, User right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !Equals(left, right);
        }
    }
}