using GestorVentas.Datos.Mapping.Almacen;
using GestorVentas.Datos.Mapping.Usuarios;
using GestorVentas.Datos.Mapping.Ventas;
using GestorVentas.Entidades.Almacen;
using GestorVentas.Entidades.Usuarios;
using GestorVentas.Entidades.Ventas;
using Microsoft.EntityFrameworkCore;

namespace GestorVentas.Datos
{
    public class Contexto:DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Articulo> Articulos { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Persona> Personas { get; set; }

        public DbSet<Ingreso> Ingresos { get; set; }

        public DbSet<DetalleIngreso> DetalleIngresos { get; set; }
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
            modelBuilder.ApplyConfiguration(new PersonaMap());
            modelBuilder.ApplyConfiguration(new IngresoMap());
            modelBuilder.ApplyConfiguration(new DetalleIngresoMap());
        }

    }
}
