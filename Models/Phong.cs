using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class Phong
{
    public int Id { get; set; }

    public string? TenPhong { get; set; }

    public int? IdViTri { get; set; }

    public bool? TrangThai { get; set; }

    public double? GiaPhong { get; set; }

    public int? IdLoaiPhong { get; set; }

    public int? IdKhachSan { get; set; }

    public int? IdTienNghiPhong { get; set; }

    public string? AnhDaiDien { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();

    public virtual ICollection<HinhAnh> HinhAnhs { get; set; } = new List<HinhAnh>();

    public virtual KhachSan? IdKhachSanNavigation { get; set; }

    public virtual LoaiPhong? IdLoaiPhongNavigation { get; set; }

    public virtual TienNghiPhong? IdTienNghiPhongNavigation { get; set; }

    public virtual ViTri? IdViTriNavigation { get; set; }
}
