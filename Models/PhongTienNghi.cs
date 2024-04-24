using System;
using System.Collections.Generic;

namespace DoAn_QLKhachSan.Models;

public partial class PhongTienNghi
{
    public int Id { get; set; }

    public int? IdPhong { get; set; }

    public int? IdTienNghi { get; set; }

    public virtual Phong? IdPhongNavigation { get; set; }

    public virtual TienNghi? IdTienNghiNavigation { get; set; }
}
