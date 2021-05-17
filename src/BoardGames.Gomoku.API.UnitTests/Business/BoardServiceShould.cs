using BoardGames.Gomoku.Business;
using BoardGames.Gomoku.Entities;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace BoardGames.Gomoku.API.UnitTests.Business
{
    public class BoardServiceShould
    {
        private readonly Mock<IRepository<GameDto>> _boardRepository;
        private readonly Mock<ILogger<BoardService>> _logger;
        private readonly BoardService _sut;

        public BoardServiceShould()
        {
            _boardRepository = new Mock<IRepository<GameDto>>();
            _logger = new Mock<ILogger<BoardService>>();
            _sut = new BoardService(_boardRepository.Object, _logger.Object);
        }

        [Fact]
        public async void Return_Board_Successfully()
        {
            var game = new GameDto { Id = Guid.NewGuid().ToString(), ColumnSize = 10, RowSize = 10 };
            _boardRepository.Setup(x => x.CreateAsync(It.IsAny<GameDto>(), It.IsAny<CancellationToken>())).ReturnsAsync(game);

            var board = await _sut.CreateBoardAsync("game1", 10, 10);

            Assert.Equal(game.RowSize, board.RowSize);
            Assert.Equal(game.ColumnSize, board.ColoumnSize);
        }
    }
}
