using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;

namespace DoAn_QLKhachSan.Controllers
{
    public class AccessController : Controller
    {
        private readonly ITaiKhoanService _taiKhoanService;
        public AccessController(ITaiKhoanService taiKhoanService)
        {
            _taiKhoanService = taiKhoanService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(DangNhapVM tk)
        {
            var user = await _taiKhoanService.LayTaiKhoanTheoTenTK(tk.TenDangNhap);
            if (user != null)
            {
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
                              new Claim(ClaimTypes.NameIdentifier, user.TenDangNhap)
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
                              new Claim(ClaimTypes.NameIdentifier, user.TenDangNhap)
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
                        return RedirectToAction("Index", "Home");
                    }
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
