using GameZone.Attribute;

namespace GameZone.ViewModels
{
    public class EditGameFormViewModel:GameFormViewModel
    {
        public int Id { get; set; }//dont forget setter and getter
        //nullable because we dont need the user to send it again we just (developer) send to it the old cover
        public string? CurrentCover { get; set; }

        //validate file extantion and size
        [AllowedExtentions(FileSettings.AllowedExtentions),
            MaxSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;//cover is mendatory in createForm viewmodel but heare is nullable

    }
}
