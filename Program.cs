using DoAn_QLKhachSan.Models;
using DoAn_QLKhachSan.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    option.LoginPath = "/Access/Login";
    option.ReturnUrlParameter = "urlRedirect";
    option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("CustomerPolicy", policy => policy.RequireRole("Customer"));
});
var connectionString = builder.Configuration.GetConnectionString("QuanLyKhachSanContext");
builder.Services.AddDbContext<QuanLyKhachSanContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString(connectionString));
    options.UseLazyLoadingProxies(); // Kích hoạt tải lười biếng
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});
builder.Services.AddScoped<ITinhThanhService, TinhThanhService>();
builder.Services.AddScoped<IPhongService, PhongService>();
builder.Services.AddScoped<IKhachSanService, KhachSanService>();
builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseCookiePolicy();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
