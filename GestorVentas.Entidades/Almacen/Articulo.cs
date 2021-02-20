using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorVentas.Entidades.Almacen
{
    public class Articulo
    {
        public int IdArticulo  { get; set; }

        [Required]
        public int IdCategoria { get; set; }

        public string Codigo { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Nombre del articulo es requerido")]
        public string Nombre { get; set; }
        
        [Required]
        public decimal Precio_Venta { get; set; }
        
        [Required]
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public bool  Condicion{ get; set; }

        [ForeignKey("idcategoria")]
        public Categoria Categoria { get; set; }
    }
}
