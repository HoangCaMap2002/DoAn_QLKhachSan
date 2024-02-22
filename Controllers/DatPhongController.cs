using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebBanGiay.Helper;

namespace DoAn_QLKhachSan.Controllers
{
    [Authorize]
    public class DatPhongController : Controller
    {
        private readonly IPhongService _phongService;
        public DatPhongController(IPhongService phongService)
        {
            _phongService = phongService;
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
                    item = new CartItem
                    {
                        Id = phong.Id,
                        TenPhong = phong.TenPhong,
                        GiaPhong = phong.GiaPhong,
                        AnhDaiDien = phong.AnhDaiDien,
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
        public async Task<IActionResult> ThongTinDatPhong(ThongTinDatPhong tt)
        {
            var tdn = HttpContext.Session.GetString("TenDangNhap");
            var rs = await _phongService.DatPhong(Carts, tdn, tt);
            if (rs == true)
            {
                return RedirectToAction("ThanhCong");
            }
            return View();
        }
        public IActionResult Thanhcong()
        {
            return View();
        }
    }
}
