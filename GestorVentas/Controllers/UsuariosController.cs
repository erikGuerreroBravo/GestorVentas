using GestorVentas.Datos;
using GestorVentas.Entidades.Usuarios;
using GestorVentas.Models.Usuarios.Usuario;
using GestorVentas.Models.Ventas.Persona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GestorVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly Contexto _contexto;
        private readonly IConfiguration _config;

        public UsuariosController(Contexto contexto, IConfiguration config)
        {
            _contexto = contexto;
            _config = config;
        }

        //Get:api/Usuarios/Listar
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var email = loginVM.email.ToLower();
            var usuario = await _contexto.Usuarios.Where(u=> u.Condicion==true).Include(u => u.Rol).FirstOrDefaultAsync(u => u.Email == email);
            if (usuario == null)
            {
                return NotFound();
            }
            //validamos las conicidencias del password
            if (!VerficarPasswordHash(loginVM.password, usuario.Password_Hash, usuario.Password_Salt))
            {
                return NotFound();
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,usuario.IdUsuario.ToString()),
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Role,usuario.Rol.Nombre),
                new Claim("idusuario",usuario.IdUsuario.ToString()),
                new Claim("rol",usuario.Rol.Nombre),
                new Claim("nombre",usuario.Nombre)
            };
            return Ok(
                new { token = GenerarToken(claims)}
                );

        }
        /// <summary>
        /// Validacion del password y comparador de contraseñas
        /// </summary>
        /// <param name="passsword">el password del usuario</param>
        /// <param name="passwordHashAlmacenado">el password del usuario hasheado</param>
        /// <param name="passwordSalt">el password del usuario hasheado con salt</param>
        /// <returns>regresa un valor true/false </returns>
        private bool VerficarPasswordHash(string passsword, byte[] passwordHashAlmacenado, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)) 
            {
                var passwordHashNuevo = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passsword));
                //comparaacion entre el hash almacenado y el hashsalt
                return new ReadOnlySpan<byte>(passwordHashAlmacenado).SequenceEqual(new ReadOnlySpan<byte>(passwordHashNuevo));
            }
        }
        private string GenerarToken(List<Claim> claims)
        {
            //se genera la clave simetrica
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            ///se genera el tokekn de seguridad
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds,
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private bool UsarioExists(int id)
        {
            return _contexto.Usuarios.Any(u=> u.IdUsuario == id);
        }
    }
}
