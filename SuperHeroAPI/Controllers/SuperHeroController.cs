using SuperHeroAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet] // get
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await this.context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")] // get with id parameter
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await this.context.SuperHeroes.FindAsync(id);
            if(hero == null)
            {
                return BadRequest("Hero not found.");
            }
            else
            {
                return Ok(hero);
            }
        }

        [HttpPost] // post
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            this.context.SuperHeroes.Add(hero);
            await this.context.SaveChangesAsync(); // save changes

            return Ok(await this.context.SuperHeroes.ToListAsync());
        }

        [HttpPut] // put
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var dbHero = await this.context.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found.");
            }

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await this.context.SaveChangesAsync(); // save changes

            return Ok(await this.context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")] // delete with id parameter
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var dbHero = await this.context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found.");
            }

            this.context.SuperHeroes.Remove(dbHero); // delete hero
            await this.context.SaveChangesAsync(); // save changes

            return Ok(await this.context.SuperHeroes.ToListAsync());
        }
    }
}
