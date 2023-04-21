using BackForLab_3.Services.Converters;
using BackForLab_3.Services.Dictionaries;
using BackForLab_3.Services.MongoDb;
using BackForLab_3.Services.Musics;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region My Services
builder.Services.AddTransient<IDictionaryService, DictionaryService>();
builder.Services.AddSingleton<IMongoContext, MongoContext>();
builder.Services.AddSingleton<IBsonConverter, BsonConverter>();
builder.Services.AddSingleton<IMusicService, MusicService>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app.UseStaticFiles();

app.UseRouting();   

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
