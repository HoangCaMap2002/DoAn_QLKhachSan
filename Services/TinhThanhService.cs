using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.ModelsApi;
using Microsoft.EntityFrameworkCore;

namespace DoAn_QLKhachSan.Services
{
    public class TinhThanhService : ITinhThanhService
    {
        private readonly QuanLyKhachSanContext _context;
        public TinhThanhService(QuanLyKhachSanContext context)
        {
            _context = context;
        }
        public async Task<List<TinhThanhModelApi>> GetAllTenTinhThanhAsync(string query)
        {
            var lstTinhThanh = await _context.TinhThanhs
                                    .Where(t => t.TenTinh.ToLower().Contains(query.ToLower()))
                                    .ToListAsync();
            var lstTinhThanhApi = lstTinhThanh.Select(t => new TinhThanhModelApi
            {
                Id = t.Id,
                TenTinh = t.TenTinh
            }).ToList();
            return lstTinhThanhApi;
        }

        public async Task<List<TinhThanh>> GetAllTinhThanhAsync()
        {
            var khuvuc = await _context.TinhThanhs.Include(t => t.KhachSans).Where(x => x.IsDelete == false).ToListAsync();
            return khuvuc;
        }
    }
}
