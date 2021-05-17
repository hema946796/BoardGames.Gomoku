using BoardGames.Gomoku.Business.Interfaces;
using BoardGames.Gomoku.Common;
using BoardGames.Gomoku.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BoardGames.Gomoku.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private readonly IBoardService _boardService;
        private readonly ILogger<BoardController> _logger;

        public BoardController(IBoardService boardService, ILogger<BoardController> logger)
        {
            _boardService = boardService;
            _logger = logger;
        }

        [Route("new")]
        [HttpPost]
        public async Task<IActionResult> New(CreateBoardRequest request)
        {
            _logger.LogInformation($"Create board request - {JsonConvert.SerializeObject(request)}");
            int rowSize = request.RowSize == 0 ? Constants.DEFAULT_ROW_SIZE : request.RowSize;
            int columnSize = request.ColumnSize == 0 ? Constants.DEFAULT_COULMN_SIZE : request.ColumnSize;
            var board = await _boardService.CreateBoardAsync(request.GameName, rowSize, columnSize);
            return Ok(board);
        }

    }
}
