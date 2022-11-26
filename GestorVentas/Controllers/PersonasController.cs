using GestorVentas.Datos;
using GestorVentas.Entidades.Ventas;
using GestorVentas.Models.Ventas.Persona;
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

        //Get:api/Personas/ListarClientes
        [Authorize(Roles = "Vendedor,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonaVM>> ListarClientes()
        {
            var personas = await _contexto.Personas.Where(p=> p.tipo_persona=="Cliente").ToListAsync();
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

        //Get:api/Personas/ListarProveedores
        [Authorize(Roles = "Almacenero,Administrador")]
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


        //Get:api/Personas/SelectProveedores
        [Authorize(Roles = "Almacenero,Administrador")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<SelectVM>> SelectProveedores()
        {
            var persona = await _contexto.Personas.Where(p=> p.tipo_persona=="Proveedor").ToListAsync();
            return persona.Select(p => new SelectVM
            {
                idpersona = p.idPersona,
                nombre = p.nombre,

            });

        }




        //POST: api/Personas/Crear
        [Authorize(Roles = "Almacenero,Administrador,Vendedor")]
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


        // PUT: api/Personas/Actualizar
        [Authorize(Roles = "Almacenero,Administrador,Vendedor")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] PersonasActualizarVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idPersona <= 0)
            {
                return BadRequest();
            }
            var persona = await _contexto.Personas.FirstOrDefaultAsync(u => u.idPersona == model.idPersona);
            if (persona == null)
            {
                return NotFound();
            }

            
            persona.nombre = model.nombre;
            persona.tipo_documento = model.tipo_documento;
            persona.num_documento = model.num_documento;
            persona.direccion = model.direccion;
            persona.telefono = model.telefono;
            persona.email = model.email.ToLower();
            
            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        private bool PersonaExists(int id)
        {
            return _contexto.Personas.Any(e => e.idPersona == id);
        }


    }
}
