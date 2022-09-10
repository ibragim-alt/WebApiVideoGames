using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiVideoGames.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiVideoGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        VideoGamesContext db;
        public GenreController(VideoGamesContext context)
        {
            db = context;
        }

        //// GET: api/<GameController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> Get()
        {
            return await db.Genres.ToListAsync();
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(int id)
        {
            Genre genre = await db.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre == null)
                return NotFound();
            return new ObjectResult(genre);
        }

        // POST api/<GameController>
        [HttpPost]
        public async Task<ActionResult<Genre>> Post(Genre genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }
            db.Genres.Add(genre);
            await db.SaveChangesAsync();

            return Ok(genre);
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Genre>> Put(Genre genre)
        {
            if (genre == null)
            {
                return BadRequest();
            }
            if (!db.Genres.Any(x => x.Id == genre.Id))
            {
                return NotFound();
            }

            db.Update(genre);
            await db.SaveChangesAsync();
            return Ok(genre);
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Genre>> Delete(int id)
        {
            Genre genre = db.Genres.FirstOrDefault(x => x.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            db.Genres.Remove(genre);
            await db.SaveChangesAsync();
            return Ok(genre);
        }
    }
}
