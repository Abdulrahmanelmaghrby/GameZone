﻿namespace GameZone.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;
    }
}
