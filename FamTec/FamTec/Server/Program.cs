using FamTec.Server.Hubs;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using Microsoft.AspNetCore.Hosting;
using FamTec.Server;
using FamTec.Server.Databases;
using FamTec.Server.Repository.Admin.AdminPlaces;
using FamTec.Server.Repository.Admin.AdminUser;
using FamTec.Server.Repository.Admin.Departmnet;
using FamTec.Server.Repository.Building;
using FamTec.Server.Repository.Floor;
using FamTec.Server.Repository.Place;
using FamTec.Server.Repository.Room;
using FamTec.Server.Repository.Unit;
using FamTec.Server.Repository.User;
using FamTec.Server.Services.Admin.Account;
using FamTec.Server.Services.Admin.Department;
using FamTec.Server.Services.Admin.Place;
using FamTec.Server.Services.Building;
using FamTec.Server.Services.Floor;
using FamTec.Server.Services.Room;
using FamTec.Server.Services.Unit;
using FamTec.Server.Services.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.FileProviders;
using FamTec.Server.Services;
using FamTec.Server.Tokens;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

//builder.Services.AddSession();
/*
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
*/

builder.Services.AddTransient<IPlaceInfoRepository, PlaceInfoRepository>();
builder.Services.AddTransient<IBuildingInfoRepository, BuildingInfoRepository>();
builder.Services.AddTransient<IUserInfoRepository, UserInfoRepository>();
builder.Services.AddTransient<IAdminUserInfoRepository, AdminUserInfoRepository>();
builder.Services.AddTransient<IAdminPlacesInfoRepository, AdminPlaceInfoRepository>();
builder.Services.AddTransient<IFloorInfoRepository, FloorInfoRepository>();
builder.Services.AddTransient<IDepartmentInfoRepository, DepartmentInfoRepository>();
builder.Services.AddTransient<IRoomInfoRepository, RoomInfoRepository>();
builder.Services.AddTransient<IUnitInfoRepository, UnitInfoRepository>();


// Add services to the container.
builder.Services.AddTransient<IAdminAccountService, AdminAccountService>();
builder.Services.AddTransient<IAdminPlaceService, AdminPlaceService>();
builder.Services.AddTransient<IBuildingService, BuildingService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IFloorService, FloorService>();
builder.Services.AddTransient<IRoomService, RoomService>();
builder.Services.AddTransient<IUnitService, UnitService>();

builder.Services.AddTransient<ILogService, LogService>();

builder.Services.AddTransient<ITokenComm, TokenComm>();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


#region JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = "https://localhost:7114/",
        ValidIssuer = "https://localhost:7114/",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:authSigningKey"]!))
    };
});
#endregion


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
=======
*/
#endregion

#region SIGNAL R CORS 등록

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("https://localhost:7114","http://localhost:5245","https://localhost:8888")
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
//app.UseSession();

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

#region MiddleWare

/*
app.UseWhen(context => context.Request.Path.Equals("/api/Login/SystemManager"), appBuilder =>
{
    appBuilder.UseMiddleware<JwtMiddleware>();
});
*/
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/Login/sign"), appBuilder =>
{
    appBuilder.UseMiddleware<JwtMiddleware>();
});



#endregion

#region 블레이저 전용 파일서버 생성
app.UseFileServer(new FileServerOptions
{
    FileProvider = new PhysicalFileProvider("N:\\개인"), // 실제경로
    RequestPath = "/System", // 호출할때 사용될 경로
    EnableDirectoryBrowsing = true // 필수옵션
});


#endregion


app.UseAuthentication();
app.UseAuthorization();

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