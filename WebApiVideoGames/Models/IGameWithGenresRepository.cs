using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVideoGames.Models
{
    public interface IGameWithGenresRepository
    {
        IEnumerable<GameWithGenresList> Get(string genre1, string genre2, string genre3);
        GameWithGenresList Get(int id);
        void Create(GameWithGenresList game);
        void Update(int id, GameWithGenresList game);
        GameWithGenresList Delete(int id);
    }
}
