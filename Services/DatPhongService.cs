using DoAn_QLKhachSan.Extension;
using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.ViewModels;

namespace DoAn_QLKhachSan.Services
{
    public class DatPhongService : IDatPhongService
    {
        private readonly QuanLyKhachSanContext _context;
        public DatPhongService(QuanLyKhachSanContext context)
        {
            _context = context;
        }

        public async Task<bool> DatPhong(List<CartItem> cart, string tendangnhap)
        {
            if (cart.Count() != 0)
            {
                foreach (var item in cart)
                {
                    DatPhong datphong = new DatPhong()
                    {
                        Id= item.Id,
                        TenDangNhap = tendangnhap,
                        BatDau = item.BatDau.ToDateTime(),
                        KetThuc = item.KetThuc.ToDateTime(),
                        TongTien = item.GiaPhong,
                        ThanhToan = "Chưa",
                    };
                    _context.Add(datphong);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            
            return false;
        }
    }
}
