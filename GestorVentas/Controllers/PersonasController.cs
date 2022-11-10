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

        //Get:api/Usuarios/ListarClientes
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaVM>> ListarClientes()
        {
            var personas = await _contexto.Personas.Where(p=> p.tipo_persona=="Clientes").ToListAsync();
            return personas.Select(p => new PersonaVM
            {
                idPersona = p.idPersona,
                tipo_persona = p.tipo_persona,
                nombre = p.nombre,
                tipo_documento= p.tipo_documento,
                num_documento=p.num_documento,
                direccion=p.direccion, 
                telefono=p.telefono,
                email=p.email,
            });
        }
        //Get:api/Usuarios/ListarProveedores
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaVM>> ListarProveedores()
        {
            var personas = await _contexto.Personas.Where(p => p.tipo_persona == "Proveedor").ToListAsync();
            return personas.Select(p => new PersonaVM
            {
                idPersona = p.idPersona,
                tipo_persona = p.tipo_persona,
                nombre = p.nombre,
                tipo_documento = p.tipo_documento,
                num_documento = p.num_documento,
                direccion = p.direccion,
                telefono = p.telefono,
                email = p.email,
            });

        }


        //POST: api/Usuarios/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = model.email.ToLower();

            if (await _contexto.Personas.AnyAsync(p => p.email == email))
            {
                return BadRequest("El email ya existe");
            }
            Persona persona = new Persona
            {
                tipo_persona = model.tipo_persona,
                nombre = model.nombre,
                tipo_documento = model.tipo_documento,
                num_documento = model.num_documento,
                direccion = model.direccion,
                telefono = model.telefono,
                email = model.email.ToLower(),
                condicion = true,
                
            };
            _contexto.Personas.Add(persona);
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

        private bool PersonaExists(int id)
        {
            return _contexto.Personas.Any(e => e.idPersona == id);
        }


    }
}
