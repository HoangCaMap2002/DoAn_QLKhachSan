using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DoAn_QLKhachSan.Controllers
{
    public class UserController : Controller
    {
        private readonly ITaiKhoanService _taiKhoanService;
        private readonly IKhachSanService _khachSanService;
        private readonly IPhongService _phongService;
        private readonly QuanLyKhachSanContext _db;

        public UserController(ITaiKhoanService taiKhoanService, QuanLyKhachSanContext db, IKhachSanService khachSanService, IPhongService phongService)
        {
            _taiKhoanService= taiKhoanService;
            _khachSanService = khachSanService;
            _phongService = phongService;   
            _db = db;
        }
        public async Task<IActionResult> YourPartialAction()
        {
            string tendangnhap = HttpContext.Session.GetString("TenDangNhap");
            if (tendangnhap != null)
            {
                var tk = await _taiKhoanService.LayTaiKhoanTheoTenTK(tendangnhap);
                var sokhachsan = await _db.KhachSans.Where(x => x.NguoiQuanLy == tendangnhap && x.IsDelete == false).ToListAsync();
                //Số lượt đặt
                var total = 0;
                var khacsans = await _khachSanService.GetAllKhachSanByUsernameAsync(tendangnhap);
                foreach (var ks in khacsans)
                {
                    var phongs = await _phongService.GetAllPhongByIdKhachSanAsync(ks.Id);
                    foreach (var phong in phongs)
                    {
                        var luotdat = _db.DatPhongs.Count(x => x.IdPhong == phong.Phong.Id);
                        total = total + luotdat;
                    }
                }
                //Số lượt đánh giá
                var totaldanhgia = 0;
                foreach (var ks in khacsans)
                {
                    var danhgia = await _db.DanhGia.Where(x => x.IdKhachSan == ks.Id).ToListAsync();
                    totaldanhgia = totaldanhgia + danhgia.Count();
                }
                var model = new ThongKeProfileViewModel
                {
                    HoVaTen = tk.HoVaTen,
                    Anh = tk.Anh,
                    SoKhachSan = sokhachsan.Count(),
                    SoLuotDat = total,
                    SoLuotDanhGia = totaldanhgia,
                };
                return PartialView("_PatialProfile", model);
            }
            return View(); 
        }
        public async Task<IActionResult> TrangCaNhan()
        {
            string tendangnhap = HttpContext.Session.GetString("TenDangNhap");
            var tk = await _taiKhoanService.LayTaiKhoanTheoTenTK(tendangnhap);
            var profile = new ProfileViewModel
            {
                HoVaTen = tk.HoVaTen,
                SoDienThoai = tk.SoDienThoai,
            };
            return View(profile);
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
        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileViewModel model)
        {
            if(ModelState.IsValid)
            {
                string tendangnhap =  HttpContext.Session.GetString("TenDangNhap");
                var tk = await _taiKhoanService.LayTaiKhoanTheoTenTK(tendangnhap);
                if (tk != null)
                {
                    if (model.Anh != null && model.Anh.Length > 0)
                    {
                        string AnhChinhFileName = Guid.NewGuid().ToString() + "_" + model.Anh.FileName;
                        string mainImagePath = Path.Combine("wwwroot/assets/images/avatar", AnhChinhFileName);

                        using (var mainImageStream = new FileStream(mainImagePath, FileMode.Create))
                        {
                            await model.Anh.CopyToAsync(mainImageStream);
                        }
                        tk.HoVaTen = model.HoVaTen;
                        tk.SoDienThoai = model.SoDienThoai;
                        tk.Anh = AnhChinhFileName;
                        await _db.SaveChangesAsync();
                        return RedirectToAction("TrangCaNhan", "User");
                    }
                    else
                    {
                        tk.HoVaTen = model.HoVaTen;
                        tk.SoDienThoai = model.SoDienThoai;
                        await _db.SaveChangesAsync();
                        return RedirectToAction("TrangCaNhan", "User");
                    }
                }
            }
            return View();
        }
        public IActionResult DanhGia(int diemsachse, int diemthom, int diemnhanvien, int diemcsvc, double dtb, string comment, int idks)
        {
            string tendangnhap = HttpContext.Session.GetString("TenDangNhap");

            var danhgia = new DanhGium
            {
                TenDangNhap = tendangnhap,
                IdKhachSan = idks,
                Comment = comment,
                NgayComment = DateTime.Now,
                DiemNhanVien= diemnhanvien,
                DiemThom = diemthom,
                DiemCoSoVatChat= diemcsvc,
                DiemSachSe = diemsachse,
            };
            _db.Add(danhgia);
            _db.SaveChanges();
            return Json(new { success = true, message = "Đánh giá thành công" });
        }
    }
}
