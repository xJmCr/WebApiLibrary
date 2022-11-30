using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Entidades;

namespace WebApiLibrary.Controllers
{
    [ApiController]
    [Route("api/carreras")]
    public class CarrerasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public CarrerasController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Carrera>>> Get()
        {
            return await context.Carreras.Include(x => x.Usuarios).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult> Post(Carrera carrera)
        {
            context.Add(carrera);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Usuario usuario, int id)
        {
            if (usuario.Id != id)
            {
                return BadRequest("El ID del usuario no concide con el establecido en la base de datos");
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
        }
    }
}
