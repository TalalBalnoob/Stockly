using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Stockly.Application.Interfaces.IRepository;
using Stockly.Application.Interfaces.Services;
using Stockly.Application.Interfaces.UseCases;
using Stockly.Application.Services;
using Stockly.Application.UseCases.CancelOrder;
using Stockly.Application.UseCases.CreateNewOrder;
using Stockly.Application.UseCases.CreateProductWithStock;
using Stockly.Application.UseCases.DeleteOrder;
using Stockly.Application.UseCases.DeleteProductAndStock;
using Stockly.Application.UseCases.UpdateOrder;
using Stockly.Infrastructure;
using Stockly.Infrastructure.Repositories;
using IStockService = Stockly.Application.Interfaces.Services.IStockService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
	.AddJsonOptions(options =>
		options.JsonSerializerOptions.Converters.Add(
			new JsonStringEnumConverter()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// db config 
builder.Services.AddDbContext<AppDbContext>(options => { options.UseSqlite("Data Source=stockly.db"); });

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockAdjustmentRepository, StockAdjustmentRepository>();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStockAdjustmentService, StockAdjustmentService>();

builder.Services.AddScoped<ICreateNewOrderUseCase, CreateNewOrderUseCase>();
builder.Services.AddScoped<ICreateProductWithStockUseCase, CreateProductWithStockUseCase>();
builder.Services.AddScoped<IDeleteOrderUseCase, DeleteOrderUseCase>();
builder.Services.AddScoped<IDeleteProductAndStock, DeleteProductAndStock>();
builder.Services.AddScoped<IUpdateOrderUseCase, UpdateOrderUseCase>();
builder.Services.AddScoped<ICancelOrderUseCase, CancelOrderUseCase>();
builder.Services.AddScoped<IReturnOrderUseCase, ReturnOrderUseCase>();

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