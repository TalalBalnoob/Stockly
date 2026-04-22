using System.Text.Json.Serialization;

using Microsoft.EntityFrameworkCore;

using Stockly.Application.Interfaces.IRepository;
using Stockly.Infrastructure;
using Stockly.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder
	.Services.AddControllers()
	.AddJsonOptions(options =>
		options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
	);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// db config
builder.Services.AddDbContext<AppDbContext>(options => {
	options.UseSqlite("Data Source=stockly.db");
});

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IStockAdjustmentRepository, StockAdjustmentRepository>();

// builder.Services.AddScoped<IProductService, ProductService>();
// builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();
// builder.Services.AddScoped<IOrderService, OrderService>();
// builder.Services.AddScoped<IStockAdjustmentService, StockAdjustmentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary) {
	public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

