using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorVentas.Models.Usuarios.Rol
{
    public class RolVM
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }
    }
}
