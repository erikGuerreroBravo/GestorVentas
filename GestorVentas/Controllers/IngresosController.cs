using GestorVentas.Datos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GestorVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController: ControllerBase
    {
        private readonly Contexto contexto;
        public IngresosController(Contexto _contexto)
        {
            contexto = _contexto;
        }
        private bool IngresoExists(int id)
        {
            return contexto.Ingresos.Any(e=> e.idingreso==id);
        }
    }
}
