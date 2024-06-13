
using FamTec.Client;
using FamTec.Client.Middleware;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


builder.Services.AddScoped<SessionService>();
builder.Services.AddBlazoredSessionStorage();

// ¿¬°á
HubObject.hubConnection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5245/broadcastHub")
    .Build();

await HubObject.hubConnection.StartAsync();

await builder.Build().RunAsync();
