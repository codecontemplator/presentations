using FsCheck;
using mbt_lib;
using System.Collections.Generic;
using System.Linq;

namespace mbt_test
{
    class AddPersonCommand : Command<PersonStore, PersonStoreSimulator>
    {
        private readonly PersonEntity _personEntity;

        public AddPersonCommand(PersonEntity personEntity)
        {
            _personEntity = personEntity;
        }

        public override PersonStore RunActual(PersonStore store) => Run(store);

        public override PersonStoreSimulator RunModel(PersonStoreSimulator store) => Run(store);

        private T Run<T>(T store) where T : IPersonStore
        {
            store.InsertOrReplace(_personEntity).Wait();
            return store;
        }

        public override Property Post(PersonStore store, PersonStoreSimulator simulator)
        {
            var queries =
                from entity in Gen.Elements((IEnumerable<PersonEntity>)simulator.Persons)
                from nationalId in Gen.Elements(entity.RowKey, null, "750329709752307")
                from country in Gen.Elements(entity.PartitionKey, null, "India")
                from eyeColor in Gen.Elements(entity.EyeColor, null, "Red")
                where nationalId != null || country != null || eyeColor != null
                select (nationalId, country, eyeColor);

            return Prop.ForAll(Arb.From(queries), query =>
                SearchResultsAreEquivalent(
                    store.Search(query.nationalId, query.country, query.eyeColor).Result,
                    simulator.Search(query.nationalId, query.country, query.eyeColor).Result
                )
            );
        }

        private static bool SearchResultsAreEquivalent(IEnumerable<PersonEntity> a, IEnumerable<PersonEntity> b)
        {
            return Enumerable.SequenceEqual(
                a.OrderBy(x => x.RowKey), 
                b.OrderBy(x => x.RowKey), 
                PersonEntityComparer.Instance);
        }

        public override string ToString()
        {
            return $"Add {_personEntity.RowKey} {_personEntity.PartitionKey} {_personEntity.EyeColor}";
        }
    }
}
