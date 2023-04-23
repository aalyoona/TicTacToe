using AutoMapper;
using Newtonsoft.Json;
using TicTacToe.DL;

namespace TicTacToe.BLL
{
    public class TicTacToeService : ITicTacToeService
    {
        private readonly ITicTacToeData _data;
        private readonly IMapper _mapper;

        public TicTacToeService(ITicTacToeData data, IMapper mapper)
        {
            _mapper = mapper;
            _data = data;
        }

        public async Task<List<GameModel>> GetAllGamesAsync() => _mapper.Map<List<GameModel>>(await _data.Games.GetAllAsync());

        public async Task<GameModel> GetGameByIdAsync(int id)
        {
            GameModel game = _mapper.Map<GameModel>(await _data.Games.GetByIdAsync(id));
            if (game is null)
            {
                throw new EntityNotFoundException($"Game with {id} was not found");
            }
            return game;
        }

        public async Task DeleteGameById(int id)
        {
            var game = await _data.Games.GetByIdAsync(id);
            if (game is null)
            {
                throw new EntityNotFoundException($"Game with {id} was not found");
            }

            await _data.Games.DeleteAsync(id);
        }

        public async Task<GameModel> AddGameAsync(GameModel game)
        {
            game.CurrentPlayer = CurrentPlayer.X.ToString();
            game.Status = GameState.InProgress.ToString();
            game.Board = "[[null,null,null],[null,null,null],[null,null,null]]";

            GameEntity newGame = await _data.Games.AddAsync(_mapper.Map<GameEntity>(game));
            return _mapper.Map<GameModel>(newGame);
        }

        public async Task<GameModel> UpdateGameAsync(int id, MoveModel moveModel)
        {
            var game = await _data.Games.GetByIdAsync(id);
            if (game is null)
            {
                throw new EntityNotFoundException($"Game with {id} was not found");
            }

            if (game.Status != GameState.WonByO.ToString() && game.Status != GameState.WonByX.ToString())
            {
                var board = JsonConvert.DeserializeObject<string[][]>(game.Board);
                string player = game.CurrentPlayer;

                int row = moveModel.Xaxis;
                int col = moveModel.Yaxis;

                if (board[row][col] != CurrentPlayer.X.ToString() && board[row][col] != CurrentPlayer.O.ToString())
                {
                    board[row][col] = player;
                }
                else
                {
                    throw new CellOccupiedException("Cell already occupied");
                }

                if (CheckWin(board, player))
                {
                    if (player == CurrentPlayer.X.ToString())
                    {
                        game.Status = GameState.WonByX.ToString();
                    }
                    else if (player == CurrentPlayer.O.ToString())
                    {
                        game.Status = GameState.WonByO.ToString();
                    }
                }
                else if (CheckDraw(board))
                {
                    game.Status = GameState.Draw.ToString();
                }
                else
                {
                    game.CurrentPlayer = player == CurrentPlayer.X.ToString() ? CurrentPlayer.O.ToString() : CurrentPlayer.X.ToString();
                }

                game.Board = JsonConvert.SerializeObject(board);

                var newGame = await _data.Games.UpdateAsync(game, id);
                return _mapper.Map<GameModel>(newGame);
            }
            else
            {
                throw new GameOverException($"Game over. {game.Status}");
            }
        }

        private bool CheckDraw(string[][] board)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i][j] == null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool CheckWin(string[][] board, string player)
        {
            // Check rows
            for (int i = 0; i < 3; i++)
            {
                if (board[i][0] == player && board[i][1] == player && board[i][2] == player)
                {
                    return true;
                }
            }

            // Check columns
            for (int j = 0; j < 3; j++)
            {
                if (board[0][j] == player && board[1][j] == player && board[2][j] == player)
                {
                    return true;
                }
            }

            // Check diagonals
            if (board[0][0] == player && board[1][1] == player && board[2][2] == player)
            {
                return true;
            }

            if (board[0][2] == player && board[1][1] == player && board[2][0] == player)
            {
                return true;
            }

            return false;
        }
    }
}