namespace GameZone.Settings
{
    public static class FileSettings
    {//this class is for some settings in our app avoid being hard coded(it means they are cooded manually as a string )
        public const string ImagesPath = "/assets/images/games";
        public const string AllowedExtentions = ".jpg,.png,.jpeg";
        public const int  MaxFileSize= 1  ;
        public const int MaxFileSizeInBytes = MaxFileSize * 1024 * 1024;

    }
}
