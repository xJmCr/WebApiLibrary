using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Entidades;

namespace WebApiLibrary.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public UsuariosController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            return await context.Usuarios.Include(x => x.Carrera).FirstOrDefaultAsync(x => x.Id == id);
        }


        [HttpPost]
        public async Task<ActionResult> Post(Usuario usuario)
        {
            var existeCarrera = await context.Carreras.AnyAsync(x => x.Id == usuario.CarreraId);

            if (!existeCarrera)
            {
                return BadRequest($"No existe la carrera de ID: {usuario.CarreraId}");
            }

            context.Add(usuario);
            await context.SaveChangesAsync();
            return Ok();
        }

        /*[HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Usuario usuario, int id)
        {
            if (usuario.Id != id)
            {
                return BadRequest("El ID del automovil no concide con el establecido en la base de datos");
            }

            context.Update(usuario);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Usuarios.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new Usuario()
            {
                Id = id
            });
            await context.SaveChangesAsync();
            return Ok();
        }*/
    }
}
