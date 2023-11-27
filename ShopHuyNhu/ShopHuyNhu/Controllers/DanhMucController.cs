using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopHuyNhu.Models;
using ShopHuyNhu.Repository;

namespace ShopHuyNhu.Controllers
{
    public class DanhMucController : Controller
    {
        private readonly DataContext _dataContext;
        public DanhMucController(DataContext context)
        {
            _dataContext = context;
        }
        public async Task<IActionResult> Index(string Slug = "")
        {
            DanhMucModel danhmuc= _dataContext.DanhMucs.Where(c =>c.Slug ==Slug).FirstOrDefault();
            if(danhmuc == null)
            {
                return RedirectToAction("Index");
            }
            var sanphamByDanhMuc = _dataContext.SanPhams.Where(p =>p.DanhMucId == danhmuc.ID);
            return View(await sanphamByDanhMuc.OrderByDescending(p =>p.Id).ToListAsync());
        }
    }
}
