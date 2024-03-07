using KgysProjectIdentity.Core.OptionsModel;
using KgysProjectIdentity.Repository.Models;
using KgysProjectIdentity.Service.Mapping;
using KgysProjectIdentity.Web.Areas.Admin.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"), options =>
    {
        options.MigrationsAssembly("KgysProjectIdentity.Repository");
    });
});

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<SecurityStampValidatorOptions>(options =>
{
    options.ValidationInterval = TimeSpan.FromSeconds(1); /*FromMinutes(30);*/ //Securitystamp de�i�irse 30 dk sonunda atar.
});

builder.Services.AddIdentityWithExt();

//Token i�in konuldu
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(30);
});


//Resim Y�klemek ��in Tan�mland�
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

//Automapper.Dependcyinjection nuget y�kledikten sonra aktif oldu
builder.Services.AddAutoMapper(typeof(ViewModelMapping));

//Cookie ayaralar� i�in
builder.Services.ConfigureApplicationCookie(options =>
{
    var cookieBuilder = new CookieBuilder();
    cookieBuilder.Name = "KgysProjectCookie";  //�sim Projeye �zel verilir.
    options.LoginPath = new PathString("/Home/SignIn");
    options.LogoutPath = new PathString("/Member/Logout"); //navbardaki ��k�� butonunun y�nlendirece�i adress
    options.AccessDeniedPath = new PathString("/Member/AccessDenied"); // Yetki a��ml� sayfa y�nlendirmesi
    options.Cookie = cookieBuilder;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);//�ifre bilgilerini cookie de 1 g�n tutar
    options.SlidingExpiration = true;
});

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
app.UseAuthentication(); //Identity kimlik do�rulama i�lemleri i�in mutlaka konulmal�
app.UseAuthorization();

//Area eklendikten sonra ula�mak i�in konuldu
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
