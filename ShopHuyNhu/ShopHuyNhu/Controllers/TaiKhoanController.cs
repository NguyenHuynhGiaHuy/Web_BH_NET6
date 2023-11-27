using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShopHuyNhu.Models;
using ShopHuyNhu.Models.ViewModels;

namespace ShopHuyNhu.Controllers
{
   
    public class TaiKhoanController : Controller
    {
        private UserManager<AppUserModel> _userManage; //ql 
        private SignInManager<AppUserModel> _signInManage;
		private readonly RoleManager<IdentityRole> _roleManager;
		public TaiKhoanController(UserManager<AppUserModel> userManage, SignInManager<AppUserModel> signInManage, RoleManager<IdentityRole> roleManager)
        {
            _userManage = userManage;
            _signInManage = signInManage;
			_roleManager = roleManager;
		}

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl=returnUrl});
        }
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel loginViewModel)
		{
			if (ModelState.IsValid)
			{
				Microsoft.AspNetCore.Identity.SignInResult result = await _signInManage.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
				if (result.Succeeded)
				{
					// Lấy thông tin người dùng đăng nhập
					AppUserModel user = await _userManage.FindByNameAsync(loginViewModel.UserName);

					// Kiểm tra và xử lý quyền của người dùng sau khi đăng nhập
					bool isAdmin = await _userManage.IsInRoleAsync(user, "Admin");
					if (isAdmin)
					{
						// Người dùng có quyền Admin, chuyển hướng đến trang Admin
						return RedirectToAction("Index", "Admin"); // Điều hướng đến trang AdminDashboard của Controller Admin
					}
					else
					{
						// Người dùng không phải Admin, chuyển hướng đến trang mua hàng
						return RedirectToAction("Index", "Home"); // Điều hướng đến trang Shop của Controller Home
					}
				}
				ModelState.AddModelError("", "SAI TK OR MK");
			}
			return View(loginViewModel);
		}

		public IActionResult Create()
		{
			return View();
		}
		public async Task<IActionResult> DangNhap()
        {
            return View();
        }
		[HttpPost]
		public async Task<IActionResult> Create(UserModel user)
		{
			if (ModelState.IsValid)
			{
				var roleNames = new[] { "Admin", "User" };

				foreach (var roleName in roleNames)
				{
					var roleExists = await _roleManager.RoleExistsAsync(roleName);
					if (!roleExists)
					{
						var newRole = new IdentityRole(roleName);
						await _roleManager.CreateAsync(newRole);
					}
				}

				AppUserModel newUser = new AppUserModel { UserName = user.UserName, Email = user.Email };
				IdentityResult result = await _userManage.CreateAsync(newUser, user.Password);
				if (result.Succeeded)
				{
					// Set quyền cho người dùng mới
					await _userManage.AddToRoleAsync(newUser, user.Role);

					TempData["success"] = "Tạo thành công";
					if (user.Role == "Admin")
					{
						// Gán quyền Admin cho người dùng mới
						await _userManage.AddToRoleAsync(newUser, "Admin");
					}

					return Redirect("/TaiKhoan/Login");
				}
				foreach (IdentityError error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(user);
		}

		public async Task<IActionResult> LogOut(string returnUrl = "/")
		{
			await _signInManage.SignOutAsync();
			return Redirect(returnUrl);
		}
		public IActionResult QuenMatKhau()
		{
			return View();
		}

	}
}
