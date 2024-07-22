using Microsoft.EntityFrameworkCore;
using OnlineShop.Api.Data;
using OnlineShop1.API.Repository.Contracts;
using OnlineShop1.API.Repository;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ShopOnlineDBContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("ShopOnlineConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShopCartRepository, ShopCartRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7066", "https://localhost:7123")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
