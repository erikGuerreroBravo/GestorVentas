using System.ComponentModel.DataAnnotations;

namespace GestorVentas.Models.Almacen.Articulo
{
    public class ArticuloCrearVM
    {
        
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
