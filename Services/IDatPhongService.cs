using DoAn_QLKhachSan.ViewModels;

namespace DoAn_QLKhachSan.Services
{
    public interface IDatPhongService
    {
        Task<bool> DatPhong(List<CartItem> cart, string tendangnhap);
    }
}
