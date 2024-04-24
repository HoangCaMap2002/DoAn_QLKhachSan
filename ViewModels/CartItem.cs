using DoAn_QLKhachSan.Extension;
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
        public int? SoSao { get; set; }
        public string? DiaChi { get; set; }
        
        public double? TongTien()
        {
            DateTime batDau = BatDau.ToDateTime();
            DateTime ketThuc = KetThuc.ToDateTime();
            TimeSpan thoiGian = ketThuc - batDau;
            var songay = (int)thoiGian.TotalDays;
            var tienphong = GiaPhong * songay;
            return tienphong;
        }
    }
}
