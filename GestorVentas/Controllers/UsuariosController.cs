using GestorVentas.Datos;
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
            var usuarios = await _contexto.Usuarios.ToListAsync();
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
                 condicion =u.condicion
            });

        }


        private bool UusarioExists(int id)
        {
            return _contexto.Usuarios.Any(u=> u.IdUsuario == id);
        }
    }
}
