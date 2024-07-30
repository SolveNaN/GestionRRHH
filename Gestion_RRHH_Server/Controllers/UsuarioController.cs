using Gestion_RRHH_Server.Models;
using Gestion_RRHH_Server.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RRHH_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsuarioController(ApplicationDbContext context)
        {

            _context = context;
        }
        //Estado de conexion con el servidor
        [HttpGet]
        [Route("ConexionServidor")]
        public async Task<ActionResult<string>> GetConexionServidor()
        {
            return "Conectado";
        }
        //Estados de conexion con la tabla de la base de datos
        [HttpGet]
        [Route("ConexionDB")]
        public async Task<ActionResult<string>> GetConexionDB()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();
                return Ok("Buena Calidad");

            }
            catch (Exception ex)
            {
                return BadRequest("Mala calidad");
            }
        }

        [HttpGet("Datos")]
        public async Task<ActionResult<List<Usuario>>> GetCuenta()
        {
            var lista = await _context.Usuarios.ToListAsync();
            return Ok(lista);
        }

        public static Usuario usuario = new Usuario();
        [HttpPost("Registrar")]
        public async Task<ActionResult<string>> CreateCuenta(UsuarioDTO objeto)
        {
            try
            {
                CreatePasswordHash(objeto.Password, out byte[] passwordHash, out byte[] passwordSalt);
                usuario.Id = 0;
                usuario.Nombre = objeto.Nombre;
                usuario.Cedula = objeto.Cedula;
                usuario.Direccion = objeto.Direccion;
                usuario.Departamento = objeto.Departamento;
                usuario.Seccion = objeto.Seccion;               
                usuario.Rol = objeto.Rol;               
                
                usuario.PasswordHash = passwordHash;
                usuario.PasswordSalt = passwordSalt;
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                var respuesta = Ok("Registrado Con Exito");

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                return BadRequest(" Error dutante el registro");
            }

        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>> InicioSesion(UsuarioDTO objeto)
        {
            var cuenta = await _context.Usuarios.Where(x => x.Cedula == objeto.Cedula).FirstOrDefaultAsync();
            if (cuenta == null)
            {
                return BadRequest("Usuario no encontrado");
            }
            if (!VerifyPasswordHash(objeto.Password, cuenta.PasswordHash, cuenta.PasswordSalt))
            {
                return BadRequest("Contraseña incorrecta");
            }
            string token = CreateToken(cuenta);
            return Ok(token);

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }


        private string CreateToken(Usuario user)
        {
            List<Claim> claims = new List<Claim>
     {
         new Claim(ClaimTypes.Name, user.Nombre),
         new Claim(ClaimTypes.Role,user.Rol),
         new Claim(ClaimTypes.NameIdentifier,Convert.ToString(user.Id))
     };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                "PROYECTO PARA LA GESTION DE RECURSOS HUMANOS CLAVE UNICA PARA GENERACION DE TOKEN"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        [HttpGet("Listado")]
        public async Task<ActionResult<List<Usuario>>> GetEjemplos()
        {
            var lista = await _context.Usuarios.ToListAsync();
            return Ok(lista);
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult<string>> DeleteUsuario(int id)
        {
            var DbObjeto = await _context.Usuarios.FirstOrDefaultAsync(Ob => Ob.Id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }

            try
            {
                _context.Usuarios.Remove(DbObjeto);
                await _context.SaveChangesAsync();

                return "Eliminado correctamente";
            }
            catch (Exception ex)
            {
                return "No fué posible eliminar el objeto";
            }

        }
    }
}
