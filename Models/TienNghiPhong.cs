using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class TienNghiPhong
{
    public int Id { get; set; }

    public string? TenTienNghi { get; set; }

    public string? ClassIcon { get; set; }

    public virtual ICollection<Phong> Phongs { get; set; } = new List<Phong>();
}
