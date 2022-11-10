using GestorVentas.Entidades.Ventas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestorVentas.Datos.Mapping.Ventas
{
    public class PersonaMap : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("persona")
                .HasKey(p => p.idPersona);
        }
    }
}
