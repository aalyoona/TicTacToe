﻿namespace TicTacToe.BLL
{
    public class GameModel
    {
        public int Id { get; set; }
        public string PlayerX { get; set; }
        public string PlayerO { get; set; }
        public string CurrentPlayer { get; set; }
        public string Status { get; set; }
        public string Board { get; set; }
    }
}
