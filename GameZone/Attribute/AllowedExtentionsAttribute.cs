using GameZone.Attribute;
using Microsoft.AspNetCore.Components.Forms;

namespace GameZone.Attribute
{
    public class AllowedExtentionsAttribute : ValidationAttribute
    {
        private readonly string _allowedextentions;
        public AllowedExtentionsAttribute(string allowedextentions)
        {
            _allowedextentions = allowedextentions;
        }

        protected override ValidationResult? IsValid
            (object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file is not null)
            {
                var extention=Path.GetExtension(file.FileName);

                var isAllowed=_allowedextentions.Split(',').Contains(extention,StringComparer.OrdinalIgnoreCase);
                if (!isAllowed)
                {
                    return new ValidationResult($"Only {_allowedextentions} is allowed !" );
                }
                
            }
            return ValidationResult.Success;
        }
    }
}




































//private readonly string _allowedextentions;
//public AllowedExtentionsAttribute(string allowedExtention)
//{
//    _allowedextentions = allowedExtention;
//}

//protected override ValidationResult? IsValid
//    (object? value, ValidationContext validationContext)
//{
//    var file = value as IFormFile;

//    if (file != null)
//    {
//        var extentions = Path.GetExtension(file.FileName);

//        var isAllowed = _allowedextentions.Split(',').Contains(extentions, StringComparer.OrdinalIgnoreCase);
//        if (!isAllowed)
//        {
//            return new ValidationResult($"only {_allowedextentions}are allowed !");
//        }
//    }
//    return ValidationResult.Success;
//}