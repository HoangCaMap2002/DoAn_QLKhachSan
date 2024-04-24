using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DoAn_QLKhachSan.Controllers
{
    public class KhachSanController : Controller
    {
        QuanLyKhachSanContext db = new QuanLyKhachSanContext();
        private readonly IKhachSanService _khachSanService;
        public KhachSanController(QuanLyKhachSanContext db, IKhachSanService khachSanService)
        {
            this.db = db;
            _khachSanService = khachSanService;
        }

        public async Task<IActionResult> KhachSanTheoTinh(int idtinhthanh)
        {
            HttpContext.Session.SetInt32("idTinhThanh", idtinhthanh);
            var khachsan = await _khachSanService.GetAllKhachSanByIdTinhAsync(idtinhthanh);
            ViewBag.TinhThanh = await db.TinhThanhs.Select(t => t.TenTinh).FirstOrDefaultAsync();
            return View(khachsan);
        }
        public async Task<IActionResult> KiemTraKhachSanConPhong(string start, string end, int city)
        {
            HttpContext.Session.SetInt32("idTinhThanh", city);
            HttpContext.Session.SetString("startDate", start);
            HttpContext.Session.SetString("EndDate", end);
            var result = await _khachSanService.GetKhachSanPhongTrongAsync(start, end, city);
            return View(result);
        }

        public IActionResult DanhGiaKhachSan(int idkhachsan)
        {
            var danhgias = db.DanhGia.Where(x => x.IdKhachSan == idkhachsan).ToList();
            return PartialView("_PatialDanhGia", danhgias);
        }

       
    }
}
