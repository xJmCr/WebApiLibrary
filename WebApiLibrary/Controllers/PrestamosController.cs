using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Entidades;

namespace WebApiLibrary.Controllers
{
    [ApiController]
    [Route("api/prestamos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PrestamosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public PrestamosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Prestamo>>> Get()
        {
            return await context.Prestamos.Include(x => x.Usuario).Include(x => x.Libro).Include(x => x.Deudas).ToListAsync();
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Post(Prestamo prestamo)
        {
            context.Add(prestamo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Put(Prestamo prestamo, int id)
        {
            if (prestamo.Id != id)
            {
                return BadRequest("El ID del prestamo no concide con el establecido en la base de datos");
            }

            context.Update(prestamo);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Prestamos.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new Prestamo()
            {
                Id = id
            });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
