using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using DoAn_QLKhachSan.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn_QLKhachSan.ViewComponents
{
    public class TinhViewComponent : ViewComponent
    {
        private readonly ITinhThanhService _tinhThanhService;
        public TinhViewComponent(ITinhThanhService tinhThanhService)
        {
            _tinhThanhService = tinhThanhService;
        }  
       
    }
}
