using GestorVentas.Datos;
using GestorVentas.Entidades.Ventas;
using GestorVentas.Models.Ventas.Persona;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestorVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly Contexto _contexto;
        public PersonasController(Contexto contexto)
        {
            _contexto = contexto;
        }

        //GET: api/Personas
        [HttpGet]
        public IEnumerable<Persona> GetPersonas()
        {
            return _contexto.Personas;
        }

        //Get:api/Usuarios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaVM>> Listar()
        {
            var personas = await _contexto.Personas.ToListAsync();
            return usuarios.Select(u => new PersonaVM
            {
                IdUsuario = u.IdUsuario,
                IdRol = u.IdRol,
                Rol = u.Rol.Nombre,
                Nombre = u.Nombre,
                Tipo_Documento = u.Tipo_Documento,
                Num_Documento = u.Num_Documento,
                Direccion = u.Direccion,
                Telefono = u.Telefono,
                Email = u.Email,
                Password_Hash = u.Password_Hash,
                Condicion = u.Condicion
            });

        }


        private bool PersonaExists(int id)
        {
            return _contexto.Personas.Any(e => e.idPersona == id);
        }


    }
}
