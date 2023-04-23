using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TicTacToe.Attribute;
using TicTacToe.BLL;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ITicTacToeService _service;
        private readonly IMapper _mapper;

        public GamesController(ITicTacToeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all games")]
        [SwaggerResponse(200, "OK", typeof(GameOutputModel))]
        [SwaggerResponse(400, "Bad Request", typeof(ExceptionResponse))]
        public async Task<ActionResult<List<GameOutputModel>>> GetAllGames()
        {
            return Ok(_mapper.Map<List<GameOutputModel>>(await _service.GetAllGamesAsync()));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get game by id")]
        [SwaggerResponse(200, "OK", typeof(GameOutputModel))]
        [SwaggerResponse(400, "Bad Request", typeof(ExceptionResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ExceptionResponse))]
        public async Task<ActionResult<GameOutputModel>> GetGameById(int id)
        {
            return Ok(_mapper.Map<GameOutputModel>(await _service.GetGameByIdAsync(id)));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create new game")]
        [SwaggerResponse(201, "Created", typeof(GameOutputModel))]
        [SwaggerResponse(400, "Bad Request", typeof(ExceptionResponse))]
        public async Task<ActionResult<GameOutputModel>> CreateGame([FromBody] GameCreateInputModel game)
        {
            return StatusCode(StatusCodes.Status201Created, (_mapper.Map<GameOutputModel>(await _service.AddGameAsync(_mapper.Map<GameModel>(game)))));
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Make move")]
        [SwaggerResponse(200, "OK", typeof(GameOutputModel))]
        [SwaggerResponse(400, "Bad Request", typeof(ExceptionResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ExceptionResponse))]
        [SwaggerResponse(409, "Conflict", typeof(ExceptionResponse))]
        public async Task<ActionResult> MakeMove(int id, [FromBody] MoveInputModel move)
        {
            return Ok(_mapper.Map<GameOutputModel>(await _service.UpdateGameAsync(id, _mapper.Map<MoveModel>(move))));
        }

        [HttpDelete("{ id}")]
        [SwaggerOperation(Summary = "Delete game by id")]
        [SwaggerResponse(204, "No Content")]
        [SwaggerResponse(400, "Bad Request", typeof(ExceptionResponse))]
        [SwaggerResponse(404, "Not Found", typeof(ExceptionResponse))]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.DeleteGameById(id);
            return NoContent();
        }
    }
}