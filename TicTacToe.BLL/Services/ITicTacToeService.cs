
namespace TicTacToe.BLL
{
    public interface ITicTacToeService
    {
        Task<GameModel> AddGameAsync(GameModel game);
        Task DeleteGameById(int id);
        Task<List<GameModel>> GetAllGamesAsync();
        Task<GameModel> GetGameByIdAsync(int id);
        Task<GameModel> UpdateGameAsync(int id, MoveModel move);
    }
}