using DoAn_QLKhachSan.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_QLKhachSan.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = "Admin")]
    public class HomeAdminController : Controller
    {
        QuanLyKhachSanContext db = new QuanLyKhachSanContext();
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]  
        public IActionResult DanhSachKhachSan()
        {
            var khachsan = db.KhachSans.ToList();
            return View(khachsan);
        }
        [HttpPost]
        public IActionResult SuaTrangThai(int idkhachsan, bool isdelete)
        {
            var khachsan = db.KhachSans.Find(idkhachsan);
            khachsan.IsDelete = isdelete;
            db.SaveChanges();
            return Json(new { success = true, message = "Sửa thành công" });
        }
    }
}
