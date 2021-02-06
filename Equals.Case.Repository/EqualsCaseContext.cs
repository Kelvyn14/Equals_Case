using Equals.Case.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Equals.Case.Repository
{
    public class EqualsCaseContext : DbContext
    {
        public EqualsCaseContext(DbContextOptions<EqualsCaseContext> options) : base(options) { }

        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Adquirente> Adquirentes { get; set; }
        public DbSet<Periodicidade> Periodicidades { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //         // modelBuilder.Entity<Arquivo>().HasKey(a => new {a.ArquivoId, a.ArquivoId});
        // }
    }
}