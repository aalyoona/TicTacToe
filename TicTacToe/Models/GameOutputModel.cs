namespace TicTacToe.Models
{
    public class GameOutputModel
    {
        public int Id { get; set; }
        public string PlayerXName { get; set; }
        public string PlayerOName { get; set; }
        public string CurrentPlayer { get; set; }
        public string Status { get; set; }
        public string Board { get; set; }
    }
}
