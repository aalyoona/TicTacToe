using AutoMapper;
using TicTacToe.BLL;
using TicTacToe.Models;

namespace TicTacToe
{
    public class AutoMapperAPI : Profile
    {
        public AutoMapperAPI()
        {
            CreateMap<GameModel, GameOutputModel>();
            CreateMap<GameCreateInputModel, GameModel>();
            CreateMap<MoveInputModel, MoveModel>();
        }
    }
}