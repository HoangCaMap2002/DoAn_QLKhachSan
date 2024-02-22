using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class GiaoDich
{
    public int Id { get; set; }

    public int? IdDatPhong { get; set; }

    public string? TenDangNhap { get; set; }

    public int? IdDichVu { get; set; }

    public int? SoLuong { get; set; }

    public DateTime? ThoiGianGiaoDich { get; set; }

    public virtual DichVu? IdDichVuNavigation { get; set; }

    public virtual TaiKhoan? TenDangNhapNavigation { get; set; }
}
