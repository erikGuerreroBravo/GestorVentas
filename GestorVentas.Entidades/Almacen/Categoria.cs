using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorVentas.Entidades.Almacen
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        [Required]
        [StringLength(50,MinimumLength =3,ErrorMessage ="El Nombre de la categoria es requerido")]
        public string Nombre{ get; set; }
        [StringLength(256)]
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }

        public ICollection<Articulo> Articulos { get; set; }
    }
}
