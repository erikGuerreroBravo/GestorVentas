using GestorVentas.Entidades.Almacen;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorVentas.Datos.Mapping.Almacen
{
    public class CaterogiaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("categoria")
                .HasKey(c => c.IdCategoria);
            //builder.Property(c => c.Nombre).HasMaxLength(50);
            //builder.Property(c => c.Descripcion).HasMaxLength(256);
        }
    }
}
