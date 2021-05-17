using BoardGames.Gomoku.Business.Interfaces;
using BoardGames.Gomoku.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace BoardGames.Gomoku.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly ILogger<GameController> _logger;

        public GameController(IGameService gameService, ILogger<GameController> logger)
        {
            _gameService = gameService;
            _logger = logger;
        }

        [Route("{gameId}/UpdateMove")]
        [HttpPost]
        public async Task<IActionResult> UpdateMove(string gameId, UpdateMoveRequest request)
        {
            var moveResult = await _gameService.UpdateMoveAsync(gameId, request);
            return Ok(moveResult);
        }
    }
}
