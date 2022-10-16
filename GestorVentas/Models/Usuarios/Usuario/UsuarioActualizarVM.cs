using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GestorVentas.Models.Usuarios.Usuario
{
    public class UsuarioActualizarVM
    {
        public int IdUsuario { get; set; }
        [Required]
        public int IdRol { get; set; }
        public string Rol { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, 
            ErrorMessage = "El nombre no debe tener más de 100 caracteres, ni menos de 3 caracteres")]
        public string Nombre { get; set; }
        public string Tipo_Documento { get; set; }
        public string Num_Documento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public bool Act_Password { get; set; }

    }
}
