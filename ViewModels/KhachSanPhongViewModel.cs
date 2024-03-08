namespace DoAn_QLKhachSan.ViewModels
{
    public class KhachSanPhongViewModel
    {
        public int? IdKhachSan { get; set; }
        public int? IdTinhThanh { get; set; }

        public string? TenKhachSan { get; set; }

        public string? DiaChi { get; set; }

        public string? GioiThieu { get; set; }

        public string? TieuDe { get; set; }

        public string? GhiChu { get; set; }

        public IFormFile? AnhDaiDien { get; set; }

        public int? SoSao { get; set; }
        public int? IdLoaiKhachSan { get; set; }

        public string? NguoiQuanLy { get; set; }

        public List<PhongViewModel> Phongs { get; set; }

        public KhachSanPhongViewModel()
        {
            // Khởi tạo danh sách phòng
            Phongs = new List<PhongViewModel>();
        }
    }
}
