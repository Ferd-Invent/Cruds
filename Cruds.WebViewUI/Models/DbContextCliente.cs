using Microsoft.EntityFrameworkCore;

namespace Cruds.WebViewUI.Models
{
    public class DbContextCliente : DbContext
    {
        public DbContextCliente(){}
        public DbContextCliente(DbContextOptions<DbContextCliente> options) : base(options) { }

        public virtual DbSet<Cliente> Clientes { get; set; }

    }
}
