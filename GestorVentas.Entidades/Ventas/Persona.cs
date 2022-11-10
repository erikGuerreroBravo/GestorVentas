using System;
using System.Collections.Generic;
using System.Text;

namespace GestorVentas.Entidades.Ventas
{
    public class Persona
    {
        public int idPersona { get; set; }
        public int idRol { get; set; }

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
