using Microsoft.EntityFrameworkCore;

namespace Stockly.Data.Persistence;

public class StocklyDbContext : DbContext
{
    public StocklyDbContext(DbContextOptions<StocklyDbContext> options)
        : base(options)
    {
    }
}
