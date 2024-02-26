using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_QLKhachSan.Controllers
{
    public class UserController : Controller
    {
        private readonly ITaiKhoanService _taiKhoanService;
        public UserController(ITaiKhoanService taiKhoanService)
        {
            _taiKhoanService= taiKhoanService;
        }
        public IActionResult TrangCaNhan()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> DoiMatKhau()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DoiMatKhau(DoiMKVM model)
        {
            var tendangnhap = HttpContext.Session.GetString("TenDangNhap");
            model.TenDangNhap = tendangnhap;
            var rs = await _taiKhoanService.DoiMatKhau(model);
            if (!rs)
            {
                return View();
            }
            return Redirect("/Access/Login");
        }
    }
}
