namespace TicTacToe.BLL
{
    public class GameOverException : Exception
    {
        public GameOverException(string message) : base(message) { }
    }
}
