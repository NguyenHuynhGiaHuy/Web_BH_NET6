﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopHuyNhu.Models;
using ShopHuyNhu.Repository;

namespace ShopHuyNhu.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class DatHangController : Controller
	{
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostenvironment;
        public DatHangController(DataContext context, IWebHostEnvironment webHostenvironment)
        {
            _dataContext = context;
            _webHostenvironment = webHostenvironment;

        }
        public async Task<IActionResult> Index()
		{
			return View(await _dataContext.DatHang.OrderByDescending(p => p.Id).ToListAsync()); 
		}
        
    }
}
