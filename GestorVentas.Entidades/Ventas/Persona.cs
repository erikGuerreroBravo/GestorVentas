using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GestorVentas.Entidades.Ventas
{
    public class Persona
    {
        public int idPersona { get; set; }
        [Required]
        public string tipo_persona { get; set; }
        [Required]
        [StringLength(100,MinimumLength =3,ErrorMessage ="El campo debe contener al menos 3 caracteres y como maximo 100 caracteres")]
        public string nombre { get; set; }

        public string tipo_documento { get; set; }
        public string num_documento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public byte[] password_hash { get; set; }
        public byte[] password_salt { get; set; }
        public bool condicion { get; set; }

    }
}
