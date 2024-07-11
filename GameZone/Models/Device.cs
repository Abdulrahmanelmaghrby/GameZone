namespace GameZone.Models
{
    public class Device :BaseEntity
    {
       
        [MaxLength(500)]
        public string Icon { get; set; } = string.Empty;
    }
}
