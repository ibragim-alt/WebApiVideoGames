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
    public class StudioController : ControllerBase
    {
        VideoGamesContext db;
        List<Game> games;
        public StudioController(VideoGamesContext context)
        {
            db = context;
            games = db.Games.ToList();//нужно для того, чтобы отображался список игр студии разработчика
        }

        //// GET: api/<GameController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Studio>>> Get()
        {
            return await db.Studios.ToListAsync();
        }

        // GET api/<GameController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Studio>> Get(int id)
        {
            Studio studio = await db.Studios.FirstOrDefaultAsync(x => x.Id == id);
            if (studio == null)
                return NotFound();
            return new ObjectResult(studio);
        }

        // POST api/<GameController>
        [HttpPost]
        public async Task<ActionResult<Studio>> Post(Studio studio)
        {
            if (studio == null)
            {
                return BadRequest();
            }
            db.Studios.Add(studio);
            await db.SaveChangesAsync();

            return Ok(studio);
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Studio>> Put(Studio studio)
        {
            if (studio == null)
            {
                return BadRequest();
            }
            if (!db.Studios.Any(x => x.Id == studio.Id))
            {
                return NotFound();
            }

            db.Update(studio);
            await db.SaveChangesAsync();
            return Ok(studio);
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Studio>> Delete(int id)
        {
            Studio studio = db.Studios.FirstOrDefault(x => x.Id == id);
            if (studio == null)
            {
                return NotFound();
            }
            db.Studios.Remove(studio);
            await db.SaveChangesAsync();
            return Ok(studio);
        }
    }
}
