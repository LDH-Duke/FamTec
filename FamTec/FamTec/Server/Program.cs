using FamTec.Server;
using FamTec.Server.Databases;
using FamTec.Server.Hubs;
using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Building;
using FamTec.Server.Repository.Floor;
using FamTec.Server.Repository.Place;
using FamTec.Server.Repository.User;
using FamTec.Server.Services.Login;
using FamTec.Server.Services.Place;
using FamTec.Server.Services.User;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IPlaceInfoRepository, PlaceInfoRepository>();
builder.Services.AddTransient<IBuildingInfoRepository, BuildingInfoRepository>();
builder.Services.AddTransient<IUserInfoRepository, UserInfoRepository>();
builder.Services.AddTransient<IAdminUserInfoRepository, AdminUserInfoRepository>();
builder.Services.AddTransient<IAdminPlacesInfoRepository, AdminPlaceInfoRepository>();
builder.Services.AddTransient<IFloorInfoRepository, FloorInfoRepository>();



// Add services to the container.
builder.Services.AddTransient<IPlaceService, PlaceService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAccountService, AccountService>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


#region DB연결 정보
var connstr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<WorksContext>(options =>
    options.UseMySql(connstr, ServerVersion.AutoDetect(connstr)));
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

/*
app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Get && context.Request.Query["mdw"] == "test")
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Middleware running.\n");
    }

    await next();
});
*/
//app.MapGet("/", () => "Hello World!");

//app.MapGet("/hi", () => "Hello!");
/*
app.Use(async (context, next) =>
{
    Console.WriteLine("Use Middleware1 Incoming Request \n");

    await next();
    Console.WriteLine("Use Middleware1 Outgoing Response \n");
});

app.Use(async (context, next) =>
{
    Console.WriteLine("Use Middleware2 Incoming Request \n");
    await next();
    Console.WriteLine("Use Middleware2 Outgoing Response \n");
});
app.Run();
*/

WorksSetting settings = new();
await settings.DefaultSetting();

app.Run();
