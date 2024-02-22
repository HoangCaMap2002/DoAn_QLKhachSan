﻿using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class DichVu
{
    public int Id { get; set; }

    public string? TenDichVu { get; set; }

    public double? GiaTien { get; set; }

    public string? GhiChu { get; set; }

    public virtual ICollection<GiaoDich> GiaoDiches { get; set; } = new List<GiaoDich>();
}
