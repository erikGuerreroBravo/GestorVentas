using GestorVentas.Datos;
using GestorVentas.Models.Almacen;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly Contexto _contexto;

        public CategoriasController(Contexto contexto)
        {
            _contexto = contexto;
        }

        //Get:api/Categorias/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoriaVM>> Listar()
        {
            var categoria = await _contexto.Categorias.ToListAsync();
            return categoria.Select(p => new CategoriaVM
            {
                IdCategoria = p.IdCategoria,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Condicion = p.Condicion
            });
           
        }

        //Get:api/Categorias/Mostrar/7
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var categoria = await _contexto.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(new CategoriaVM
            {
                IdCategoria = categoria.IdCategoria,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
                Condicion = categoria.Condicion
            });
        }

    }
}
