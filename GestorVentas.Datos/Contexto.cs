using GestorVentas.Datos.Mapping.Almacen;
using GestorVentas.Entidades.Almacen;
using Microsoft.EntityFrameworkCore;

namespace GestorVentas.Datos
{
    public class Contexto:DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }

        public Contexto(DbContextOptions<Contexto> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CaterogiaMap());
        }

    }
}
