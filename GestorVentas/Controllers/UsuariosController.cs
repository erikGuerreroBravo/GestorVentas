using GestorVentas.Datos;
using GestorVentas.Entidades.Usuarios;
using GestorVentas.Models.Usuarios.Usuario;
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
    public class UsuariosController : ControllerBase
    {
        private readonly Contexto _contexto;

        public UsuariosController(Contexto contexto)
        {
            _contexto = contexto;
        }

        //Get:api/Usuarios/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioVM>> Listar()
        {
            var usuarios = await _contexto.Usuarios.Include(u=>u.Rol).ToListAsync();
            return usuarios.Select(u => new UsuarioVM
            {
                 IdUsuario = u.IdUsuario,
                 IdRol = u.IdRol,
                 Rol= u.Rol.Nombre,
                 Nombre =u.Nombre,
                 Tipo_Documento =u.Tipo_Documento,
                 Num_Documento =u.Num_Documento,
                 Direccion = u.Direccion,
                 Telefono =u.Telefono,
                 Email =u.Email,
                 Password_Hash =u.Password_Hash,
                 Condicion =u.Condicion
            });

        }

        //POST: api/Usuarios/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] UsuarioCrearVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var email = model.Email.ToLower();

            if (await _contexto.Usuarios.AnyAsync(u => u.Email == email))
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
                Condicion= true,
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

        // PUT: api/Usuarios/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] UsuarioActualizarVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.IdUsuario <= 0)
            {
                return BadRequest();
            }

            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == model.IdUsuario);

            if (usuario == null)
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


        // PUT: api/Usuarios/Desactivar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Condicion = false;

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

        // PUT: api/Usuarios/Activar/1
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var usuario = await _contexto.Usuarios.FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Condicion = true;

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

        private void CrearPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        public IActionResult Login()
        { }


        private bool UsarioExists(int id)
        {
            return _contexto.Usuarios.Any(u=> u.IdUsuario == id);
        }
    }
}
