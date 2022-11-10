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

            CrearPasswordHash(model.Password, out byte[] passwordHash, out byte[] passwordSalt);

            Usuario usuario = new Usuario
            {
                IdRol = model.IdRol,
                Nombre = model.Nombre,
                Tipo_Documento = model.Tipo_Documento,
                Num_Documento = model.Num_Documento,
                Direccion = model.Direccion,
                Telefono = model.Telefono,
                Email = model.Email.ToLower(),
                Condicion = true,
                Password_Hash = passwordHash,
                Password_Salt = passwordSalt
            };
            _contexto.Usuarios.Add(usuario);
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
