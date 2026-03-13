using Microsoft.EntityFrameworkCore;
using Dominio;

namespace AplicacionWeb.Data
{
    public class SistemaPagosContext : DbContext
    {
        public SistemaPagosContext(DbContextOptions<SistemaPagosContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Equipo> Equipos { get; set; }

        public DbSet<TipoDeGasto> TiposDeGastos { get; set; }

        public DbSet<PagoUnico> PagosUnicos { get; set; }

        public DbSet<PagoRecurrente> PagosRecurrentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pago>()
                .HasDiscriminator<string>("TipoPago")
                .HasValue<PagoUnico>("Unico")
                .HasValue<PagoRecurrente>("Recurrente");
        }
    }
}