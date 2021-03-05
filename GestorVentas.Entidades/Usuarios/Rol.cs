using System.ComponentModel.DataAnnotations;

namespace GestorVentas.Entidades.Usuarios
{
    public class Rol
    {
        public int IdRol { get; set; }
        [Required]
        [StringLength(30,MinimumLength =3,ErrorMessage ="El nombre no debe tener más de 30 caracteres, ni menos de 3 caracteres")]
        public string Nombre { get; set; }
    }
}
