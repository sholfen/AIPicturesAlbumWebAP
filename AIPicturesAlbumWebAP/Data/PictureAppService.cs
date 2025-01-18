using AIPicturesAlbumWebAP.Data.Model;

namespace AIPicturesAlbumWebAP.Data
{
    public class PictureAppService : IPictureAppService
    {
        private IPictureRepository _pictureRepository;

        public PictureAppService(IPictureRepository pictureRepository) 
        {
            _pictureRepository = pictureRepository;
        }

        public async Task AddPicData(PictureData pictureData, Stream stream)
        {
            await _pictureRepository.AddPicData(pictureData, stream);
        }

        public async Task AddThumbPicData(PictureData pictureData, Stream stream)
        {
            await _pictureRepository.AddPicData(pictureData, stream);
        }

        public async Task<List<PictureData>> GetAllPicData()
        {
            List<PictureData> pictureDatas = await _pictureRepository.GetPictureDataList();

            return pictureDatas;
        }

        public async Task<List<PictureData>> GetAllThumbPicData()
        {
            return await _pictureRepository.GetAllThumbPicData();
        }
    }
}
