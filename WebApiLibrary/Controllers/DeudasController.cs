using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiLibrary.Entidades;

namespace WebApiLibrary.Controllers
{
    [ApiController]
    [Route("api/deudas")]
    public class DeudasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DeudasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Deuda>> Get(int id)
        {
            return await context.Deudas.Include(x => x.Prestamo).Include(x => x.Prestamo.Usuario).Include(x => x.Prestamo.Libro).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Post(Deuda deuda)
        {
            context.Add(deuda);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Put(Deuda deuda, int id)
        {
            if (deuda.Id != id)
            {
                return BadRequest("El ID de la deuda no concide con el establecido en la base de datos");
            }

            context.Update(deuda);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Deudas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new Deuda()
            {
                Id = id
            });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
