using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Net.WebSockets;

namespace DoAn_QLKhachSan.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        QuanLyKhachSanContext db = new QuanLyKhachSanContext();
        private readonly ITinhThanhService _tinhThanhService;

        public HomeController(ILogger<HomeController> logger, ITinhThanhService tinhThanhService)
        {
            _logger = logger;
            _tinhThanhService= tinhThanhService;
        }
        public async Task<IActionResult> Index()
        {
            var khuvuc = await _tinhThanhService.GetAllTinhThanhAsync();
            
            return View(khuvuc);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
// dotnet ef dbcontext scaffold -o Models -d "Data Source=localhost,1433;Initial Catalog = QuanLyKhachSan;User ID = SA;Password = Password123;TrustServerCertificate=True" "Microsoft.EntityFrameworkCore.SqlServer"
//dotnet ef dbcontext scaffold -o Models -d "Data Source=localhost,1433;Initial Catalog=QuanLyKhachSan;User ID=SA;Password=Password123;TrustServerCertificate=True" "Microsoft.EntityFrameworkCore.SqlServer" --force