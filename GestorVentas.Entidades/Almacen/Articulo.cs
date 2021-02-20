using System.ComponentModel.DataAnnotations;

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
        public decimal PrecioVenta { get; set; }
        [Required]
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public bool  Condicion{ get; set; }

        public Categoria Categoria { get; set; }
    }
}
