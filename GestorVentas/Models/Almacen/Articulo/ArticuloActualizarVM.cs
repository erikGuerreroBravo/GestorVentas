using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorVentas.Models.Almacen.Articulo
{
    public class ArticuloActualizarVM
    {
        [Required]
        public int IdArticulo { get; set; }
        [Required]
        public int IdCategoria { get; set; }

        public string Categoria { get; set; }

        public string Codigo { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Nombre del articulo es requerido")]
        public string Nombre { get; set; }
        [Required]
        public decimal Precio_Venta { get; set; }
        [Required]
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        
    }
}
