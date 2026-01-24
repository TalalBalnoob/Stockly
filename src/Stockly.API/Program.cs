using Microsoft.EntityFrameworkCore;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Application.Services;
using Stockly.Infrastructure;
using Stockly.Infrastructure.Repositories;
using IStockService = Stockly.Application.Interfaces.Services.IStockService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// db config 
builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlite("Data Source=stockly.db"); });

builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddSingleton<IStockRepository, StockRepository>();
builder.Services.AddSingleton<IStockAdjustmentRepository, StockAdjustmentRepository>();

builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<IStockService, StockService>();
builder.Services.AddSingleton<IOrderItemsService, OrderItemsService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) {
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}