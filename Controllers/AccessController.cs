using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace DoAn_QLKhachSan.Controllers
{
    public class AccessController : Controller
    {
        private readonly IKhachSanService _khachSanService;
        private readonly ITaiKhoanService _taiKhoanService;
        private readonly IPhongService _phongService;
        private readonly QuanLyKhachSanContext _context;

        public AccessController(ITaiKhoanService taiKhoanService, QuanLyKhachSanContext context, IKhachSanService khachSanService, IPhongService phongService)
        {
            _khachSanService = khachSanService;
            _taiKhoanService = taiKhoanService;
            _phongService = phongService;
            _context = context;
        }
        [HttpGet]
        public IActionResult Login(string? returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(DangNhapVM tk, string? returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            if (ModelState.IsValid)
            {
                var user = await _taiKhoanService.LayTaiKhoanTheoTenTK(tk.TenDangNhap);
                var khacsans = await _khachSanService.GetAllKhachSanByUsernameAsync(tk.TenDangNhap);
                if (user != null)
                {
                    var sokhachsan = user.KhachSans.Count(x => x.IsDelete == false);
                    
                    var quyen = await _taiKhoanService.LayQuyenCuaTk(user.TenDangNhap);
                    var passwordHasher = new PasswordHasher();
                    var passwordVerification = passwordHasher.VerifyHashedPassword(user.MatKhau, tk.MatKhau);
                    if (passwordVerification == Microsoft.AspNet.Identity.PasswordVerificationResult.Success)
                    {
                        HttpContext.Session.SetString("TenDangNhap", user.TenDangNhap);
                        if (quyen.Contains("Admin"))
                        {
                            List<Claim> claims = new List<Claim>()
                              {
                              new Claim(ClaimTypes.NameIdentifier, user.TenDangNhap),
                              new Claim("Anh", user.Anh ?? ""),
                              new Claim("HoVaTen", user.HoVaTen ?? ""),
                              };
                            foreach (var role in quyen)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role)); // Thêm từng quyền vào danh sách claims
                            }
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                                      CookieAuthenticationDefaults.AuthenticationScheme);
                            AuthenticationProperties properties = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity), properties);
                            return Redirect("/Admin/HomeAdmin/Index");
                        }
                        if (quyen.Contains("Customer"))
                        {
                            List<Claim> claims = new List<Claim>()
                              {
                              new Claim(ClaimTypes.NameIdentifier, user.TenDangNhap),
                              new Claim("Anh", user.Anh ?? ""),
                              new Claim("HoVaTen", user.HoVaTen ?? ""),
                              };
                            foreach (var role in quyen)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role)); // Thêm từng quyền vào danh sách claims
                            }
                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                                      CookieAuthenticationDefaults.AuthenticationScheme);
                            AuthenticationProperties properties = new AuthenticationProperties()
                            {
                                AllowRefresh = true,
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity), properties);
                            if (Url.IsLocalUrl(returnurl))
                            {
                                return Redirect(returnurl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("loi", "không có khách hàng này");
                }
            }
            return View();
        }
        public IActionResult Register() { 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(DangKyVM rvm)
        {
            var result = await _taiKhoanService.Regiter(rvm);
            if (!result)
            {
                TempData["Error"] = "Tài khoản đã tồn tại";
                return View();
            }

            return RedirectToAction("Login", "Access");
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }
    }
}
