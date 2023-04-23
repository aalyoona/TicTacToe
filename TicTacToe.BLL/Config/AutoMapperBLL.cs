using AutoMapper;
using TicTacToe.DL;

namespace TicTacToe.BLL
{
    public class AutoMapperBLL : Profile
    {
        public AutoMapperBLL()
        {
            CreateMap<GameEntity, GameModel>().ReverseMap();
        }
    }
}