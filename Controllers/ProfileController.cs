using DoAn_QLKhachSan.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_QLKhachSan.Controllers
{
    public class ProfileController : Controller
    {
        private readonly QuanLyKhachSanContext _context;

        public ProfileController(QuanLyKhachSanContext context) {
            _context = context;
        }
        public IActionResult ThongTinTK()
        {
            return View();
        }
    }
}
