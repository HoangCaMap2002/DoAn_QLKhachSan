using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class TrangThai
{
    public int Id { get; set; }

    public string? TenTrangThai { get; set; }

    public virtual ICollection<DatPhong> DatPhongs { get; set; } = new List<DatPhong>();
}
