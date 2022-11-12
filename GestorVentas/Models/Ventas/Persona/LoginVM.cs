using System.ComponentModel.DataAnnotations;

namespace GestorVentas.Models.Ventas.Persona
{
    public class LoginVM
    {
        [Required]
        [EmailAddress]
        public string  email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
