using Microsoft.EntityFrameworkCore;

namespace Stockly.Infrastructure;

public class AppDbContext : DbContext {
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
	}
}