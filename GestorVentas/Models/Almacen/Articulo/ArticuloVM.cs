using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorVentas.Models.Almacen.Articulo
{
    public class ArticuloVM
    {
        public int IdArticulo { get; set; }
       
        public int IdCategoria { get; set; }

        public string Categoria { get; set; }

        public string Codigo { get; set; }
        
        public string Nombre { get; set; }
       
        public decimal Precio_Venta { get; set; }
        
        public int Stock { get; set; }
        public string Descripcion { get; set; }
        public bool Condicion { get; set; }

        
    }
}
