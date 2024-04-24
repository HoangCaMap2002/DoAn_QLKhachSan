using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class Phong
{
    public int Id { get; set; }

    public string? TenPhong { get; set; }

    public bool? TrangThai { get; set; }

    public double? GiaPhong { get; set; }

    public int? IdKhachSan { get; set; }

    public string? AnhDaiDien { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();

    public virtual ICollection<HinhAnh> HinhAnhs { get; set; } = new List<HinhAnh>();

    public virtual KhachSan? IdKhachSanNavigation { get; set; }

    public virtual ICollection<PhongTienNghi> PhongTienNghis { get; set; } = new List<PhongTienNghi>();
}
