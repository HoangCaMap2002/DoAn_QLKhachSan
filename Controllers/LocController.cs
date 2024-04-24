using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace DoAn_QLKhachSan.Controllers
{
    public class LocController : Controller
    {
        private readonly IKhachSanService _khachSanService;

        public LocController(IKhachSanService khachSanService) {
            _khachSanService = khachSanService;
        }
        public async Task<IActionResult> LocKhachSan(string sao, double max, double min, int loaikhachsan)
        { 
            int idtinhthanh = (int)HttpContext.Session.GetInt32("idTinhThanh");
            string start = HttpContext.Session.GetString("startDate");
            string end = HttpContext.Session.GetString("EndDate");
            var khachsans = await _khachSanService.GetKhachSanPhongTrongAsync(start, end, idtinhthanh);
            var conditions = new List<Func<KhachSan, bool>>();
            var searchResults = khachsans;
            //Tìm kiếm theo sao
            
            if (sao != null)
            {
                List<int> saoList = sao.Split(',').Select(int.Parse).ToList();
                conditions.Add(hotel => saoList.Any(select => hotel.SoSao == select));
            }
            //Tìm kiếm theo khoảng giá
            if (min > 0 && max > 0)
            {
                conditions.Add(hotel =>
                 hotel.Phongs.Average(x => x.GiaPhong).Value > min && hotel.Phongs.Average(x => x.GiaPhong).Value < max
                );
            }
            if (loaikhachsan > 0)
            {
                conditions.Add(hotel => hotel.IdLoaiKhachSan == loaikhachsan);
            }
            if(conditions.Count() == 0)
            {
                return PartialView("_PatialKhachSan", khachsans);
            }
            searchResults = searchResults.Where(hotel =>
                conditions.All(condition => condition(hotel))).ToList();
            return PartialView("_PatialKhachSan", searchResults);
        }
    }
}
