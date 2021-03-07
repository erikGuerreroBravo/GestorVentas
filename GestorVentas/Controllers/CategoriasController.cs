using GestorVentas.Datos;
using GestorVentas.Entidades.Almacen;
using GestorVentas.Models.Almacen;
using GestorVentas.Models.Almacen.Categoria;
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

        //Get:api/Categorias/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectCategoriaVM>> Seleccionar()
        {
            var categoria = await _contexto.Categorias.Where(c=> c.Condicion== true).ToListAsync();
            return categoria.Select(p => new SelectCategoriaVM
            {
                IdCategoria = p.IdCategoria,
                Nombre = p.Nombre,
                
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


        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] CategoriaActualizarVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.IdCategoria <= 0)
            {
                return BadRequest();
            }
            var categoria = await _contexto.Categorias
                .FirstOrDefaultAsync(p => p.IdCategoria == model.IdCategoria);

            if (categoria == null)
            {
                return NotFound();
            }
            categoria.Nombre = model.Nombre;
            categoria.Descripcion = model.Descripcion;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
               
            }

            return Ok();

        }

        //POST: api/Categorias/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CategoriaCrearVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Categoria categoria = new Categoria
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Condicion = true
            };
            _contexto.Categorias.Add(categoria);
            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (System.Exception ex)
            {
                string mensaje = ex.Message;
                return BadRequest(mensaje);
            }

            return Ok();
        }

        //DELETE: api/Categorias/Desactivar/1
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var categoria = await _contexto.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            _contexto.Categorias.Remove(categoria);
            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
            return Ok();
        }

        //PUT: api/Categorias/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
          
            var categoria = await _contexto.Categorias
                .FirstOrDefaultAsync(p => p.IdCategoria == id);

            if (categoria == null)
            {
                return NotFound();
            }
            categoria.Condicion = false;
           
            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();

            }

            return Ok();

        }

        //PUT: api/Categorias/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var categoria = await _contexto.Categorias
                .FirstOrDefaultAsync(p => p.IdCategoria == id);

            if (categoria == null)
            {
                return NotFound();
            }
            categoria.Condicion = true;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();

            }

            return Ok();

        }

    }
}
