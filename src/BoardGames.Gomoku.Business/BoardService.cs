using BoardGames.Gomoku.Business.Interfaces;
using BoardGames.Gomoku.Entities;
using BoardGames.Gomoku.Models;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BoardGames.Gomoku.Business
{
    public class BoardService : IBoardService
    {
        private readonly IRepository<GameDto> _boardRepository;
        private readonly ILogger<BoardService> _logger;

        public BoardService(IRepository<GameDto> boardRepository, ILogger<BoardService> logger)
        {
            _boardRepository = boardRepository;
            _logger = logger;
        }

        public async Task<Board> CreateBoardAsync(string gameName, int rowSize, int columnSize)
        {
            var gameData = new int[rowSize, columnSize];
            var game = new GameDto { Id = Guid.NewGuid().ToString(), GameName = gameName, Data = gameData, RowSize = rowSize, ColumnSize = columnSize };
            game = await _boardRepository.CreateAsync(game);
            return new Board { GameId = game.Id, RowSize = game.RowSize, ColoumnSize = game.ColumnSize };
        }
    }
}
