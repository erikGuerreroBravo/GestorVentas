using GestorVentas.Datos;
using GestorVentas.Entidades.Almacen;
using GestorVentas.Models.Almacen.Articulo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("Todos")]
    public class ArticulosController : ControllerBase
    {
        private readonly Contexto _contexto;

        public ArticulosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        //Get:api/Articulos/Listar
        [Authorize(Roles="Almacenero,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloVM>> Listar()
        {
            
            var articulos = 
                await _contexto.Articulos
                .Include(a => a.Categoria).ToListAsync();///
            return articulos.Select(a => new ArticuloVM
            {
                IdArticulo = a.IdArticulo,
                IdCategoria = a.IdCategoria,
                Codigo = a.Codigo,
                Nombre= a.Nombre,
                Precio_Venta = a.Precio_Venta,
                Stock =a.Stock,
                Descripcion = a.Descripcion,
                Condicion = a.Condicion,
                Categoria = a.Categoria.Nombre

            }); 
        }
        //Get:api/Articulos/Mostrar/7
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var articulo = await _contexto.Articulos
                .Include(a =>a.Categoria)
                .SingleOrDefaultAsync(a =>a.IdArticulo==id);
            if (articulo == null)
            {
                return NotFound();
            }
            return Ok(new ArticuloVM
            {
                IdArticulo = articulo.IdArticulo,
                IdCategoria = articulo.IdCategoria,
                Codigo = articulo.Codigo,
                Nombre = articulo.Nombre,
                Precio_Venta = articulo.Precio_Venta,
                Stock = articulo.Stock,
                Descripcion = articulo.Descripcion,
                Condicion = articulo.Condicion,
                Categoria = articulo.Categoria.Nombre

            }); 

            
        }
        //POST:api/Articulos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] ArticuloCrearVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Categoria categoria = new Categoria { IdCategoria = model.IdCategoria, Condicion = true };
            Articulo articulo = new Articulo {
               
                IdCategoria = model.IdCategoria,
                Codigo = model.Codigo,
                Nombre = model.Nombre,
                Precio_Venta = model.Precio_Venta,
                Stock = model.Stock,
                Descripcion = model.Descripcion,
                Condicion = true,
                Categoria = categoria
            };
            _contexto.Articulos.Add(articulo);
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
        //PUT: api/Articulos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ArticuloActualizarVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (model.IdArticulo <= 0)
            {
                return BadRequest();
            }

            var articulo = await _contexto.Articulos.
                FirstOrDefaultAsync(p => p.IdArticulo == model.IdArticulo);
           
            if (articulo == null)
            {
                return NotFound();
            }
            articulo.IdCategoria = model.IdCategoria;
            articulo.Codigo = model.Codigo;
            articulo.Nombre = model.Nombre;
            articulo.Precio_Venta = model.Precio_Venta;
            articulo.Stock = model.Stock;
            articulo.Descripcion = model.Descripcion;
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


        //PUT: api/Articulos/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var articulo = await _contexto.Articulos.FirstOrDefaultAsync(p=> p.IdArticulo == id);
            
            if (articulo == null)
            {
                return NotFound();
            }
            articulo.Condicion = false;

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

        //PUT: api/Articulos/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var articulo = await _contexto.Articulos.FirstOrDefaultAsync(p => p.IdArticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }
            articulo.Condicion = true;

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


        public bool ArticuloExists(int id)
        {
            return _contexto.Articulos.Any(e => e.IdArticulo == id);
        }



    }
}
