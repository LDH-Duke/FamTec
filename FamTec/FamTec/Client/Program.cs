
using FamTec.Client;
using FamTec.Client.Middleware;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Tewr.Blazor.FileReader;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddFileReaderService(options =>
{
    options.UseWasmSharedBuffer = true;
});

builder.Services.AddScoped<SessionService>();
builder.Services.AddBlazoredSessionStorage();

// 연결
HubObject.hubConnection = new HubConnectionBuilder()
    .WithUrl("http://123.2.156.148:5245/broadcastHub",transports:Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets | Microsoft.AspNetCore.Http.Connections.HttpTransportType.ServerSentEvents | Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling) // 전송방법 3개 
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
        // This will set ALL logging to Debug level
        logging.SetMinimumLevel(LogLevel.Debug);
    })
    .Build();

HubObject.hubConnection.KeepAliveInterval = System.TimeSpan.FromSeconds(15); //최소 설정가능한 값5초.
HubObject.hubConnection.ServerTimeout = System.TimeSpan.FromSeconds(30); // 서버로부터 30초 안에 메시지를 수신 못하면 클라이언트가 끊음

// 집에서 추가
HubObject.hubConnection.Closed += async (exception) =>
{
    if (exception == null)
    {
        
        //await Console.WriteLine("Connection closed without error.");
    }
    else
    {
        //Console.WriteLine($"Connection closed due to an error: {exception}");
    }
};



await HubObject.hubConnection.StartAsync();

await builder.Build().RunAsync();
