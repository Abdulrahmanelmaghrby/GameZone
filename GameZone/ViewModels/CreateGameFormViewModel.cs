

using GameZone.Attribute;
using GameZone.Settings;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel:GameFormViewModel
    {
        

        //validate file extantion and size
        [AllowedExtentions(FileSettings.AllowedExtentions),
            MaxSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
