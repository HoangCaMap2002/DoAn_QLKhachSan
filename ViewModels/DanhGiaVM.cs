namespace DoAn_QLKhachSan.ViewModels
{
    public class DanhGiaVM
    {
        public int IdDanhGia { get; set; }

        public string? TenDangNhap { get; set; }

        public int? IdKhachSan { get; set; }

        public string? Comment { get; set; }

        public DateTime? NgayComment { get; set; }

        public int? DiemNhanVien { get; set; }

        public int? DiemSachSe { get; set; }

        public int? DiemCoSoVatChat { get; set; }

        public int? DiemThom { get; set; }

        public string? TenNguoiComment { get; set; }
        public double? DiemTb => Math.Round(((double)DiemNhanVien + (double)DiemSachSe + (double)DiemThom + (double)DiemCoSoVatChat) / 4.0, 1);
    }
}
