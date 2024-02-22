using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.ModelsApi;

namespace DoAn_QLKhachSan.Services
{
    public interface ITinhThanhService
    {
        Task<List<TinhThanh>> GetAllTinhThanhAsync();
        Task<List<TinhThanhModelApi>> GetAllTenTinhThanhAsync(string query);
    }
}
