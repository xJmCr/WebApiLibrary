using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<Carrera>>> Get()
        {
            return await context.Carreras.Include(x => x.Usuarios).ToListAsync();
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Post(Carrera carrera)
        {
            context.Add(carrera);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Put(Carrera carrera, int id)
        {
            if (carrera.Id != id)
            {
                return BadRequest("El ID de la carrera no concide con el establecido en la base de datos");
            }

            context.Update(carrera);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await context.Carreras.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            context.Remove(new Carrera()
            {
                Id = id
            });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
