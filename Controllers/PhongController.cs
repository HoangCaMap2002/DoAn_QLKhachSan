using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DoAn_QLKhachSan.Controllers
{
    public class PhongController : Controller
    {
        QuanLyKhachSanContext db = new QuanLyKhachSanContext();
        private readonly IPhongService _phongService;
        public PhongController(QuanLyKhachSanContext db, IPhongService phongService)
        {
            this.db = db;
            _phongService = phongService;
        }
        
        public async Task<IActionResult> PhongTheoKhachSan(int idkhachsan)
        {
            string start = HttpContext.Session.GetString("startDate");
            string end = HttpContext.Session.GetString("EndDate");
            int idtinhthanh = (int)HttpContext.Session.GetInt32("idTinhThanh");
            //Nếu không chọn ngày tháng thì lấy tất cả phòng
            if (start == null || end == null)
            {
                var phong = await _phongService.GetAllPhongByIdKhachSanAsync(idkhachsan);
                return View(phong);
            }

            var phongViewModels = await _phongService.GetAllPhongTrongByIdKhachSanAsync(idkhachsan, start, end);
            return View(phongViewModels);
        }
        public async Task<IActionResult> ChiTietPhong(int idphong)
        {
            var phong = await _phongService.GetPhongById(idphong);
            if (phong != null)
            {
                var phongViewModels = new PhongModel
                {
                    Phong = phong,
                    // Gán các thuộc tính khác của PhongViewModel
                    ImagePaths = phong.HinhAnhs.Select(filename => filename.UrlHinhAnh).ToList(),
                };
                return PartialView(phongViewModels);
            }
            return View(NotFound());
        }
    }
}
