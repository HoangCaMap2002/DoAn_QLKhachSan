using DoAn_QLKhachSan.Extension;
using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using WebBanGiay.Helper;

namespace DoAn_QLKhachSan.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IVnPayService _vnPayService;
        private readonly IPhongService _phongService;
        private readonly QuanLyKhachSanContext _context;

        private void SendConfirmationEmail(ThongTinDatPhong bookingModel, string customerEmail, string paymentMethod)
        {
            string subject;
            string paymentInfo;
            var start = HttpContext.Session.GetString("startDate");
            var end = HttpContext.Session.GetString("EndDate");
            // Tạo nội dung email
            var tenks = (from ks in _context.KhachSans
                         join p in _context.Phongs on ks.Id equals p.IdKhachSan
                         where p.Id == bookingModel.Phong.Id
                         select ks.TenKhachSan).FirstOrDefault();
            var diachi = (from ks in _context.KhachSans
                          join p in _context.Phongs on ks.Id equals p.IdKhachSan
                          where p.Id == bookingModel.Phong.Id
                          select ks.DiaChi).FirstOrDefault();
            var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
            var strSanPham = "<table border='1'>";
            strSanPham += "<thead><tr><th>Tên khách sạn</th><th>Tên phòng</th><th>Ngày đến</th><th>Ngày đi</th><th>Địa chỉ</th><th>";
            if (paymentMethod == "Thanh toán cọc")
            {
                strSanPham += "Tiền cọc</th><th>Cần thanh toán thêm</th>";
            }
            else
            {
                strSanPham += "Tổng tiền</th>";
            }
            strSanPham += "</tr></thead>";
            strSanPham += "<tbody>";
            strSanPham += "<tr>";
            strSanPham += $"<td>{tenks}</td>";
            strSanPham += $"<td>{bookingModel.Phong.TenPhong}</td>";
            strSanPham += $"<td>{start}</td>";
            strSanPham += $"<td>{end}</td>";
            strSanPham += $"<td>{diachi}</td>";
            if (paymentMethod == "Thanh toán cọc")
            {
                strSanPham += $"<td>{data.Sum(x => x.TongTien()) * 40 / 100}</td>";
                strSanPham += $"<td>{data.Sum(x => x.TongTien()) * 60 / 100}</td>";
            }
            else
            {
                strSanPham += $"<td>{data.Sum(x => x.TongTien())}</td>";
            }
            strSanPham += "</tr>";
            strSanPham += "</tbody></table>";
            var strThongTinKhachHang = $@"
    <p>Họ tên khách hàng: {bookingModel.HoVaTen}</p>
    <p>Email: {bookingModel.Email}</p>
    <p>Số điện thoại: {bookingModel.SoDienThoai}</p>
    ";
            var fullContent = $@"
    <h2>Thông tin đặt phòng</h2>
        {strSanPham}
    <h2>Thông tin khách hàng</h2>
        {strThongTinKhachHang}
    ";

            // Xác định tiêu đề và thông tin thanh toán dựa trên phương thức thanh toán
            if (paymentMethod == "Thanh toán cọc")
            {
                subject = "Xác nhận đặt cọc phòng thành công";
                paymentInfo = "Bạn đã đặt cọc phòng thành công.";
            }
            else
            {
                subject = "Phòng bạn được đặt thành công";
                paymentInfo = "Cảm ơn bạn đã đặt phòng.";
            }

            // Gửi email
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("doducviet3012@gmail.com", "ebfwregutahnwhrj"),
                EnableSsl = true,
            };
            var fromAddress = new MailAddress("doducviet3012@gmail.com", "Hotel");
            var toAddress = new MailAddress(customerEmail);
            var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = $"{fullContent}<p>{paymentInfo}</p>",
                IsBodyHtml = true
            };
            smtpClient.Send(mailMessage);
        }

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
            var phuongthuc = HttpContext.Session.GetString("PhuongThuc");
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VNpay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }
            var data = HttpContext.Session.Get<List<CartItem>>("GioHang");
            //Lưu đơn hàng
            if (phuongthuc == "Thanh toán cọc")
            {
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
                        GhiChu = ghichu,
                        TongTien = item.TongTien() * 40 /100,
                        NgayDat = DateTime.Now,
                        IdTrangThai = 2
                    };
                    _context.Add(don);
                }
                _context.SaveChanges();
                TempData["Message"] = $"Thanh toán VNpay thành công: {response.VnPayResponseCode}";
                SendConfirmationEmail(new ThongTinDatPhong { HoVaTen = hovaten, Email = email, SoDienThoai = sdt, GhiChu = ghichu, Phong = data[0] }, email ?? "", phuongthuc ?? "");
                return RedirectToAction("ThanhCong", "DatPhong");
            }
            else
            {
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
                        GhiChu = ghichu,
                        TongTien = item.TongTien(),
                        NgayDat = DateTime.Now,
                        IdTrangThai = 1
                    };
                    _context.Add(don);
                }
                _context.SaveChanges();
                TempData["Message"] = $"Thanh toán VNpay thành công: {response.VnPayResponseCode}";

                SendConfirmationEmail(new ThongTinDatPhong { HoVaTen = hovaten, Email = email, SoDienThoai = sdt, GhiChu = ghichu, Phong = data[0] },email ?? "", phuongthuc ?? "");
                return RedirectToAction("ThanhCong", "DatPhong");
            }
        }

        public IActionResult PaymentFail()
        {
            return View();
        }
    }
}
