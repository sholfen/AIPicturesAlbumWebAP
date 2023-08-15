namespace AIPicturesAlbumWebAP.Data
{
    public interface IPictureRepository
    {
        Task AddPicData(PictureData pictureData, Stream stream);
        Task<PictureData> GetPictureData(string rowkey);
        Task<List<PictureData>> GetPictureDataList();
    }
}
