using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ShopHuyNhu.Areas.Admin.Controllers
{
	public class AdminController : Controller
	{
		[Area("Admin")]
		[Authorize(Roles = "Admin")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
