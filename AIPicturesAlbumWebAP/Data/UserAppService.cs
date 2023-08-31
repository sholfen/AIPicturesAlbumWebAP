using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AIPicturesAlbumWebAP.Data
{
    public class UserAppService : IUserAppService
    {
        private IHttpContextAccessor _httpContextAccessor;

        public UserAppService(IHttpContextAccessor httpContextAccessor) 
        {
            _httpContextAccessor = httpContextAccessor;
            //_signInManager = signInManager;
        }

        public async Task<bool> Login(string username, string password)
        {
            if (UserData.UserName == username && UserData.UserPassword == password)
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, username)
                }, "auth");
                ClaimsPrincipal claims = new ClaimsPrincipal(claimsIdentity);
                await _httpContextAccessor.HttpContext.SignInAsync(claims);
                //await _signInManager.SignInAsync(claims, true);
                return true;
            }
            return false;
        }

        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync();
            //await _signInManager.SignOutAsync();
        }

        public bool CheckLoginState()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return true;
            }
            return false; 
        }
    }
}
