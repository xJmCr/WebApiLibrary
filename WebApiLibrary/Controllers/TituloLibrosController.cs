using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Entidades;

namespace WebApiLibrary.Controllers
{
    [ApiController]
    [Route("api/TituloLibro")]
    public class TituloLibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public TituloLibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<TituloLibro>>> Get()
        {
            return await context.TituloLibros.Include(x => x.libros).ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult> Post(TituloLibro tituloLibro)
        {
            context.Add(tituloLibro);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(TituloLibro tituloLibro, int id)
        {
            if (tituloLibro.Id != id)
            {
                return BadRequest("El ID del libro no concide con el establecido en la base de datos");
            }

            context.Update(tituloLibro);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.TituloLibros.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new TituloLibro()
            {
                Id = id
            });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
