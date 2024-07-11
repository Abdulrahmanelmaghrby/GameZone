namespace GameZone.ViewModels
{
    public class GameFormViewModel //which contains all the coman proberities bettwen creat and edit view models
    {
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Catiegory")]
        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        public List<int> SelectedDevices { get; set; } = default!;/*new List<int>();we have to make it = defult; to get her service side validation*/

        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
    }
}
