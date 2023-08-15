namespace AIPicturesAlbumWebAP.Data
{
    public interface IPictureAppService
    {
        Task AddPicData(PictureData pictureData, Stream stream);
        Task<List<PictureData>> GetAllPicData();
    }
}
