using AIPicturesAlbumWebAP.Data.Model;

namespace AIPicturesAlbumWebAP.Data
{
    public interface IPictureAppService
    {
        Task AddPicData(PictureData pictureData, Stream stream);
        Task AddThumbPicData(PictureData pictureData, Stream stream);
        Task<List<PictureData>> GetAllPicData();
        Task<List<PictureData>> GetAllThumbPicData();
    }
}
