using GestorVentas.Datos;
using GestorVentas.Models.Almacen.Ingreso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
        
        //[Authorize(Roles = "Almacenero,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<IngresoVM>> Listar()
        {
            var ingreso = await contexto.Ingresos
                .Include(i => i.usuario)
                .Include(i => i.persona)
                .OrderByDescending(i=> i.idingreso)
                .Take(100)
                .ToListAsync();
            return ingreso.Select(i => new IngresoVM
            {
                idingreso = i.idingreso,
                idproveedor = i.idproveedor,    
                proveedor= i.persona.nombre,
                idusuario= i.idusuario,
                usuario = i.usuario.Nombre,
                tipo_comprobante = i.tipo_comprobante,
                num_comprobante= i.num_comprobante,
                serie_comprobante=i.serie_comnprobante,
                fecha_hora = i.fecha_hora,
                impuesto = i.impuesto,
                total = i.total,
                estado= i.estado

            });



        }

        //POST: api/Ingresos/Crear
        [Authorize(Roles ="Almacenero,Administrador")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }




        }



        private bool IngresoExists(int id)
        {
            return contexto.Ingresos.Any(e=> e.idingreso==id);
        }
    }
}
