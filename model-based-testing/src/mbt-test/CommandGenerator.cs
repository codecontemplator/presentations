using FsCheck;
using mbt_lib;
using Microsoft.Extensions.Options;
using mtb_webapp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace mbt_test
{
    class CommandGenerator : ICommandGenerator<PersonStore, PersonStoreSimulator>
    {
        
        public PersonStore InitialActual
        {
            get
            {
                var config = new StoreConfig
                {
                    ConnectionString = Environment.GetEnvironmentVariable("DOTNET_StoreConfig__ConnectionString"),
                    TableNamePrefix = "TempPersonEntities" + Guid.NewGuid().ToString().Replace("-", "")
                };

                return new PersonStore(Options.Create(config));
            }
        }

        public PersonStoreSimulator InitialModel => new PersonStoreSimulator();

        public Gen<Command<PersonStore, PersonStoreSimulator>> Next(PersonStoreSimulator value)
        {
            return
                from personEntity in GeneratePersons()
                select new AddPersonCommand(personEntity) as Command<PersonStore, PersonStoreSimulator>;            
        }

        private Gen<PersonEntity> GeneratePersons()
        {
            return
                from nationalId in Gen.ArrayOf(10, Gen.Choose(0,9))
                from country in Gen.Elements("Sweden", "Denmark", "Norway")
                from name in Arb.Generate<string>()
                from eyeColor in Gen.Elements("Blue", "Green", "Brown")
                select new PersonEntity { RowKey = DigitsToString(nationalId), PartitionKey = country, Name = name, EyeColor = eyeColor }; 
        }

        private static string DigitsToString(IEnumerable<int> digits) => string.Join("", digits.Select(x => x.ToString()));
    }
}
