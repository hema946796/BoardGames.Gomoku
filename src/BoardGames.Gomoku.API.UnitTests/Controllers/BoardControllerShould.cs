using BoardGames.Gomoku.API.Controllers;
using BoardGames.Gomoku.Business.Interfaces;
using BoardGames.Gomoku.Common;
using BoardGames.Gomoku.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace BoardGames.Gomoku.API.UnitTests.Controllers
{
    public class BoardControllerShould
    {
        private readonly Mock<IBoardService> _boardService;
        private readonly Mock<ILogger<BoardController>> _logger;
        private readonly BoardController _sut;

        public BoardControllerShould()
        {
            _boardService = new Mock<IBoardService>();
            _logger = new Mock<ILogger<BoardController>>();
            _sut = new BoardController(_boardService.Object, _logger.Object);
        }

        [Fact]
        public async void CreateBoard_With_Default_Size_Values_If_Request_DoesNotHave()
        {
            var request = new CreateBoardRequest();
            var board = new Board { GameId = Guid.NewGuid().ToString(), RowSize = 15, ColoumnSize = 15 };
            _boardService.Setup(x => x.CreateBoardAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(board);

            var response = (ObjectResult)await _sut.New(request);

            var newBoard = (Board)response.Value;
            Assert.Equal(Constants.DEFAULT_ROW_SIZE, newBoard.RowSize);
            Assert.Equal(Constants.DEFAULT_COULMN_SIZE, newBoard.ColoumnSize);
        }
    }
}
