using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.ViewModels;

namespace DoAn_QLKhachSan.Services
{
    public interface IPhongService
    {
        Task<IEnumerable<PhongModel>> GetAllPhongByIdKhachSanAsync(int idkhachsan);
        Task<IEnumerable<PhongModel>> GetAllPhongTrongByIdKhachSanAsync(int idkhachsan, string start, string end);
        Task<Phong> GetPhongById(int idphong);
        Task<bool> DatPhong(List<CartItem> cart, string tendangnhap, ThongTinDatPhong tt);
    }
}
