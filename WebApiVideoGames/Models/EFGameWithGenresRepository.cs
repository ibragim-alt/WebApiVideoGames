using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiVideoGames.Models
{
    public class EFGameWithGenresRepository : IGameWithGenresRepository
    {
        private VideoGamesContext db;
        List<GameWithGenresList> gameWithGenresList;
        Dictionary<int, List<int>> gamesGenresDict;

        public EFGameWithGenresRepository(VideoGamesContext context)
        {
            db = context;

            if (!db.Genres.Any())
            {
                var arcades = new Genre { Name = "Аркады" };
                var sims = new Genre { Name = "Симуляторы" };
                var strategs = new Genre { Name = "Стратегии" };
                db.Genres.AddRange(arcades, sims, strategs);
                db.SaveChanges();
            }

            if (!db.Studios.Any())
            {
                var epicGames = new Studio { Name = "Epic Games" };
                var nvidia = new Studio { Name = "Nvidia" };
                var wargamingNet = new Studio { Name = "Wargaming.net" };
                var ubisoft = new Studio { Name = "Ubisoft" };
                db.Studios.AddRange(epicGames, nvidia, wargamingNet, ubisoft);
                db.SaveChanges();
            }

            if (!db.Games.Any())
            {
                db.Games.Add(new Game { Name = "Game1", StudioId = 1 });
                db.Games.Add(new Game { Name = "Game2", StudioId = 2 });
                db.Games.Add(new Game { Name = "Game3", StudioId = 3 });
                db.Games.Add(new Game { Name = "Game4", StudioId = 4 });
                db.SaveChanges();
            }

            if (!db.GamesGenres.Any())
            {
                db.GamesGenres.Add(new GameGenre { GameId = 1, GenreId = 1 });
                db.GamesGenres.Add(new GameGenre { GameId = 1, GenreId = 2 });
                db.GamesGenres.Add(new GameGenre { GameId = 2, GenreId = 2 });
                db.GamesGenres.Add(new GameGenre { GameId = 2, GenreId = 3 });
                db.GamesGenres.Add(new GameGenre { GameId = 3, GenreId = 1 });
                db.GamesGenres.Add(new GameGenre { GameId = 3, GenreId = 3 });
                db.GamesGenres.Add(new GameGenre { GameId = 4, GenreId = 1 });
                db.SaveChanges();
            }

            //Сделаем предобработку. Это фактически то же самое, что представление в БД
            //в котором отображается экземпляр игры и ее список наименований жанров
            //Создаем словарь IDшников "Игра - Жанры" на основе таблицы из БД GamesGenres
            gamesGenresDict = MakeADictionaryOfGamesGenres();
            //Создаем список класса, объединяющего игры с жанрами
            gameWithGenresList = MakeAListOfGames();
        }

        public void Create(GameWithGenresList game)
        {
            db.Games.Add(game.Game);
            db.SaveChanges();

            if (game.Genres != null)
            {
                AddValues(game);
            }
        }

        public GameWithGenresList Delete(int id)
        {
            Game game = db.Games.FirstOrDefault(x => x.Id == id);
            var gameWithGenres = gameWithGenresList.Find(p => p.Game.Id == id);
            if (game != null)
            {
                //Сначала удалим из таблицы GamesGenres с помощью доп метода
                //Можно сделать просто триггер в БД, но пусть будет так
                DeleteValues(id);
                //Затем удалим из таблицы
                db.Games.Remove(game);
                db.SaveChanges();
            }
            return gameWithGenres;
        }

        public IEnumerable<GameWithGenresList> Get(string genre1, string genre2, string genre3)
        {
            if (genre1 != null || genre2 != null || genre3 != null)
            {
                var genres = new List<string>();
                if (genre1 != null) genres.Add(genre1);
                if (genre2 != null) genres.Add(genre2);
                if (genre3 != null) genres.Add(genre3);
                return MakeAListOfGames(genres);
            }
            return gameWithGenresList;
        }

        public GameWithGenresList Get(int id)
        {
            var game = gameWithGenresList.FirstOrDefault(x => x.Game.Id == id);
            return game;
        }

        public void Update(int id, GameWithGenresList game)
        {
            var g = db.Games.FirstOrDefault(p => p.Id == id);
            g.Name = game.Game.Name;
            g.StudioId = game.Game.StudioId;
            db.Update(g);
            db.SaveChanges();
            if (game.Genres != null)
            {
                game.Game.Id = id;
                UpdateValues(game);
            }
        }

        //Доп метод добавления строк Игра-Жанр в таблицу GamesGenres
        private void AddValues(GameWithGenresList game)
        {
            var genres = game.Genres;
            foreach (var genreName in genres)
            {
                var genre = db.Genres.FirstOrDefault(p => p.Name == genreName);
                //Если указанного жанра нет в таблице жанров, то он не добавится
                if (genre != null)
                    db.GamesGenres.Add(new GameGenre { GameId = game.Game.Id, GenreId = genre.Id });
            }

            db.SaveChanges();
        }

        //Доп метод редактирования связок Игра-Жанр в случае редактирования вместе с указанием НОВЫХ жанров (т.е. они заменяются)
        private void UpdateValues(GameWithGenresList gameWithGenres)
        {
            DeleteValues(gameWithGenres.Game.Id);
            AddValues(gameWithGenres);
        }

        //Доп метод удаления связок Игра-Жанр из таблицы GamesGenres
        private void DeleteValues(int gameId)
        {
            var gameGenre = db.GamesGenres.Where(p => p.GameId == gameId).ToList();
            db.GamesGenres.RemoveRange(gameGenre);
            db.SaveChanges();
        }

        //Создаем словарь Игра-Жанры на основе таблицы из БД
        private Dictionary<int, List<int>> MakeADictionaryOfGamesGenres()
        {
            IQueryable<GameGenre> gamesGenres = db.GamesGenres;
            var dictionaryGamesGenres = new Dictionary<int, List<int>>();
            foreach (var game in gamesGenres)
            {
                if (!dictionaryGamesGenres.ContainsKey(game.GameId))
                    dictionaryGamesGenres[game.GameId] = new List<int>();
                dictionaryGamesGenres[game.GameId].Add(game.GenreId);
            }
            return dictionaryGamesGenres;
        }

        //Создаем список на основе модели GameWithGenresList
        private List<GameWithGenresList> MakeAListOfGames()
        {
            var gwgList = new List<GameWithGenresList>();
            //Пробегаемся по всем пунктам словаря, параллельно создавая словарь наименований жанров на основе списка ID жанров
            foreach (var gameGenres in gamesGenresDict)
            {
                var game = db.Games.FirstOrDefault(p => p.Id == gameGenres.Key);
                var genreNames = new List<string>();
                foreach (var genre in gameGenres.Value)
                {
                    genreNames.Add(db.Genres.FirstOrDefault(p => p.Id == genre).Name);
                }
                gwgList.Add(new GameWithGenresList
                {
                    Game = game,
                    Genres = genreNames
                });
            }
            return gwgList;
        }

        //Создаем лист игр на основе переданных жанров
        private List<GameWithGenresList> MakeAListOfGames(List<string> genres)
        {
            //Получаем таблицу жанров из БД и создаем лист ID жанров
            IQueryable<Genre> genresDB = db.Genres;
            var genresList = new List<int>();
            foreach (var genre in genres)
            {
                var g = genresDB.FirstOrDefault(p => p.Name == genre);
                if(g != null)
                    genresList.Add(g.Id);
            }
            //Если в запросе передали все жанры, которых нет в БД, то список будет пуст, выводим все игры
            if (genresList.Count == 0) return gameWithGenresList;
            //Список соответствующих запросу игр
            var gamesList = new List<GameWithGenresList>();
            //Если в списке жанров игры нет запрошенного жанра, то не добавляем
            foreach (var game in gamesGenresDict)
            {
                var trueGame = true;
                foreach (var genre in genresList)
                {
                    if (!game.Value.Contains(genre))
                    {
                        trueGame = false;
                        break;
                    }
                }
                if(trueGame)
                    gamesList.Add(gameWithGenresList.FirstOrDefault(p => p.Game.Id == game.Key));
            }

            return gamesList;
        }
    }
}
