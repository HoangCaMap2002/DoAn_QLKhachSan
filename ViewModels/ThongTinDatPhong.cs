using DoAn_QLKhachSan.Models;

namespace DoAn_QLKhachSan.ViewModels
{
    public class ThongTinDatPhong
    {
        public string? HoVaTen { get; set; }

        public string? SoDienThoai { get; set; }

        public string? Email { get; set; }

        public string? GhiChu { get; set; }

        public CartItem? Phong { get; set; }
    }
}
