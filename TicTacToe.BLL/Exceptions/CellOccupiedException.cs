namespace TicTacToe.BLL
{
    public class CellOccupiedException : Exception
    {
        public CellOccupiedException(string message) : base(message) { }
    }
}
