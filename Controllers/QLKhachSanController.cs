using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DoAn_QLKhachSan.Controllers
{
    [Authorize]
    public class QLKhachSanController : Controller
    {
        private readonly IKhachSanService _khachSanService;
        private readonly IPhongService _phongService;
        private readonly QuanLyKhachSanContext _db;
        public QLKhachSanController(IKhachSanService khachSanService, IPhongService phongService, QuanLyKhachSanContext db)
        {
            _phongService = phongService;
            _khachSanService = khachSanService;
            _db = db;
        }
        public async Task<IActionResult> KhachSanTheoTaiKhoan()
        {
            var tendangnhap = HttpContext.Session.GetString("TenDangNhap");
            var rs = await _khachSanService.GetAllKhachSanByUsernameAsync(tendangnhap);
            return View(rs);
        }
        public async Task<IActionResult> DanhSachPhong(int idkhachsan)
        {
            var lst = await _phongService.GetAllPhongByIdKhachSanAsync(idkhachsan);
            return View(lst);
        }
        [HttpGet]
        public async Task<IActionResult> SuaKhachSan(int idkhachsan)
        {
            ViewBag.IdTinhThanh = new SelectList(await _db.TinhThanhs.ToListAsync(), "Id", "TenTinh");
            ViewBag.IdLoaiKhachSan = new SelectList(await _db.LoaiKhachSans.ToListAsync(), "Id", "TenLoai");
            ViewBag.IdTienNghi = new SelectList(await _db.TienNghis.ToListAsync(), "Id", "TenTienNghi");
            ViewBag.IdTienNghiTonTai = await _db.KhachSanTienNghis.Where(x => x.IdKhachSan == idkhachsan).Select(t => t.IdTienNghi).ToListAsync();
            var khachsan = await _khachSanService.GetKhachSanByIdAsync(idkhachsan);
            return View(khachsan);
        }
        [HttpPost]
        public async Task<IActionResult> SuaKhachSan(KhachSanViewModel model)
        {
                try
                {
                var test = model;
                var existingKhachSan = await _db.KhachSans.FindAsync(model.Id);
                if (model.AnhDaiDien != null && model.AnhDaiDien.Length > 0)
                {
                    string AnhChinhFileName = Guid.NewGuid().ToString() + "_" + model.AnhDaiDien.FileName;
                    string mainImagePath = Path.Combine("wwwroot/assets/images/hotels", AnhChinhFileName);

                    using (var mainImageStream = new FileStream(mainImagePath, FileMode.Create))
                    {
                        await model.AnhDaiDien.CopyToAsync(mainImageStream);
                    }
                    if (existingKhachSan != null)
                    {
                        var tiennghis = await _db.KhachSanTienNghis.Where(x => x.IdKhachSan == model.Id).ToListAsync();
                        _db.KhachSanTienNghis.RemoveRange(tiennghis);
                        await _db.SaveChangesAsync();
                        existingKhachSan.TenKhachSan = model.TenKhachSan;
                        existingKhachSan.IdLoaiKhachSan = model.IdLoaiKhachSan;
                        existingKhachSan.SoSao = model.SoSao;
                        existingKhachSan.DiaChi = model.DiaChi;
                        existingKhachSan.IdTinhThanh = model.IdTinhThanh;
                        existingKhachSan.AnhDaiDien = AnhChinhFileName;
                        existingKhachSan.GhiChu = model.GhiChu;
                        await _db.SaveChangesAsync();
                        var khachSanTienNghis = model.IdTienNghis.Select(tiennghi => new KhachSanTienNghi
                        {
                            IdTienNghi = tiennghi,
                            IdKhachSan = model.Id, // Sử dụng Id của KhachSan mới thêm vào
                        }).ToList();
                        _db.KhachSanTienNghis.AddRange(khachSanTienNghis);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    if (existingKhachSan != null)
                    {
                        var tiennghis = await _db.KhachSanTienNghis.Where(x => x.IdKhachSan == model.Id).ToListAsync();
                        _db.KhachSanTienNghis.RemoveRange(tiennghis);
                        await _db.SaveChangesAsync();
                        existingKhachSan.TenKhachSan = model.TenKhachSan;
                        existingKhachSan.IdLoaiKhachSan = model.IdLoaiKhachSan;
                        existingKhachSan.SoSao = model.SoSao;
                        existingKhachSan.DiaChi = model.DiaChi;
                        existingKhachSan.IdTinhThanh = model.IdTinhThanh;
                        existingKhachSan.AnhDaiDien = existingKhachSan.AnhDaiDien;
                        existingKhachSan.GhiChu = model.GhiChu;
                        await _db.SaveChangesAsync();
                        var khachSanTienNghis = model.IdTienNghis.Select(tiennghi => new KhachSanTienNghi
                        {
                            IdTienNghi = tiennghi,
                            IdKhachSan = model.Id, // Sử dụng Id của KhachSan mới thêm vào
                        }).ToList();
                        _db.KhachSanTienNghis.AddRange(khachSanTienNghis);
                        await _db.SaveChangesAsync();
                    }
                }
                    return RedirectToAction("KhachSanTheoTaiKhoan", "QLKhachSan");
                }
                catch (Exception)
                {
                    // Xử lý khi xảy ra lỗi
                    return RedirectToAction("Error", "Home");
                }
        }
        public async Task<IActionResult> XoaKhachSan(int idkhachsan)
        {
            var ks = await _khachSanService.GetKhachSanByIdAsync(idkhachsan);
            ks.IsDelete = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("KhachSanTheoTaiKhoan", "QLKhachSan");
        }
        [HttpGet]
        public async Task<IActionResult> ThemPhong(int idkhachsan)
        {
            ViewBag.IdTienNghi = new SelectList(await _db.TienNghis.ToListAsync(), "Id", "TenTienNghi");
            ViewBag.IdKhachSan = idkhachsan;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ThemPhong(PhongViewModel model)
        {
            ViewBag.IdTienNghi = new SelectList(await _db.TienNghis.ToListAsync(), "Id", "TenTienNghi");
            string AnhChinhFileName = Guid.NewGuid().ToString() + "_" + model.AnhDaiDien.FileName;
            string mainImagePath = Path.Combine("wwwroot/assets/images/phong", AnhChinhFileName);
            using (var mainImageStream = new FileStream(mainImagePath, FileMode.Create))
            {
                await model.AnhDaiDien.CopyToAsync(mainImageStream);
            }
            var phong = new Phong
            {
                TenPhong = model.TenPhong,
                GiaPhong = model.GiaPhong,
                IdKhachSan = model.IdKhachSan,
                AnhDaiDien = AnhChinhFileName,
                IsDelete= false,
            };
            _db.Phongs.Add(phong);
            await _db.SaveChangesAsync();

            var idphong = phong.Id;
            if (model.IdTienNghiPhong != null)
            {
                var phongTienNghis = model.IdTienNghiPhong.Select(tiennghi => new PhongTienNghi
                {
                    IdTienNghi = tiennghi,
                    IdPhong = idphong,
                }).ToList();
                _db.PhongTienNghis.AddRange(phongTienNghis);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("KhachSanTheoTaiKhoan", "QLKhachSan");
        }
        [HttpGet]
        public async Task<IActionResult> SuaPhong(int idphong)
        {
            var phongs = await _phongService.GetPhongById(idphong);
            ViewBag.IdTienNghi = new SelectList(await _db.TienNghis.ToListAsync(), "Id", "TenTienNghi");
            ViewBag.IdTienNghiTonTai = await _db.PhongTienNghis.Where(x => x.IdPhong == idphong).Select(t => t.IdTienNghi).ToListAsync();
            return View(phongs);
        }
        [HttpPost]
        public async Task<IActionResult> SuaPhong(PhongViewModel model)
        {
            try
            {
                var test = model;
                var existingPhong = await _db.Phongs.FindAsync(model.Id);
                if (model.AnhDaiDien != null && model.AnhDaiDien.Length > 0)
                {
                    // Nếu có tệp tin mới, lưu tệp tin mới và sử dụng tên mới
                    string AnhChinhFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.AnhDaiDien.FileName);
                    string mainImagePath = Path.Combine("wwwroot/assets/images/phong", AnhChinhFileName);

                    using (var mainImageStream = new FileStream(mainImagePath, FileMode.Create))
                    {
                        await model.AnhDaiDien.CopyToAsync(mainImageStream);
                    }

                    // Thực hiện các xử lý cần thiết với ảnh mới
                    if (existingPhong != null)
                    {
                        var tiennghis = await _db.PhongTienNghis.Where(x => x.IdPhong == model.Id).ToListAsync();
                        _db.PhongTienNghis.RemoveRange(tiennghis);
                        existingPhong.TenPhong = model.TenPhong;
                        existingPhong.GiaPhong = model.GiaPhong;
                        existingPhong.AnhDaiDien = AnhChinhFileName;
                        await _db.SaveChangesAsync();
                        var phongTienNghis = model.IdTienNghiPhong.Select(tiennghi => new PhongTienNghi
                        {
                            IdTienNghi = tiennghi,
                            IdPhong = model.Id, 
                        }).ToList();
                        _db.PhongTienNghis.AddRange(phongTienNghis);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    if (existingPhong != null)
                    {
                        var tiennghis = await _db.PhongTienNghis.Where(x => x.IdPhong == model.Id).ToListAsync();
                        _db.PhongTienNghis.RemoveRange(tiennghis);
                        existingPhong.TenPhong = model.TenPhong;
                        existingPhong.GiaPhong = model.GiaPhong;
                        existingPhong.AnhDaiDien = existingPhong.AnhDaiDien;
                        await _db.SaveChangesAsync();
                        var phongTienNghis = model.IdTienNghiPhong.Select(tiennghi => new PhongTienNghi
                        {
                            IdTienNghi = tiennghi,
                            IdPhong = model.Id,
                        }).ToList();
                        _db.PhongTienNghis.AddRange(phongTienNghis);
                        await _db.SaveChangesAsync();
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                
                return RedirectToAction("KhachSanTheoTaiKhoan", "QLKhachSan");
            }
            catch (Exception)
            {
                // Xử lý khi xảy ra lỗi
                return RedirectToAction("Error", "Home");
            }
        }
        public async Task<IActionResult> XoaPhong(int idphong)
        {
            var phong = await _phongService.GetPhongById(idphong);
            phong.IsDelete = true;
            await _db.SaveChangesAsync();
            return RedirectToAction("DanhSachPhong", "QLKhachSan", new { idkhachsan = phong.IdKhachSan});
        }
        [HttpGet]
        public async Task<IActionResult> UploadImage(int idphong)
        {
            ViewBag.IdPhong = idphong;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage(int IdPhong, List<IFormFile?> DanhSachAnh)
        {
            if (DanhSachAnh != null)
            {
                foreach (var item in DanhSachAnh)
                {
                    string AnhFileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    string mainImagePath = Path.Combine("wwwroot/assets/images/phong", AnhFileName);
                    using (var mainImageStream = new FileStream(mainImagePath, FileMode.Create))
                    {
                        await item.CopyToAsync(mainImageStream);
                    }
                    var anh = new HinhAnh
                    {
                        IdPhong= IdPhong,
                        UrlHinhAnh = AnhFileName,
                        IsDelete = false,  
                    };
                    _db.HinhAnhs.Add(anh);
                }
                await _db.SaveChangesAsync();
                return RedirectToAction("KhachSanTheoTaiKhoan", "QLKhachSan");
            }
           
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> LichSuDat()
        {
            var tendangnhap = HttpContext.Session.GetString("TenDangNhap");
            if (tendangnhap !=null)
            {
                var datphong = await _db.DatPhongs.Where(x => x.TenDangNhap == tendangnhap).ToListAsync();
                List<LichSuDatVM> lichsu = new List<LichSuDatVM>();
                foreach (var item in datphong)
                {
                    var phong = await _db.Phongs.Where(x => x.Id == item.IdPhong).FirstOrDefaultAsync();
                    var khachsan = await _db.KhachSans.Where(x => x.Id == phong.IdKhachSan).FirstOrDefaultAsync();
                    var ls = new LichSuDatVM
                    {
                        BatDau = item.BatDau,
                        KetThuc = item.KetThuc,
                        TenNguoiDat = item.HoVaTen,
                        IdPhong = item.IdPhong,
                        SoDienThoai= item.SoDienThoai,
                        Email = item.Email,
                        GhiChu = item.GhiChu,
                        ThanhToan = item.ThanhToan,
                        Status = item.Status,
                        AnhDaiDien = phong.AnhDaiDien,
                        TongTien = item.TongTien,
                       TenKhachSan = khachsan.TenKhachSan
                    };
                    lichsu.Add(ls);
                }
                return View(lichsu);
            }
            
            return View();
        }

    }
}
