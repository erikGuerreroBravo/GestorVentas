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


        //POST: api/Personas/Crear
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


        // PUT: api/Personas/Actualizar
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

            usuario.IdRol = model.IdRol;
            usuario.Nombre = model.Nombre;
            usuario.Tipo_Documento = model.Tipo_Documento;
            usuario.Num_Documento = model.Num_Documento;
            usuario.Direccion = model.Direccion;
            usuario.Telefono = model.Telefono;
            usuario.Email = model.Email.ToLower();

            if (model.Act_Password == true)
            {
                CrearPasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.Password_Hash = passwordHash;
                usuario.Password_Salt = passwordSalt;
            }

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
