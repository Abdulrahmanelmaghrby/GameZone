using GameZone.Models;

namespace GameZone.Services
{
    public interface IGamesService 
    {
        IEnumerable<Game> GetAllGames();

        Game? GetGameById(int id);
        public Task CreateOnDB(CreateGameFormViewModel model);
        public Task<Game?> Update(EditGameFormViewModel model);

        public bool Delete(int id);


    }
}
