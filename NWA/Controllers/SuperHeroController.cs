using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NWA.Data;

namespace NWA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
      
        private readonly DataContext _context;

        //constructor
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }


        //GET
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
         
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        //GET single element
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int? id)
        {


            var hero = _context.SuperHeroes.FindAsync(id);
            if(hero == null)
            {
                return BadRequest("hero no found");
            }
            return Ok(hero);
        }

        //POST
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        //PUT
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero updatedHero)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(updatedHero.Id);
            if (dbHero == null)
            {
                return BadRequest("hero no found");
            }

            dbHero.Name = updatedHero.Name;
            dbHero.FirstName = updatedHero.FirstName;
            dbHero.LastName = updatedHero.LastName;
            dbHero.Place = updatedHero.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        //DELETE
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int? id)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(id);
            if (dbHero == null)
            {
                return BadRequest("hero no found");
            }
            _context.SuperHeroes.Remove(dbHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
