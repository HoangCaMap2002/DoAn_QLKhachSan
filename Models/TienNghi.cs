using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class TienNghi
{
    public int Id { get; set; }

    public string? TenTienNghi { get; set; }

    public string? ClassIcon { get; set; }

    public virtual ICollection<KhachSanTienNghi> KhachSanTienNghis { get; set; } = new List<KhachSanTienNghi>();

    public virtual ICollection<PhongTienNghi> PhongTienNghis { get; set; } = new List<PhongTienNghi>();
}
