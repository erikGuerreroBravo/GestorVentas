using GestorVentas.Datos.Mapping.Almacen;
using GestorVentas.Datos.Mapping.Usuarios;
using GestorVentas.Entidades.Almacen;
using GestorVentas.Entidades.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace GestorVentas.Datos
{
    public class Contexto:DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public Contexto(DbContextOptions<Contexto> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CaterogiaMap());
            modelBuilder.ApplyConfiguration(new ArticuloMap());
            modelBuilder.ApplyConfiguration(new RolMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

    }
}
