using GameZone.Services;
using GameZone.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        
        private readonly ICategoriesService _categoriesService;
        private readonly IDeviceService _deviceService;
        private readonly IGamesService _gamesService;

        public GamesController(ICategoriesService categoriesService, IDeviceService deviceService, IGamesService gamesService)
        {

            _categoriesService = categoriesService;
            _deviceService = deviceService;
            _gamesService = gamesService;
        }

        public IActionResult Index()
        {
            var game =_gamesService.GetAllGames();
            return View(game);
        }

        public IActionResult Details(int id)
        { 
            var game= _gamesService.GetGameById(id);

            if (game == null)//we must allways cheak if the game is valid or not
                return NotFound();

            return View(game); 
        }
        
        [HttpGet] // by default it is get 
        public IActionResult Create()
        {
            //projection =when i select the categories i will change its type by one
            // step using (.Select() function) from  Icollection list to seclected list 
            //     as it is assigned in CreateGameFormViewModelv

            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesService.GetCategories(),
                Devices = _deviceService.GetDevices(),
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//for securty in dot net
        public async Task <IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories= _categoriesService.GetCategories();
                model.Devices=_deviceService.GetDevices();
                return View(model);
            }
           await _gamesService.CreateOnDB(model);
            //save game to DB
            //save cover toto server 
            return RedirectToAction( nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var game = _gamesService.GetGameById(id);

            if (game == null)//we must allways cheak if the game is valid or not
                return NotFound();
            EditGameFormViewModel viewModel = new()
            {
                Id = game.Id,
                Name = game.Name,
                Description = game.Description,
                Categories = _categoriesService.GetCategories(),
                Devices = _deviceService.GetDevices(),
                SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                CurrentCover = game.Cover,
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesService.GetCategories();
                model.Devices = _deviceService.GetDevices();
                return View(model);
            }
          var game=   await _gamesService.Update(model);

            if (game == null)
                return BadRequest();
            //save game to DB
            //save cover toto server 
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            
            var isDeleted = _gamesService.Delete(id);


            return isDeleted ? Ok() : BadRequest();
        }

    }

  

}
//var game = _gamesService.GetGameById(id);

//if (game == null)
//    return NotFound();

//EditGameFormViewModel viewModel = new()
//{
//    Id = game.Id,
//    Name = game.Name,
//    Description = game.Description,
//    CategoryId = game.CategoryId,
//    Categories = _categoriesService.GetCategories(),
//    Devices = _deviceService.GetDevices(),
//    //Now :we need to fill the seleced devices(edit view) but in the create it is not selected before
//    SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList(),
//    CurrentCover = game.Cover,

//};

//return View(viewModel);