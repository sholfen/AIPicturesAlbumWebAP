using Azure;
using Azure.Data.Tables;

namespace AIPicturesAlbumWebAP.Data
{
    public record PictureData : ITableEntity
    {
        public string RowKey { get; set; } = Guid.NewGuid().ToString();

        public string PartitionKey { get; set; } = "PictureData";

        public ETag ETag { get; set; } = default!;

        public DateTimeOffset? Timestamp { get; set; } = default!;

        public string Title { get; init; } = string.Empty;

        public string Desc { get; init; } = string.Empty;

        public string ImageUrl { get; init; } = string.Empty;
    }
}
