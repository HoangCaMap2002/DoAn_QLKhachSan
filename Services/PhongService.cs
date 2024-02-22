using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DoAn_QLKhachSan.Extension;

namespace DoAn_QLKhachSan.Services
{
    public class PhongService : IPhongService
    {
        private readonly QuanLyKhachSanContext _context;
        public PhongService(QuanLyKhachSanContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PhongModel>> GetAllPhongByIdKhachSanAsync(int idkhachsan)
        {
            var phong = await _context.Phongs.Where(x => x.IdKhachSan == idkhachsan).ToListAsync();
            var phongViewModels = phong.Select(p => new PhongModel
            {
                Phong = p,
                // Gán các thuộc tính khác của PhongViewModel
                ImagePaths =  p.HinhAnhs.Select(filename => $"{{'src': '../assets/Images/phong/{filename.UrlHinhAnh}'}}").ToList()
            }).ToList();
            return phongViewModels;
        }

        public async Task<IEnumerable<PhongModel>> GetAllPhongTrongByIdKhachSanAsync(int idkhachsan, string start, string end)
        {
            string sql = "EXEC sp_LayDanhSachPhongTrong @IdKhachSan, @StartDate, @EndDate";
            var parameters = new[]
            {
                new SqlParameter("@IdKhachSan", idkhachsan),
                new SqlParameter("@StartDate", start),
                new SqlParameter("@EndDate", end)
            };
            IEnumerable<Phong> phong = await _context.Phongs.FromSqlRaw(sql, parameters).ToListAsync();
            foreach (var item in phong)
            {
                var hinhanh = _context.HinhAnhs.Where(x => x.IdPhong == item.Id).ToList();
                item.HinhAnhs = hinhanh;
            }

            //var phong = db.Phongs.Include(x => x.HinhAnhs).Where(x => x.IdKhachSan == idkhachsan && x.IsDelete == false).ToList();

            var phongViewModels = phong.Select(p => new PhongModel
            {
                Phong = p,
                // Gán các thuộc tính khác của PhongViewModel
                ImagePaths = p.HinhAnhs.Select(filename => $"{{'src': '../assets/Images/phong/{filename.UrlHinhAnh}'}}").ToList()
            }).ToList();
            return phongViewModels;
        }

        public async Task<Phong> GetPhongById(int idphong)
        {
            var phong = await _context.Phongs.Include(x => x.HinhAnhs).Where(x => x.Id == idphong).FirstOrDefaultAsync();
            return phong;
        }
        public async Task<bool> DatPhong(List<CartItem> cart, string tendangnhap, ThongTinDatPhong tt)
        {
            if (cart.Count() != 0)
            {
                foreach (var item in cart)
                {
                    DatPhong datphong = new DatPhong()
                    {
                        IdPhong = item.Id,
                        TenDangNhap = tendangnhap,
                        BatDau = item.BatDau.ToDateTime(),
                        KetThuc = item.KetThuc.ToDateTime(),
                        TongTien = item.GiaPhong,
                        HoVaTen = tt.HoVaTen,
                        SoDienThoai= tt.SoDienThoai,
                        Email = tt.Email,
                        GhiChu = tt.GhiChu,
                        ThanhToan = "Chưa",
                        Status = false
                    };
                    _context.Add(datphong);
                    await _context.SaveChangesAsync();
                }
                return true;
            }

            return false;
        }
    }
}
