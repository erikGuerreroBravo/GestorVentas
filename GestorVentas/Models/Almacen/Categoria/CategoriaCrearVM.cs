using System.ComponentModel.DataAnnotations;

namespace GestorVentas.Models.Almacen
{
    public class CategoriaCrearVM
    {
        
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El Nombre de la categoria es requerido")]
        public string Nombre { get; set; }
        [StringLength(256)]
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
    }
}
