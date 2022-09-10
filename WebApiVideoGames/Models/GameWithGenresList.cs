using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVideoGames.Models
{
    //Класс-представление для упрощенного просмотра и редактирования содержимого БД
    public class GameWithGenresList
    {
        public Game Game { get; set; }
        public List<string> Genres { get; set; }
        public GameWithGenresList()
        {
            Genres = new List<string>();
        }
    }
}
