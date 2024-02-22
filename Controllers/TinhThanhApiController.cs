using DoAn_QLKhachSan.Services;
using Microsoft.AspNetCore.Mvc;

namespace DoAn_QLKhachSan.Controllers
{
    public class TinhThanhApiController : Controller
    {
        private readonly ITinhThanhService _tinhThanhService;
        public TinhThanhApiController(ITinhThanhService tinhThanhService)
        {
            _tinhThanhService = tinhThanhService;
        }

        [HttpGet]
        public async Task<IActionResult> GoiYTimKiem(string query)
        {
            var kq = await _tinhThanhService.GetAllTenTinhThanhAsync(query);
            return Json(kq);
        }
    }
}
