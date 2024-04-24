using DoAn_QLKhachSan.Extension;
using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebBanGiay.Helper;

namespace DoAn_QLKhachSan.Controllers
{
    [Authorize]
    public class DatPhongController : Controller
    {
        private readonly IPhongService _phongService;
        private readonly IVnPayService _vnPayService;
        private readonly QuanLyKhachSanContext _context;

        public DatPhongController(IPhongService phongService, IVnPayService vnPayService, QuanLyKhachSanContext context)
        {
            _phongService = phongService;
            _vnPayService = vnPayService;
            _context = context;
        }

        //Cart
        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ThongTinDatPhong()
        {
            return View(Carts);
        }
        public async Task<IActionResult> BookPhong(int id)
        {
            var start = HttpContext.Session.GetString("startDate");
            var end = HttpContext.Session.GetString("EndDate");
            if (start == null || end == null || start =="" || end == "")
            {
                TempData["ErrorMessage"] = "Vui lòng nhập vào cả ngày bắt đầu và ngày kết thúc.";
                return Redirect("/Home/Index");
            }
            else
            {
                var myCart = Carts;
                var item = myCart.SingleOrDefault(p => p.Id == id);

                if (item == null)
                {
                    var phong = await _phongService.GetPhongById(id);
                    var khachsan = _context.KhachSans.Where(x => x.Id == phong.IdKhachSanNavigation.Id).SingleOrDefault();
                    item = new CartItem
                    {
                        Id = phong.Id,
                        TenPhong = phong.TenPhong,
                        GiaPhong = phong.GiaPhong,
                        AnhDaiDien = phong.AnhDaiDien,
                        SoSao = khachsan.SoSao,
                        DiaChi  = khachsan.DiaChi,
                        BatDau = start,
                        KetThuc = end,
                    };
                    myCart.Add(item);
                }
                HttpContext.Session.Set("GioHang", myCart);
            }
            return RedirectToAction("ThongTinDatPhong");
        }
        [HttpPost]
        public async Task<IActionResult> ThongTinDatPhong(ThongTinDatPhong tt, string payment = "COD")
        {
            HttpContext.Session.SetString("SoDienThoai", tt.SoDienThoai);
            HttpContext.Session.SetString("Email", tt.Email);
            HttpContext.Session.SetString("HoVaTen", tt.HoVaTen);
            HttpContext.Session.SetString("GhiChu", tt.GhiChu);
            if (payment == "Thanh toán VNPay")
            {
                var vnPayModel = new VnPaymentRequestModel
                {
                    Amount = Carts.Sum(x => x.TongTien()),
                    CreatedDate = DateTime.Now,
                    Description = $"{tt.HoVaTen} {tt.SoDienThoai}",
                    FullName = tt.HoVaTen,
                };
                return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
            }
            var tdn = HttpContext.Session.GetString("TenDangNhap");
            var rs = await _phongService.DatPhong(Carts, tdn, tt);
            if (rs == true)
            {
                return RedirectToAction("ThanhCong");
            }
            return RedirectToAction("Thanhcong", "DatPhong");
        }
        public IActionResult Thanhcong()
        {
            Carts.Clear();
            return View();
        }
    }
}
