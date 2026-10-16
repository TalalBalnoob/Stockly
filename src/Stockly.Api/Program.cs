using System.Collections.ObjectModel;

using Stockly.Api.Middleware;
using Stockly.Data;
using Stockly.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddOpenApi();
// Add Services From Data and Application Layers
builder.Services.AddData(builder.Configuration);
builder.Services.AddSingleton<Collection<Order>>();
builder.Services.AddSingleton<Collection<Product>>();
builder.Services.AddSingleton<Collection<StockAdjustment>>();

builder.Services.AddApplication();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Middleware
if (app.Environment.IsDevelopment()) {
	app.MapOpenApi();

	app.UseSwaggerUI(options => {
		options.SwaggerEndpoint("/openapi/v1.json", "Stockly API v1");
	});
}

app.UseHttpsRedirection();

// app.UseAuthentication(); // Uncomment when you add auth
// app.UseAuthorization();

app.MapControllers();

app.Run();
