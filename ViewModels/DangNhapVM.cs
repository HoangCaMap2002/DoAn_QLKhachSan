using System.ComponentModel.DataAnnotations;

namespace DoAn_QLKhachSan.ViewModels
{
    public class DangNhapVM
    {
        [Display(Name ="Tên đăng nhập")]
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập")]
        [MaxLength(100,ErrorMessage = "Tối đa 100 ký tự")]
        public string? TenDangNhap { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string? MatKhau { get; set; }
    }
}
