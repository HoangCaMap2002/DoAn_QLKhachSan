using System.ComponentModel.DataAnnotations;

namespace DoAn_QLKhachSan.ViewModels
{
    public class ProfileViewModel
    {
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string? SoDienThoai { get; set; }
        [Display(Name = "Ảnh")]
        public IFormFile? Anh { get; set; }
        [Display(Name = "Họ và tên")]
        [Required(ErrorMessage = "Họ và tên là bắt buộc")]
        [StringLength(50, ErrorMessage = "Họ và tên không được vượt quá 50 ký tự")]
        public string? HoVaTen { get; set; }
    }
}
