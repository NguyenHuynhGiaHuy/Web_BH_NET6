using Castle.Core.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShopHuyNhu.Models;
using ShopHuyNhu.Repository;
using ShopHuyNhu.Repository.Components;

namespace ShopHuyNhu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("appsettings.json");
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectedDb"]);
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(15);
                option.Cookie.IsEssential = true;
            });

			

			builder.Services.AddIdentity<AppUserModel, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

            builder.Services.AddRazorPages();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false; // ko yc kí tự đặt biệt
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                //Lockout settings.

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                //options.User.AllowedUserNameCharacters =
                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            var app = builder.Build();
            app.UseStatusCodePagesWithRedirects("/Home/Error");
            app.UseSession();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area:exists}/{controller=SanPham}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "danhmuc",
                    pattern: "/danhmuc/{Slug?}",
                    defaults: new { controller = "DanhMuc", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "brand",
                    pattern: "/brand/{Slug?}",
                    defaults: new { controller = "Brand", action = "Index" });
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

			

			var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
            SeedData.SeedingData(context);

            app.Run();
        }
    }
}
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using ShopHuyNhu.Models;
//using ShopHuyNhu.Repository;
//using ShopHuyNhu.Repository.Components;

//namespace ShopHuyNhu
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);
//            builder.Configuration.AddJsonFile("appsettings.json");
//            //builder.Services.AddScoped<DanhMucsViewComponent>();

//            builder.Services.AddDbContext<DataContext>(options =>
//            {
//                options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectedDb"]);
//            });


//            // Add services to the container.
//            builder.Services.AddControllersWithViews();
//            builder.Services.AddDistributedMemoryCache();
//            builder.Services.AddSession(option =>
//            {
//                option.IdleTimeout = TimeSpan.FromMinutes(15);
//                option.Cookie.IsEssential = true;
//            });

//            builder.Services.AddIdentity<AppUserModel, IdentityRole>()
//    .AddEntityFrameworkStores<DbContext>().AddDefaultTokenProviders();
//            builder.Services.AddRazorPages();

//            builder.Services.Configure<IdentityOptions>(options =>
//            {
//                // Password settings.
//                options.Password.RequireDigit = true;
//                options.Password.RequireLowercase = true;
//                options.Password.RequireNonAlphanumeric = false; //yc kí tự đặt biệt
//                options.Password.RequireUppercase = false;
//                options.Password.RequiredLength = 6;
//                // options.Password.RequiredUniqueChars = 1;

//                // Lockout settings.
//                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//                options.Lockout.MaxFailedAccessAttempts = 5;
//                options.Lockout.AllowedForNewUsers = true;

//                // User settings.
//                //options.User.AllowedUserNameCharacters =
//                //"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//                options.User.RequireUniqueEmail = true;
//            });
//            var app = builder.Build();
//            app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");
//            app.UseSession();

//            // Configure the HTTP request pipeline.
//            if (!app.Environment.IsDevelopment())
//            {
//                app.UseExceptionHandler("/Home/Error");
//            }
//            app.UseStaticFiles();

//            app.UseRouting();

//            app.UseAuthentication();

//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllerRoute(
//                    name: "Areas",
//                    pattern: "{area:exists}/{controller=SanPham}/{action=Index}/{id?}");
//            });
//            app.MapControllerRoute(
//                name: "danhmuc",
//                pattern: "/danhmuc/{Slug?}",
//                defaults: new { controller = "DanhMuc", action = "Index" });
//            app.MapControllerRoute(
//               name: "brand",
//               pattern: "/brand/{Slug?}",
//               defaults: new { controller = "Brand", action = "Index" });


//            app.MapControllerRoute(
//                name: "default",
//                pattern: "{controller=Home}/{action=Index}/{id?}");




//            //SEEDINGDATA
//            var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
//            SeedData.SeedingData(context);

//            app.Run();
//        }
//    }
//}