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
            var entitySet = new List<PersonEntity>();
            entitySet.AddRange(simulator.Persons);
            entitySet.Add(new PersonEntity { RowKey = "273523532241129057", PartitionKey = "Sweden", EyeColor = "Brown" });

            var testCaseGenerator =
                from entity in Gen.Elements((IEnumerable<PersonEntity>)entitySet)
                select new { Query = entity };

            return Prop.ForAll(Arb.From(testCaseGenerator), testCase =>
                Enumerable.SequenceEqual(
                    store.Search(testCase.Query.RowKey, testCase.Query.PartitionKey, testCase.Query.EyeColor).Result,
                    simulator.Search(testCase.Query.RowKey, testCase.Query.PartitionKey, testCase.Query.EyeColor).Result,
                    PersonEntityComparer.Instance
                )
            );
        }
    }
}
