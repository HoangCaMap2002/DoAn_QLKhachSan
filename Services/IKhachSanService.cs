using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.ViewModels;

namespace DoAn_QLKhachSan.Services
{
    public interface IKhachSanService
    {
        Task<List<KhachSan>> GetAllKhachSanByIdTinhAsync(int? idtinhthanh);
        Task<IEnumerable<KhachSan>> GetKhachSanPhongTrongAsync(string start, string end, int city);
        Task<bool> CreateKhachSanAsync(KhachSanPhongViewModel model);
        Task<List<KhachSan>> GetAllKhachSanByUsernameAsync(string tendangnhap);
        Task<KhachSan> GetKhachSanByIdAsync(int idkhachsan);
    }
}
