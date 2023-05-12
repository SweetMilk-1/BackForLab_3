using BackForLab_3.Services.Cache;
using BackForLab_3.Services.Converters;
using BackForLab_3.Services.Dictionaries;
using BackForLab_3.Services.MongoDb;
using BackForLab_3.Services.Musics;
using BackForLab_3.Services.Posts;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region My Services
builder.Services.AddStackExchangeRedisCache(options => {
    options.Configuration = "localhost";
    options.InstanceName = "local";
});
builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient<IDictionaryService, DictionaryService>();
builder.Services.AddTransient<IPostService, PostService>();
builder.Services.AddSingleton<IMongoContext, MongoContext>();
builder.Services.AddSingleton<IBsonConverter, BsonConverter>();
builder.Services.AddSingleton<IMusicService, MusicService>();
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();   

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
