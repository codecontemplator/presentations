using mbt_lib;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mbt_test
{
    class PersonStoreSimulator : IPersonStore
    {
        private Dictionary<string, PersonEntity> PersonsMap { get; } = new Dictionary<string, PersonEntity>();

        public IReadOnlyCollection<PersonEntity> Persons => PersonsMap.Values;

        public Task InsertOrReplace(PersonEntity person)
        {
            PersonsMap[person.RowKey] = person;
            return Task.CompletedTask;
        }
        
        public Task<IEnumerable<PersonEntity>> Search(string nationalId, string country, string eyeColor)
        {
            IEnumerable<PersonEntity> query = PersonsMap.Values;

            if (!string.IsNullOrWhiteSpace(nationalId))
                query = query.Where(x => x.RowKey == nationalId);

            if (!string.IsNullOrWhiteSpace(country))
                query = query.Where(x => x.PartitionKey == country);

            if (!string.IsNullOrWhiteSpace(eyeColor))
                query = query.Where(x => x.EyeColor == eyeColor);

            var result = query.ToList();
            return Task.FromResult((IEnumerable<PersonEntity>)result);
        }
    }
}