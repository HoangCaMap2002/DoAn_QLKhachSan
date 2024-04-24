using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace DoAn_QLKhachSan.Controllers
{
    [Authorize]
    public class DangKyKhachSanController : Controller
    {
        QuanLyKhachSanContext db = new QuanLyKhachSanContext();
      
        public IActionResult Index()
        {
            ViewBag.IdTinhThanh = new SelectList(db.TinhThanhs.ToList(), "Id", "TenTinh");
            ViewBag.IdLoaiKhachSan = new SelectList(db.LoaiKhachSans.ToList(), "Id", "TenLoai");
            ViewBag.IdTienNghi = new SelectList(db.TienNghis.ToList(), "Id", "TenTienNghi");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ThemKhachSan(KhachSanPhongViewModel model)
        {
            try
            {
                // Xử lý logic lưu trữ ảnh đại diện chính
                string AnhChinhFileName = Guid.NewGuid().ToString() + "_" + model.AnhDaiDien.FileName;
                string mainImagePath = Path.Combine("wwwroot/assets/images/hotels", AnhChinhFileName);

                using (var mainImageStream = new FileStream(mainImagePath, FileMode.Create))
                {
                    await model.AnhDaiDien.CopyToAsync(mainImageStream);
                }

                // Tạo một thực thể KhachSan mới
                var ks = new KhachSan
                {
                    IdTinhThanh = model.IdTinhThanh,
                    TenKhachSan = model.TenKhachSan,
                    DiaChi = model.DiaChi,
                    GhiChu = model.GhiChu,
                    NguoiQuanLy = HttpContext.Session.GetString("TenDangNhap"),
                    SoSao = model.SoSao,
                    IdLoaiKhachSan = model.IdLoaiKhachSan,
                    AnhDaiDien = AnhChinhFileName,
                };

                // Thêm KhachSan mới vào cơ sở dữ liệu
                db.KhachSans.Add(ks);
                await db.SaveChangesAsync();

                // Lấy Id của KhachSan vừa thêm vào
                var khachSanId = ks.Id;

                // Tạo danh sách KhachSanTienNghi từ IdTienNghis
                var khachSanTienNghis = model.IdTienNghis.Select(tiennghi => new KhachSanTienNghi
                {
                    IdTienNghi = tiennghi,
                    IdKhachSan = khachSanId, // Sử dụng Id của KhachSan mới thêm vào
                }).ToList();

                // Thêm danh sách KhachSanTienNghi vào cơ sở dữ liệu
                db.KhachSanTienNghis.AddRange(khachSanTienNghis);
                await db.SaveChangesAsync();

                return Json(new { success = true, message = "Thêm thành công", IdKhachSan = khachSanId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> CapNhatKhachSan(KhachSanPhongViewModel model)
        {
            try
            {
                int khachSanId = (int)model.IdKhachSan;
                var khachSan = await db.KhachSans.FindAsync(khachSanId);
                var tiennghis = await db.KhachSanTienNghis.Where(x => x.IdKhachSan == khachSanId).ToListAsync();
                db.KhachSanTienNghis.RemoveRange(tiennghis);
                await db.SaveChangesAsync();
                if (khachSan == null)
                {
                    // Xử lý khi không tìm thấy khách sạn
                    return NotFound();
                }
                string AnhChinhFileName = Guid.NewGuid().ToString() + "_" + model.AnhDaiDien.FileName;
                string mainImagePath = Path.Combine("wwwroot/assets/images/hotels", AnhChinhFileName);

                using (var mainImageStream = new FileStream(mainImagePath, FileMode.Create))
                {
                    await model.AnhDaiDien.CopyToAsync(mainImageStream);
                }
                khachSan.IdTinhThanh = model.IdTinhThanh;
                khachSan.TenKhachSan = model.TenKhachSan;
                khachSan.DiaChi = model.DiaChi;
                khachSan.GhiChu = model.GhiChu;
                khachSan.SoSao = model.SoSao;
                khachSan.IdLoaiKhachSan = model.IdLoaiKhachSan;
                var khachSanTienNghis = model.IdTienNghis.Select(tiennghi => new KhachSanTienNghi
                {
                    IdTienNghi = tiennghi,
                    IdKhachSan = khachSanId, // Sử dụng Id của KhachSan mới thêm vào
                }).ToList();

                // Thêm danh sách KhachSanTienNghi vào cơ sở dữ liệu
                db.KhachSanTienNghis.AddRange(khachSanTienNghis);
                await db.SaveChangesAsync();
                return Json(new { success = true, message = "Sửa thành công", IdKhachSan = khachSanId });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> ThemPhong(KhachSanPhongViewModel model) {

            try
            {
                var count = model.Phongs.Count;
                foreach (var item in model.Phongs)
                {
                    string AnhChinhFileName = Guid.NewGuid().ToString() + "_" + item.AnhDaiDien.FileName;
                    string mainImagePath = Path.Combine("wwwroot/assets/images/phong", AnhChinhFileName);
                    using (var mainImageStream = new FileStream(mainImagePath, FileMode.Create))
                    {
                        await item.AnhDaiDien.CopyToAsync(mainImageStream);
                    }
                    var phong = new Phong
                    {
                        TenPhong = item.TenPhong,
                        GiaPhong = item.GiaPhong,
                        IdKhachSan = model.IdKhachSan,
                        AnhDaiDien = AnhChinhFileName,
                    };
                    db.Phongs.Add(phong);
                    await db.SaveChangesAsync();

                    var idphong = phong.Id;
                    var phongtiennghi = model.Phongs[0].IdTienNghiPhong.Select(tiennghi => new PhongTienNghi
                    {
                        IdTienNghi = tiennghi,
                        IdPhong = idphong,
                    }).ToList();

                    // Thêm danh sách KhachSanTienNghi vào cơ sở dữ liệu
                    db.PhongTienNghis.AddRange(phongtiennghi);
                    await db.SaveChangesAsync();
                }
                return Json(new { success = true, message = "Thêm phòng thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Message = ex.Message });
            }
        }
    }
}
