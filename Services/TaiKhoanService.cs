using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace DoAn_QLKhachSan.Services
{
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly QuanLyKhachSanContext _context;
        public TaiKhoanService(QuanLyKhachSanContext context)
        {
            _context = context;
        }

        public async Task<bool> DoiMatKhau(DoiMKVM model)
        {
            try
            {
                TaiKhoan? user = await _context.TaiKhoans.SingleOrDefaultAsync(u => u.TenDangNhap == model.TenDangNhap);
                if (user == null)
                {
                    return false;
                }
                string mkcu = model.MatKhauCu;
                var passwordHasher = new PasswordHasher();
                string hashedMkCu = passwordHasher.HashPassword(mkcu);
                if (hashedMkCu != user.MatKhau)
                {
                    return false;
                }
                string hashedMkMoi = passwordHasher.HashPassword(model.MatKhauMoi);
                user.MatKhau = hashedMkMoi;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public async Task<List<string>?> LayQuyenCuaTk(string tendangnhap)
        {
            try
            {
                var quyen = await(from u in _context.TaiKhoans
                                           join tu in _context.PhanQuyens on u.TenDangNhap equals tu.TenDangNhap
                                           join t in _context.Quyens on tu.IdQuyen equals t.Id
                                           where u.TenDangNhap == tendangnhap
                                           select t.TenQuyen).ToListAsync();
                if (quyen != null)
                {
                    return quyen;
                }
                return null;
            }
            catch (Exception )
            {
                return null;
            }
        }

        public async Task<TaiKhoan?> LayTaiKhoanTheoTenTK(string tendangnhap)
        {
            try
            {
                TaiKhoan? user = await _context.TaiKhoans.SingleOrDefaultAsync(u => u.TenDangNhap == tendangnhap);
                if (user != null)
                {
                    return user;
                }
                else return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Regiter(DangKyVM rvm)
        {
            var existingUser = await _context.TaiKhoans.FirstOrDefaultAsync(u => u.TenDangNhap == rvm.TenDangNhap);

            if (existingUser != null)
            {
                return false; // Người dùng đã tồn tại
            }
            string plainPassword = rvm.MatKhau;
            var passwordHasher = new PasswordHasher();
            string hashedPassword = passwordHasher.HashPassword(plainPassword);
            TaiKhoan taikhoan = new TaiKhoan()
            {
                TenDangNhap = rvm.TenDangNhap,
                MatKhau = hashedPassword,

            };
            _context.Add(taikhoan);
            await _context.SaveChangesAsync();
            PhanQuyen phanquyen = new PhanQuyen()
            {
                TenDangNhap = taikhoan.TenDangNhap,
                IdQuyen = 1
            };
            _context.Add(phanquyen);
            await _context.SaveChangesAsync();
            return true; // Đăng ký thành công
        }
    }
}
