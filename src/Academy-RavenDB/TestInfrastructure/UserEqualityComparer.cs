using System.Collections.Generic;
using Academy_RavenDB.Models;

namespace Academy_RavenDB.TestInfrastructure
{
    public class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User x, User y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (ReferenceEquals(x, null))
            {
                return false;
            }
            if (ReferenceEquals(y, null))
            {
                return false;
            }
            if (x.GetType() != y.GetType())
            {
                return false;
            }
            return x.Id.Equals(y.Id) && string.Equals(x.Name, y.Name) && x.Birthday.Equals(y.Birthday);
        }

        public int GetHashCode(User obj)
        {
            unchecked
            {
                var hashCode = obj.Id.GetHashCode();
                hashCode = (hashCode*397) ^ obj.Name.GetHashCode();
                hashCode = (hashCode*397) ^ obj.Birthday.GetHashCode();
                return hashCode;
            }
        }
    }
}