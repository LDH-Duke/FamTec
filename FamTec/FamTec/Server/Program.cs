using FamTec.Server.Hubs;
using FamTec.Shared.Model;
using FamTec.Shared.Models;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


#region DB연결 정보
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WorksContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
#endregion


#region SIGNAL R 등록
/*
builder.Services.AddSignalR(opts =>
{
    opts.EnableDetailedErrors = true;
    opts.KeepAliveInterval = TimeSpan.FromMinutes(1);
});

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});
*/
#endregion

#region SIGNAL R CORS 등록
builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:8888")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials()
        .SetIsOriginAllowed((host) => true);
    });
});


builder.Services.AddSignalR();
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

#endregion



var app = builder.Build();

#region SIGNALR CORS 사용
app.UseCors();
#endregion

#region SIGNALR HUB 사용
app.UseResponseCompression();
app.MapHub<BroadcastHub>("/broadcastHub");
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
