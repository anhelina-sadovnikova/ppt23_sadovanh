using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Ppt23.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var httpClientBaseAddress = builder.Configuration["HttpClientBaseAddress"];
ArgumentNullException.ThrowIfNull(httpClientBaseAddress);
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(httpClientBaseAddress) });
builder.Services.AddLocalization();//miltilingual app

await builder.Build().RunAsync();
