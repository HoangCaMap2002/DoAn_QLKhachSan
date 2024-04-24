namespace DoAn_QLKhachSan.ViewModels
{
    public class PhongViewModel
    {
            public int Id { get; set; }
            public string? TenPhong { get; set; }
            public double? GiaPhong { get; set; }
            public int? IdKhachSan { get; set; }
            public List<int?> IdTienNghiPhong { get; set; }
            public IFormFile? AnhDaiDien { get; set; }
    }
}
