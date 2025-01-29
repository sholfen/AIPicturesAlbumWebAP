using AIPicturesAlbumWebAP.Data.Model;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using System.ComponentModel;

namespace AIPicturesAlbumWebAP.Data
{
    public class PictureRepository : IPictureRepository
    {
        private readonly string _tableName = "AzurePicTable";
        private static readonly string PartitionKey = "PictureData";
        public static string ContainerName = string.Empty;
        public static string BlobConnectionString = string.Empty;
        public static string TableConnectionString = string.Empty;

        public static string CDNDomain = string.Empty;

        private TableClient? _cosmosClient = null;

        private BlobContainerClient? _blobContainerClient = null;

        public PictureRepository()
        {
            Init();
        }

        public PictureRepository(string connectionString)
        {
            Init();
        }

        private void Init()
        {
            TableServiceClient tableServiceClient = new TableServiceClient(TableConnectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: _tableName);

            tableClient.CreateIfNotExistsAsync().Wait();
            _cosmosClient = tableClient;

            _blobContainerClient = new BlobContainerClient(BlobConnectionString, ContainerName);
            _blobContainerClient.CreateIfNotExists();
        }

        public List<AzureTestData> GetAzureTestDatas()
        {
            var azureTestDatas = _cosmosClient.Query<AzureTestData>(x => x.PartitionKey == PartitionKey);
            return azureTestDatas.ToList();
        }

        public async Task AddData(AzureTestData data)
        {
            await _cosmosClient.AddEntityAsync<AzureTestData>(data);
        }

        public async Task AddPicData(PictureData pictureData, Stream stream)
        {
            var response = await _blobContainerClient.UploadBlobAsync(pictureData.FileName, stream);
           // pictureData.ImageUrl = $"{CDNDomain}/{ContainerName}/{pictureData.FileName}";
            await _cosmosClient.AddEntityAsync<PictureData>(pictureData);
        }

        private List<string> GetAllRowKey()
        {
            var pictureDatas = _cosmosClient.Query<PictureData>(p => p.PartitionKey == PartitionKey);
            return pictureDatas.Select(p => p.RowKey).ToList();
        }

        public async Task<PictureData> GetPictureData(string rowkey)
        {
            PictureData pictureData = await _cosmosClient.GetEntityAsync<PictureData>(PartitionKey, rowkey);
            return pictureData;
        }

        public async Task<List<PictureData>> GetPictureDataList()
        {
            List<PictureData> list = _cosmosClient.Query<PictureData>(p => p.PartitionKey == PartitionKey).ToList();

            list = list.Where(p => !string.IsNullOrEmpty(p.ThumbUrl))
                .OrderByDescending(p => p.Timestamp)
                .ToList();

            return list;
        }

        public async Task<List<PictureData>> GetAllThumbPicData()
        {
            List<PictureData> list = _cosmosClient.Query<PictureData>(p => p.PartitionKey == PartitionKey)
                .ToList();

            list = list.Where(p => string.IsNullOrEmpty(p.ThumbUrl))
                .OrderByDescending(p=>p.Timestamp)
                .ToList();

            return list;
        }

        public string GetImageUrlByRowKey(string rowkey)
        {
            var data = _cosmosClient.Query<PictureData>(p => p.PartitionKey == PartitionKey && p.RowKey == rowkey).FirstOrDefault();
            return data.ImageUrl;
        }
    }
}
