namespace AIPicturesAlbumWebAP.Data
{
    public interface IUserAppService
    {
        Task<bool> Login(string username, string password);
        Task Logout();
        bool CheckLoginState();
    }
}
