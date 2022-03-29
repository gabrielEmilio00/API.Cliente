using API.Cliente.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Cliente.Context
{
    public class ClienteContext : DbContext
    {
        public DbSet<ClienteModel> Clientes { get; set; }

        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteModel>(c =>
            {
                c.ToTable("Cliente");
                c.HasKey(k => k.Id);
            });
        }
    }
}
