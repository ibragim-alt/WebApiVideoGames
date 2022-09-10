using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVideoGames.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StudioId { get; set; }
    }
}
