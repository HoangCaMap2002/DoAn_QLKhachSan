namespace DoAn_QLKhachSan.ViewModels
{
    public class KhachSanViewModel
    {
        public int Id { get; set; }

        public int? IdTinhThanh { get; set; }
        public int? IdLoaiKhachSan { get; set; }

        public string? TenKhachSan { get; set; }

        public string? DiaChi { get; set; }

        public string? GioiThieu { get; set; }

        public string? TieuDe { get; set; }

        public string? GhiChu { get; set; }

        public IFormFile? AnhDaiDien { get; set; }
        public List<int> IdTienNghis { get; set; }

        public int? SoSao { get; set; }

        public bool? IsDelete { get; set; }

        public double? GiaTrungBinh { get; set; }
    }
}
