using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class PhanQuyen
{
    public int Id { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public int IdQuyen { get; set; }

    public virtual Quyen IdQuyenNavigation { get; set; } = null!;

    public virtual TaiKhoan TenDangNhapNavigation { get; set; } = null!;
}
