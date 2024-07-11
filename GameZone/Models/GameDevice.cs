﻿namespace GameZone.Models
{
    public class GameDevice
    {
        public Game Game { get; set; } = default!;
        public int GameId { get; set; }
       
        public Device Device { get; set; }=default!;
        public int DeviceId { get; set; }
        //we will use fluent api to make a composet Promary key Game Id+Device ID
    }
}
