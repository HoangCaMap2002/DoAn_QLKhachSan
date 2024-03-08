using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class LoaiKhachSan
{
    public int Id { get; set; }

    public string? TenLoai { get; set; }

    public virtual ICollection<KhachSan> KhachSans { get; set; } = new List<KhachSan>();
}
