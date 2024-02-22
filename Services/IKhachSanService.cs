using DoAn_QLKhachSan.Models;

namespace DoAn_QLKhachSan.Services
{
    public interface IKhachSanService
    {
        Task<List<KhachSan>> GetAllKhachSanByIdTinhAsync(int idtinhthanh);
        Task<IEnumerable<KhachSan>> GetKhachSanPhongTrongAsync(string start, string end, int city);
    }
}
