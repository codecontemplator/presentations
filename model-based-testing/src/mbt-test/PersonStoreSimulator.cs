using mbt_lib;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mbt_test
{
    class PersonStoreSimulator : IPersonStore
    {
        private struct Key
        {
            public Key(PersonEntity pe)
            {
                RowKey = pe.RowKey;
                PartitionKey = pe.PartitionKey;
                EyeColor = pe.EyeColor;
            }

            public string RowKey { get; }
            public string PartitionKey { get; }
            public string EyeColor { get; }
        }

        private Dictionary<Key, PersonEntity> PersonsMap { get; } = new Dictionary<Key, PersonEntity>();

        public IReadOnlyCollection<PersonEntity> Persons => PersonsMap.Values;

        public Task InsertOrReplace(PersonEntity person)
        {
            PersonsMap[new Key(person)] = person;
            return Task.CompletedTask;
        }
        
        public Task<IEnumerable<PersonEntity>> Search(string nationalId, string country, string eyeColor)
        {
            IEnumerable<PersonEntity> result = PersonsMap.Values;

            if (!string.IsNullOrWhiteSpace(nationalId))
                result = result.Where(x => x.RowKey == nationalId);

            if (!string.IsNullOrWhiteSpace(country))
                result = result.Where(x => x.PartitionKey == country);

            if (!string.IsNullOrWhiteSpace(eyeColor))
                result = result.Where(x => x.EyeColor == eyeColor);

            return Task.FromResult(result);
        }
    }
}