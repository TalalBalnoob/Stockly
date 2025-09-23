namespace Stockly.Middleware;

public class ErrorHanlingMiddleware {
	private readonly RequestDelegate _next;
	private readonly ILogger<ErrorHanlingMiddleware> _logger;
	private readonly IWebHostEnvironment _env;

	public ErrorHanlingMiddleware(RequestDelegate next, ILogger<ErrorHanlingMiddleware> logger, IWebHostEnvironment env) {
		_next = next;
		_logger = logger;
		_env = env;
	}

	public async Task Invoke(HttpContext context) {
		try {
			await _next(context);
		}
		catch (Exception e) {
			_logger.LogError(e, "Unhandled exception");

			context.Request.ContentType = "application/json";
			context.Response.StatusCode = 500;

			var response = new {
				message = _env.IsDevelopment() ? e.Message : "Internal Server Error",
				stack = _env.IsDevelopment() ? e.StackTrace : null
			};

			await context.Response.WriteAsJsonAsync(response);
		}
	}
}
