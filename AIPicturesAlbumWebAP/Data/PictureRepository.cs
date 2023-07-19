using Azure.Data.Tables;

namespace AIPicturesAlbumWebAP.Data
{
    public class PictureRepository : IPictureRepository
    {
        private readonly string _tableName = "AzureTestTable";

        //private readonly string _connectionString = "DefaultEndpointsProtocol=https;AccountName=voicedubbing;AccountKey=zTHm2subJmv5zVXGphM9cXsD7R87ABKQYFTJrJcxnxirZ6/RMHUKkzsJIOaRQzygH3yLiWXPusmy+AStazzeoA==;EndpointSuffix=core.windows.net";

        private readonly string _connectionString = string.Empty;

        private TableClient _cosmosClient;

        public PictureRepository()
        {
            Init();
        }

        public PictureRepository(string connectionString)
        {
            _connectionString = connectionString;
            Init();
        }

        private void Init()
        {
            TableServiceClient tableServiceClient = new TableServiceClient(_connectionString);
            TableClient tableClient = tableServiceClient.GetTableClient(tableName: _tableName);

            tableClient.CreateIfNotExistsAsync().Wait();
            _cosmosClient = tableClient;
        }

        public List<AzureTestData> GetAzureTestDatas()
        {
            var azureTestDatas = _cosmosClient.Query<AzureTestData>(x => x.PartitionKey == "AzureTestData");
            return azureTestDatas.ToList();
        }

        public async Task AddData(AzureTestData data)
        {
            await _cosmosClient.AddEntityAsync<AzureTestData>(data);
        }
    }
}
