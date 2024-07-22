using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using OnlineShop.Web;
using OnlineShop.Web.Services.Contracts;
using OnlineShop.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7066/")});
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShopCartService, ShopCartService>();
await builder.Build().RunAsync();
