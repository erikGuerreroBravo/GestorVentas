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


        private bool PersonaExists(int id)
        {
            return _contexto.Personas.Any(e => e.idPersona == id);
        }


    }
}
