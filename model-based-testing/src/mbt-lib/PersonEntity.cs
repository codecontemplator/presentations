using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace mbt_lib
{
    public class PersonEntity : TableEntity
    {
        ///// <summary>
        ///// National Id
        ///// </summary>
        //public string RowKey { get; set; }

        ///// <summary>
        ///// Country
        ///// </summary>
        //public string PartitionKey { get; set; }

        public string Name { get; set; }

        public string EyeColor { get; set; }
    }
}
