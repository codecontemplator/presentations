using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using mtb_webapp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mbt_lib
{
    public class PersonStore : IPersonStore
    {
        private CloudTableClient _tableClient;
        private readonly string _tableNamePrefix;
        private readonly StoreConfig _storeConfig;

        public PersonStore(IOptions<StoreConfig> storeConfig)
        {
            _storeConfig = storeConfig.Value;
            CloudStorageAccount storageAccount;
            storageAccount = CloudStorageAccount.Parse(_storeConfig.ConnectionString);
            _tableClient = storageAccount.CreateCloudTableClient();
            _tableNamePrefix = _storeConfig.TableNamePrefix;
        }

        public async Task InsertOrReplace(PersonEntity person)
        {
            await DeleteEntityIfExists(person);
            var tableName = $"{_tableNamePrefix}{person.EyeColor}";
            var table = _tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();
            var insertOperation = TableOperation.Insert(person);
            await table.ExecuteAsync(insertOperation);
        }

        public async Task<IEnumerable<PersonEntity>> Search(string nationalId, string country, string eyeColor)
        {
            var filter = "";
            void AddFilterCondition(string condition)
            {
                filter =
                    string.IsNullOrEmpty(filter) ?
                        condition :
                            TableQuery.CombineFilters(filter, TableOperators.And, condition);
            }

            if (!string.IsNullOrWhiteSpace(nationalId))
                AddFilterCondition(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, nationalId));

            if (!string.IsNullOrWhiteSpace(country))
                AddFilterCondition(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, country));

            if (!string.IsNullOrWhiteSpace(eyeColor))
                AddFilterCondition(TableQuery.GenerateFilterCondition("EyeColor", QueryComparisons.Equal, eyeColor));

            var query = new TableQuery<PersonEntity>().Where(filter);

            var tables = await GetTables(eyeColor);
            var result = new List<PersonEntity>();

            foreach (var table in tables)
            {
                TableContinuationToken continuationToken = null;
                do
                {
                    var tempResult = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                    result.AddRange(tempResult);

                } while (continuationToken != null);
            }

            return result;
        }

        private async Task<IEnumerable<CloudTable>> GetTables(string eyeColor = null)
        {
            var result = new List<CloudTable>();
            if (string.IsNullOrEmpty(eyeColor))
            {
                TableContinuationToken continuationToken = null;
                do
                {
                    var tempResult = await _tableClient.ListTablesSegmentedAsync(_tableNamePrefix, continuationToken);
                    result.AddRange(tempResult);
                } while (continuationToken != null);
            }
            else
            {
                var tableName = $"{_tableNamePrefix}{eyeColor}";
                var table = _tableClient.GetTableReference(tableName);
                var exists = await table.ExistsAsync();
                if (exists) result.Add(table);
            }

            return result;
        }

        private async Task DeleteEntityIfExists(PersonEntity person)
        {
            var tables = await GetTables();
            var query =
                TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, person.RowKey),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition("PartiionKey", QueryComparisons.Equal, person.RowKey)
                    );

            foreach(var table in tables)
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(new TableQuery<PersonEntity>().Where(query), null);
                var personToDelete = queryResult.FirstOrDefault();
                if (personToDelete != null)
                {
                    var deleteOperation = TableOperation.Delete(personToDelete);
                    await table.ExecuteAsync(deleteOperation);
                }
            }
        }
    }
}
