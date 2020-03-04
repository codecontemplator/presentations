using FsCheck;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace mbt_test
{
    public class PersonStoreTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [FsCheck.NUnit.Property(MaxTest = 15, EndSize = 15, Replay = "867624238,296714373")]
        public Property PersonStoreShouldBehaveAsSimulator() => (new PersonStoreCommandGenerator()).ToProperty();

        [Test]
        public async Task DeleteTempTables()
        {
            CloudStorageAccount storageAccount;
            storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("DOTNET_StoreConfig__ConnectionString"));
            var tableClient = storageAccount.CreateCloudTableClient();
            TableContinuationToken token = null;
            do
            {
                var tablesToDelete = await tableClient.ListTablesSegmentedAsync("TempPersonEntities", token);
                foreach(var table in tablesToDelete)
                {
                    await table.DeleteAsync();
                }
            } while (token != null);
        }
    }
}