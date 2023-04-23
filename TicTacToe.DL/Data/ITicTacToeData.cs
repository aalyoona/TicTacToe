namespace TicTacToe.DL
{
    public interface ITicTacToeData
    {
        IGenericRepository<GameEntity> Games { get; }
    }
}