using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Entidades;

namespace WebApiLibrary.Controllers
{
    [ApiController]
    [Route("api/libros")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LibrosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public LibrosController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Libro>> Get(int id)
        {
            return await context.Libros.Include(x => x.TituloLibro).FirstOrDefaultAsync(x => x.Id == id);
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Post(Libro libro)
        {
            var existeLibro = await context.TituloLibros.AnyAsync(x => x.Id == libro.TituloLibroId);

            if (!existeLibro)
            {
                return BadRequest($"No existe el libro de ID: {libro.TituloLibroId}");
            }

            context.Add(libro);
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
