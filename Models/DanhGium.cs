using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class DanhGium
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
    public double? DiemTb => Math.Round(((double)DiemNhanVien + (double)DiemSachSe + (double)DiemThom + (double)DiemCoSoVatChat) / 4.0, 1);

    public virtual TaiKhoan? TenDangNhapNavigation { get; set; }
}
