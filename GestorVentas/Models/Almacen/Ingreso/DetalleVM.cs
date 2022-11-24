using System.ComponentModel.DataAnnotations;

namespace GestorVentas.Models.Almacen.Ingreso
{
    public class DetalleVM
    {
        [Required]
        public int idarticulo { get; set; }
        [Required]
        public int cantidad { get; set; }
        [Required]
        public decimal precio { get; set; }
    }
}
