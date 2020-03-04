using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace mbt_lib
{
    public class PersonEntity : TableEntity
    {
        // RowKey === National Id
        // PartitionKey === Country

        public string Name { get; set; }

        public string EyeColor { get; set; }
    }
}
