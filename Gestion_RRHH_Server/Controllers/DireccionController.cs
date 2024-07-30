using Gestion_RRHH_Server.Models;
using Gestion_RRHH_Server.Server.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestion_RRHH_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DireccionController(ApplicationDbContext context)
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
                var usuarios = await _context.Direccions.ToListAsync();
                return Ok("Buena Calidad");

            }
            catch (Exception ex)
            {
                return BadRequest("Mala calidad");
            }
        }
        //Aqui comienza el CRUD
        [HttpGet("Listado")]
        public async Task<ActionResult<List<Direccion>>> GetDireccions()
        {
            var lista = await _context.Direccions.ToListAsync();
            return Ok(lista);
        }


        [HttpGet]
        [Route("ConsutarId/{id}")]
        public async Task<ActionResult<List<Direccion>>> GetSingleDireccion(int id)
        {
            var miobjeto = await _context.Direccions.FirstOrDefaultAsync(ob => ob.Id == id);
            if (miobjeto == null)
            {
                return NotFound(" :/");
            }

            return Ok(miobjeto);
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<string>> CreateDireccion(Direccion objeto)
        {
            try
            {
                _context.Direccions.Add(objeto);
                await _context.SaveChangesAsync();
                return "Creado con éxio";
            }
            catch (Exception ex)
            {
                return "Error durante el procesi de almacenamiento";
            }

        }

        [HttpPut("Actualizar/{id}")]
        public async Task<ActionResult<List<Direccion>>> UpdateDireccion(Direccion objeto)
        {

            var DbObjeto = await _context.Direccions.FindAsync(objeto.Id);
            if (DbObjeto == null)
                return BadRequest("no se encuentra");
            //DbObjeto.Nombre = objeto.Nombre;
            await _context.SaveChangesAsync();
            return Ok(await _context.Direccions.ToListAsync());
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<ActionResult<string>> DeleteDireccion(int id)
        {
            var DbObjeto = await _context.Direccions.FirstOrDefaultAsync(Ob => Ob.Id == id);
            if (DbObjeto == null)
            {
                return NotFound("no existe :/");
            }

            try
            {
                _context.Direccions.Remove(DbObjeto);
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
