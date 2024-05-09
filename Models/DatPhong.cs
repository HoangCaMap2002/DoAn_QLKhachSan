using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class DatPhong
{
    public int Id { get; set; }

    public int? IdPhong { get; set; }

    public string? TenDangNhap { get; set; }

    public DateTime? BatDau { get; set; }

    public DateTime? KetThuc { get; set; }

    public double? TongTien { get; set; }

    public string? ThanhToan { get; set; }

    public bool? Status { get; set; }

    public string? HoVaTen { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? GhiChu { get; set; }

    public DateTime? NgayDat { get; set; }

    public int? IdTrangThai { get; set; }

    public virtual Phong? IdPhongNavigation { get; set; }

    public virtual TrangThai? IdTrangThaiNavigation { get; set; }

    public virtual TaiKhoan? TenDangNhapNavigation { get; set; }
}
