using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApiVideoGames.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiVideoGames.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        IGameWithGenresRepository gwgRepository;

        public GameController(IGameWithGenresRepository gwgr)
        {
            gwgRepository = gwgr;
        }

        //// GET: api/<GameController>
        [HttpGet(Name = "GetAllGames")]
        public IEnumerable<GameWithGenresList> Get(string genre1, string genre2, string genre3)
        {
            return gwgRepository.Get(genre1, genre2, genre3);
        }

        // GET api/<GameController>/5
        [HttpGet("{id}", Name = "GetGame")]
        public ActionResult Get(int id)
        {
            GameWithGenresList game = gwgRepository.Get(id);

            if (game == null)
            {
                return NotFound();
            }

            return new ObjectResult(game);
        }

        // POST api/<GameController>
        [HttpPost]
        public ActionResult Post(GameWithGenresList gameWithGenres)
        {
            if (gameWithGenres == null || gameWithGenres.Game == null)
            {
                return BadRequest();
            }
            gwgRepository.Create(gameWithGenres);
            return CreatedAtRoute("GetGame", new { id = gameWithGenres.Game.Id }, gameWithGenres);
        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] GameWithGenresList gameWithGenres)
        {
            if (gameWithGenres == null || gameWithGenres.Game.Id != id)
            {
                return BadRequest();
            }

            var gameFromDB = gwgRepository.Get(id);
            if (gameFromDB == null)
            {
                return NotFound();
            }

            gwgRepository.Update(id, gameWithGenres);
            return RedirectToRoute("GetAllGames");
        }

        // DELETE api/<GameController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var game = gwgRepository.Delete(id);

            if (game == null)
            {
                return BadRequest();
            }

            return new ObjectResult(game);
        }
    }
}
