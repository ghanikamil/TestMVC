using MyWebFormApp.BLL;
using MyWebFormApp.BLL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//register DI
builder.Services.AddScoped<ICategoryBLL,CategoryBLL>();
builder.Services.AddScoped<IArticleBLL,ArticleBLL>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Articles}/{action=index}/{id?}"
);

app.Run();
