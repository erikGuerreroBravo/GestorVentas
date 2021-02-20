using GestorVentas.Datos;
using GestorVentas.Models.Almacen.Articulo;
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
    public class ArticulosController : ControllerBase
    {
        private readonly Contexto _contexto;

        public ArticulosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        //Get:api/Articulos/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloVM>> Listar()
        {
            
            var articulos = await _contexto.Articulos.Include(a=>a.Categoria).ToListAsync();
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
            var articulo = await _contexto.Articulos.Include(a =>a.Categoria).SingleOrDefaultAsync(a =>a.IdArticulo==id);
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



    }
}
