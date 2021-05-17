using BoardGames.Gomoku.Business;
using BoardGames.Gomoku.Common;
using BoardGames.Gomoku.Entities;
using BoardGames.Gomoku.Models;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using Xunit;

namespace BoardGames.Gomoku.API.UnitTests.Business
{
    public class GameServiceShould
    {
        private readonly Mock<IRepository<GameDto>> _gameRepository;
        private readonly Mock<ILogger<GameService>> _logger;
        private readonly GameService _sut;

        public GameServiceShould()
        {
            _gameRepository = new Mock<IRepository<GameDto>>();
            _logger = new Mock<ILogger<GameService>>();
            _sut = new GameService(_gameRepository.Object, _logger.Object);
        }

        [Fact]
        public async void Throw_Exception_If_Game_NotFound()
        {
            var request = new UpdateMoveRequest();
            var gameId = Guid.NewGuid().ToString();
            _gameRepository.Setup(x => x.GetAsync(gameId, It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync((GameDto)null);

            var ex = await Assert.ThrowsAsync<WebApiException>(() => _sut.UpdateMoveAsync(gameId, request));

            Assert.Equal(HttpStatusCode.BadRequest, ex.HttpStatusCode);
            Assert.Equal(101, ex.ErrorCode);
        }

        [Fact]
        public async void Throw_Exception_If_RowPosition_OutOf_BoardSize()
        {
            var gameId = Guid.NewGuid().ToString();
            var request = new UpdateMoveRequest { PlayerId = 1, rowPosition = 15, columnPosition = 10 };
            var game = new GameDto { Id = gameId, RowSize = 12, ColumnSize = 12 };
            _gameRepository.Setup(x => x.GetAsync(gameId, It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(game);

            var ex = await Assert.ThrowsAsync<WebApiException>(() => _sut.UpdateMoveAsync(gameId, request));

            Assert.Equal(HttpStatusCode.BadRequest, ex.HttpStatusCode);
            Assert.Equal(102, ex.ErrorCode);
        }

        [Fact]
        public async void Return_MoveNotAllowed_If_Position_Already_Used()
        {
            var gameId = Guid.NewGuid().ToString();
            var request = new UpdateMoveRequest { PlayerId = 1, rowPosition = 5, columnPosition = 6 };
            var gameData = new int[12, 12];
            gameData[5, 6] = 1;
            var game = new GameDto { Id = gameId, Data = gameData, RowSize = 12, ColumnSize = 12 };
            _gameRepository.Setup(x => x.GetAsync(gameId, It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(game);

            var moveResult = await _sut.UpdateMoveAsync(gameId, request);

            Assert.True(moveResult.MoveNotAllowed);
        }

        [Fact]
        public async void Return_IsGameOverTrue_If_Has_Winner()
        {
            var gameId = Guid.NewGuid().ToString();
            int rowSize = 7, columnSize = 7;
            var request = new UpdateMoveRequest { PlayerId = 1, rowPosition = 4, columnPosition = 4 };
            var gameData = new int[rowSize, columnSize];
            int playerId = 1;
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    if (i == 4 && j == 4)
                    {
                        continue;
                    }
                    if (playerId == 1)
                    {
                        gameData[i, j] = 1;
                        playerId = 2;
                        continue;
                    }
                    gameData[i, j] = 2;
                    playerId = 1;
                }
            }
            var game = new GameDto { Id = gameId, Data = gameData, RowSize = rowSize, ColumnSize = columnSize };
            _gameRepository.Setup(x => x.GetAsync(gameId, It.IsAny<string>(), It.IsAny<CancellationToken>())).ReturnsAsync(game);

            var moveResult = await _sut.UpdateMoveAsync(gameId, request);

            Assert.False(moveResult.IsDraw);
            Assert.True(moveResult.IsGameOver);
            Assert.False(moveResult.MoveNotAllowed);
        }
    }
}
