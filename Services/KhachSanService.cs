using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DoAn_QLKhachSan.Services
{
    public class KhachSanService : IKhachSanService
    {
        private readonly QuanLyKhachSanContext _context;
        public KhachSanService(QuanLyKhachSanContext context)
        {
            _context= context;
        }

        public async Task<bool> CreateKhachSanAsync(KhachSanPhongViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<KhachSan>> GetAllKhachSanByIdTinhAsync(int idtinhthanh)
        {
            var khachsan = await _context.KhachSans.Include(x => x.Phongs).Where(x => x.IdTinhThanh == idtinhthanh && x.IsDelete == false).ToListAsync();
            return khachsan;
        }

        public async Task<List<KhachSan>> GetAllKhachSanByUsernameAsync(string tendangnhap)
        {
            var khachsans = await _context.KhachSans.Where(x => x.NguoiQuanLy == tendangnhap && x.IsDelete == false).ToListAsync();
            return khachsans;
        }

        public async Task<IEnumerable<KhachSan>> GetKhachSanPhongTrongAsync(string start, string end, int city)
        {
            string sql = "EXEC sp_LayKhachSanCoPhongTrong @IdTinhThanh, @StartDate, @EndDate";
            var parameters = new[]
            {
                new SqlParameter("@IdTinhThanh", city),
                new SqlParameter("@StartDate", start),
                new SqlParameter("@EndDate", end)
            };
            IEnumerable<KhachSan> result =  await _context.KhachSans.FromSqlRaw(sql, parameters).ToListAsync();
            foreach (var khachSan in result)
            {
                var phongs = _context.Phongs.Where(p => p.IdKhachSan == khachSan.Id).ToList();
                khachSan.Phongs = phongs;
            }
            return result;
        }
    }
}
