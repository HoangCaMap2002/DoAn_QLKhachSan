using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class TinhThanh
{
    public int Id { get; set; }

    public string? AnhDaiDien { get; set; }

    public string? TenTinh { get; set; }

    public string? GhiChu { get; set; }

    public bool? IsDelete { get; set; }

    public virtual ICollection<KhachSan> KhachSans { get; set; } = new List<KhachSan>();
}
