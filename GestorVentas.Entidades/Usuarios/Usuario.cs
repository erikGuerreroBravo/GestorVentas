using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestorVentas.Entidades.Usuarios
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe tener más de 100 caracteres, ni menos de 3 caracteres")]
        public string Nombre { get; set; }
        public string Tipo_Documento { get; set; }
        public string Num_Documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password_Hash { get; set; }
        [Required]
        public byte[] Password_Salt { get; set; }
        public bool Condicion { get; set; }
        [Required]
        public int IdRol { get; set; }
        [ForeignKey("idrol")]
        public Rol Rol { get; set; }

    }
}
