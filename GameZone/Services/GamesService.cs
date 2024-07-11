using GameZone.Models;
using GameZone.Settings;

namespace GameZone.Services
{
    public class GamesService : IGamesService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private string images;

       

        public GamesService(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            images = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
            _context = context;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games
                .Include(g=>g.Category)
                .Include(g=>g.Devices)
                .ThenInclude(d=>d.Device)
                .AsNoTracking ()
                .ToList();
        }
        public Game? GetGameById(int id)
        {
            return _context.Games
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .AsNoTracking()
                .SingleOrDefault(g=>g.Id==id);
        }


        public async Task CreateOnDB(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cover = coverName,
                Devices = model.SelectedDevices.Select(d => new GameDevice { DeviceId = d }).ToList()
            };
         //   _context.Games.Add(game); old nethod asp now is more smat that can understand from the obj name
             _context.Add(game);
             _context.SaveChanges();
           
        }

        public async Task<Game?> Update(EditGameFormViewModel model)
        {
            var game = _context.Games
                .Include(g=>g.Devices)
                .Include(g => g.Category)
                .SingleOrDefault(g=>g.Id==model.Id);

            if (game == null)
                return null;

            var hasNewCover = model.Cover is not null;
            var oldCover = game.Cover;

            game.Name = model.Name;
            game.Description = model.Description;
            game.CategoryId = model.CategoryId;
            game.Devices=model.SelectedDevices.Select(d => new GameDevice { DeviceId=d }).ToList();

            if(hasNewCover)
                game.Cover = await SaveCover(model.Cover!);

            var effectedRows= _context.SaveChanges();

            if(effectedRows>0)
            {
                if(hasNewCover)
                { //if we updated the cover by a new one we have to delete the old one
                    var cover = Path.Combine(images, oldCover); 
                    File.Delete(cover);
                }

                return game;
                
            }
            else //if the update faild we have to delete the cover we have add to server
            {
                var cover=Path.Combine(images,game.Cover);
                File.Delete(cover);
                return null;
            }
            

        }
        public bool Delete(int id)
        { 
            var isDeleted =false;

            var game = _context.Games.Find(id);

            if (game is null)
                return isDeleted;

            _context.Remove(game);

            var effectedRows=_context.SaveChanges();

            if (effectedRows>0)
            {
                isDeleted = true;
                var cover = Path.Combine(images, game.Cover);
                File.Delete(cover);
                
            }
            return isDeleted;
        }

        private async Task<string> SaveCover(IFormFile file)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var path = Path.Combine(images, coverName);

            using var stream = File.Create(path);
            await file.CopyToAsync(stream);

            return coverName;
        }

    }
}
