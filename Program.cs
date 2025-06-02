using Microsoft.EntityFrameworkCore;
using YakitTuketimTahmini.Models; // ApplicationDbContext için

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantı dizesi (appsettings.json içindeki adıyla)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Controller + Razor View desteği
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson();

var app = builder.Build();

// Hata yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Orta katmanlar (middleware)
app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot klasörünü sunar
app.UseRouting();
app.UseAuthorization();

// Rotalama
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
