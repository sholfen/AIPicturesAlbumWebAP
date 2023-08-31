using AIPicturesAlbumWebAP.Data;
using Microsoft.AspNetCore.Mvc;

namespace AIPicturesAlbumWebAP.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserAppService _userAppService;

        public AuthController(IUserAppService userAppService) 
        {
            _userAppService = userAppService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginModel loginModel) 
        {
            var isLogin = await _userAppService.Login(loginModel.UserName, loginModel.Password);
            if (isLogin)
            {
                return Redirect("/UploadPic");
            }

            return Redirect("/Login");
        }
    }
}
