using ATM.Models;
using Microsoft.EntityFrameworkCore;

namespace ATM.DataBase
{
    public class DataBaseContext : DbContext
    {
        public DbSet<CassettesInfo> Cassettes { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=LocalATM.db");
        }
    }
}
