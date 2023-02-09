using Limset.Models;
using Microsoft.EntityFrameworkCore;

namespace Limset.Helper
{
    public class LimSet_DbContext : DbContext
    {
        public DbSet<users> users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=MGANDAPC\SQLEXPRESS; database=Limset; User Id=sa; Password=Kasarani17b; TrustServerCertificate=True");
        }
        
    }
}
