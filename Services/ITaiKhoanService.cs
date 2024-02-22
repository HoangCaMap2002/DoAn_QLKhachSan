using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.ViewModels;

namespace DoAn_QLKhachSan.Services
{
    public interface ITaiKhoanService
    {
        Task<bool> Regiter(DangKyVM rvm);
        Task<TaiKhoan?> LayTaiKhoanTheoTenTK(string tendangnhap);
        Task<List<string>?> LayQuyenCuaTk(string tendangnhap);
        Task<bool> DoiMatKhau(DoiMKVM model);
    }
}
