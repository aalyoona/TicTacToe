using Microsoft.EntityFrameworkCore;

namespace TicTacToe.DL
{
    public class TicTacToeDbContext : DbContext
    {
        public TicTacToeDbContext(DbContextOptions<TicTacToeDbContext> options) : base(options) { }

        public DbSet<GameEntity> Games { get; set; }
    }
}
