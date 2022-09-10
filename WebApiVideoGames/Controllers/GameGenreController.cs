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
    public class GameGenreController : ControllerBase
    {
        VideoGamesContext db;
        public GameGenreController(VideoGamesContext context)
        {
            db = context;
        }

        //// GET: api/<GameController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameGenre>>> Get()
        {
            return await db.GamesGenres.ToListAsync();
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GameGenre>> Get(int id)
        {
            GameGenre gameGenre = await db.GamesGenres.FirstOrDefaultAsync(x => x.Id == id);
            if (gameGenre == null)
                return NotFound();
            return new ObjectResult(gameGenre);
        }

        // POST api/<GameController>
        [HttpPost]
        public async Task<ActionResult<GameGenre>> Post(GameGenre gameGenre)
        {
            if (gameGenre == null)
            {
                return BadRequest();
            }
            db.GamesGenres.Add(gameGenre);
            await db.SaveChangesAsync();

            return Ok(gameGenre);
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<GameGenre>> Put(GameGenre gameGenre)
        {
            if (gameGenre == null)
            {
                return BadRequest();
            }
            if (!db.GamesGenres.Any(x => x.Id == gameGenre.Id))
            {
                return NotFound();
            }

            db.Update(gameGenre);
            await db.SaveChangesAsync();
            return Ok(gameGenre);
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GameGenre>> Delete(int id)
        {
            GameGenre gameGenre = db.GamesGenres.FirstOrDefault(x => x.Id == id);
            if (gameGenre == null)
            {
                return NotFound();
            }
            db.GamesGenres.Remove(gameGenre);
            await db.SaveChangesAsync();
            return Ok(gameGenre);
        }
    }
}
