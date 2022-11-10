using GestorVentas.Datos;
using GestorVentas.Models.Usuarios.Rol;
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
    public class RolesController : ControllerBase
    {
        private readonly Contexto _contexto;

        public RolesController(Contexto contexto)
        {
            _contexto = contexto;
        }

        //Get:api/Roles/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolVM>> Listar()
        {
            var roles = await _contexto.Roles.ToListAsync();
            return roles.Select(p => new RolVM
            {
                IdRol = p.IdRol,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Condicion = p.Condicion
            });

        }

        //Get:api/Roles/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectRolVM>> Seleccionar()
        {
            var roles = await _contexto.Roles.
                Where(c => c.Condicion == true)
               .ToListAsync();

           
            return roles.Select(p => new SelectRolVM
            {
                IdRol = p.IdRol,
                Nombre = p.Nombre,

            });

        }
        private bool RolExists(int id)
        {
            return _contexto.Roles.Any(e=> e.IdRol==id);
        }

    }
}
