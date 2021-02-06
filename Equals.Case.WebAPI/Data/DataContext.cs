using Equals.Case.WebAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Equals.Case.WebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Adquirente> Adquirentes { get; set; }
        public DbSet<Periodicidade> Periodicidades { get; set; }
    }
}