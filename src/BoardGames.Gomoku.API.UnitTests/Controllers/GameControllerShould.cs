using BoardGames.Gomoku.API.Controllers;
using BoardGames.Gomoku.Business.Interfaces;
using BoardGames.Gomoku.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace BoardGames.Gomoku.API.UnitTests.Controllers
{
    public class GameControllerShould
    {
        private readonly Mock<IGameService> _gameService;
        private readonly Mock<ILogger<GameController>> _logger;
        private readonly GameController _sut;

        public GameControllerShould()
        {
            _gameService = new Mock<IGameService>();
            _logger = new Mock<ILogger<GameController>>();
            _sut = new GameController(_gameService.Object, _logger.Object);
        }

        [Fact]
        public async void Return_Success_On_Valid_Move()
        {
            var gameId = Guid.NewGuid().ToString();
            var request = new UpdateMoveRequest { PlayerId = 1, columnPosition = 5, rowPosition = 5 };
            var moveResult = new MoveResult { GameId = gameId, IsDraw = false, IsGameOver = true, MoveNotAllowed = false };
            _gameService.Setup(x => x.UpdateMoveAsync(It.IsAny<string>(), It.IsAny<UpdateMoveRequest>())).ReturnsAsync(moveResult);

            var response = (ObjectResult)await _sut.UpdateMove(gameId, request);

            var resule = (MoveResult)response.Value;
            Assert.Equal(gameId, resule.GameId);
        }
    }
}
