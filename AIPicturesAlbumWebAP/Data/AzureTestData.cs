using Azure.Data.Tables;
using Azure;

namespace AIPicturesAlbumWebAP.Data
{
    public record AzureTestData : ITableEntity
    {
        public string RowKey { get; set; } = Guid.NewGuid().ToString();

        public string PartitionKey { get; set; } = "AzureTestData";

        public ETag ETag { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public string Name { get; init; } = default!;

        public int Age { get; init; }

        public string Desc { get; init; } = string.Empty;
    }
}
