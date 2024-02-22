using DoAn_QLKhachSan.Models;

namespace DoAn_QLKhachSan.ViewModels
{
    public class CartItem
    {
        public int Id { get; set; }
        public string? TenPhong { get; set; }
        public double? GiaPhong { get; set; }
        public string? AnhDaiDien { get; set; }
        public string? BatDau { get; set; }
        public string? KetThuc { get; set; }
    }
}
