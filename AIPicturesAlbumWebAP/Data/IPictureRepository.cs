using AIPicturesAlbumWebAP.Data.Model;

namespace AIPicturesAlbumWebAP.Data
{
    public interface IPictureRepository
    {
        Task AddPicData(PictureData pictureData, Stream stream);
        Task<PictureData> GetPictureData(string rowkey);
        Task<List<PictureData>> GetPictureDataList();
        Task<List<PictureData>> GetAllThumbPicData();
        string GetImageUrlByRowKey(string rowkey);
    }
}
