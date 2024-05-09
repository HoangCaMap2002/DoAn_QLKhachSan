using DoAn_QLKhachSan.Extension;
using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanGiay.Helper;

namespace DoAn_QLKhachSan.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly IPhongService _phongService;
        private readonly QuanLyKhachSanContext _context;

        public PaymentController(IVnPayService vnPayService, IPhongService phongService, QuanLyKhachSanContext context)
        {
            _vnPayService = vnPayService;
            _phongService = phongService;
            _context = context;
        }
        [Authorize]
        public IActionResult PaymentCallBack()
        {
            var user = HttpContext.Session.GetString("TenDangNhap");
            var start = HttpContext.Session.GetString("startDate");
            var end = HttpContext.Session.GetString("EndDate");
            var sdt = HttpContext.Session.GetString("SoDienThoai");
            var email = HttpContext.Session.GetString("Email");
            var hovaten = HttpContext.Session.GetString("HoVaTen");
            var ghichu = HttpContext.Session.GetString("GhiChu");
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VNpay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }
            //Lưu đơn hàng
            var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
            foreach (var item in data)
            {
                var don = new DatPhong
                {
                    TenDangNhap = user,
                    IdPhong = item.Id,
                    BatDau = start.ToDateTime(),
                    KetThuc = end.ToDateTime(),
                    ThanhToan = "đã thanh toán",
                    SoDienThoai = sdt,
                    Email = email,
                    HoVaTen = hovaten,
                    GhiChu= ghichu,
                    TongTien = item.TongTien(),
                    NgayDat = DateTime.Now,
                    IdTrangThai = 4
                };
                _context.Add(don);
            }
            _context.SaveChanges();
            TempData["Message"] = $"Thanh toán VNpay thành công: {response.VnPayResponseCode}";
            return RedirectToAction("ThanhCong", "DatPhong");
        }

        public IActionResult PaymentFail()
        {
            return View();
        }
    }
}
