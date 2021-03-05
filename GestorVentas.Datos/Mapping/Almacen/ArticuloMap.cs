using GestorVentas.Entidades.Almacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorVentas.Datos.Mapping.Almacen
{
    public class ArticuloMap : IEntityTypeConfiguration<Articulo>
    {
        public void Configure(EntityTypeBuilder<Articulo> builder)
        {
           
            builder.ToTable("articulo")
                .HasKey(a => a.IdArticulo);
                
           
        }
    }
}
