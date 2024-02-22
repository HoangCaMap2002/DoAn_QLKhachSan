using Microsoft.AspNetCore.Mvc;

namespace DoAn_QLKhachSan.Controllers
{
    public class UserController : Controller
    {
        public IActionResult TrangCaNhan()
        {
            return View();
        }
        public IActionResult DoiMatKhau()
        {
            return View();
        }
    }
}
