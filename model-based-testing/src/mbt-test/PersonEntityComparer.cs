using mbt_lib;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace mbt_test
{
    class PersonEntityComparer : IEqualityComparer<PersonEntity>
    {
        static PersonEntityComparer()
        {
            Instance = new PersonEntityComparer();
        }

        public bool Equals([AllowNull] PersonEntity x, [AllowNull] PersonEntity y)
        {
            return 
                x.RowKey == y.RowKey && 
                x.PartitionKey == y.PartitionKey && 
                x.EyeColor == y.EyeColor && 
                x.Name == y.Name;
        }

        public int GetHashCode([DisallowNull] PersonEntity obj)
        {
            return obj.RowKey.GetHashCode();
        }

        public static PersonEntityComparer Instance { get; }
    }
}
