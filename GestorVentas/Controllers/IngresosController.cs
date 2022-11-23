using GestorVentas.Datos;
using GestorVentas.Models.Almacen.Ingreso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
        [Authorize(Roles = "Almacenero,Administrador")]
        [HttpGet("[action]")]
        public async Task<IOrderedEnumerable<IngresoVM>> Listar()
        {
            var ingreso = await contexto.Ingresos
                .Include(i => i.usuario)
                .Include(i => i.persona)
                .OrderByDescending(i=> i.idingreso)
                .Take(100)
                .ToListAsync();

        }
        private bool IngresoExists(int id)
        {
            return contexto.Ingresos.Any(e=> e.idingreso==id);
        }
    }
}
