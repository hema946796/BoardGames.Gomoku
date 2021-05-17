using BoardGames.Gomoku.Business.Interfaces;
using BoardGames.Gomoku.Common;
using BoardGames.Gomoku.Entities;
using BoardGames.Gomoku.Models;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace BoardGames.Gomoku.Business
{
    public class GameService : IGameService
    {
        private readonly IRepository<GameDto> _gameRepository;
        private readonly ILogger<GameService> _logger;

        public GameService(IRepository<GameDto> gameRepository, ILogger<GameService> logger)
        {
            _gameRepository = gameRepository;
            _logger = logger;
        }

        public async Task<MoveResult> UpdateMoveAsync(string gameId, UpdateMoveRequest request)
        {
            var game = await _gameRepository.GetAsync(gameId);
            if (game == null)
            {
                _logger.LogError($"Game not found. Id - {gameId}");
                throw new WebApiException(HttpStatusCode.BadRequest, 101, $"Game {gameId} not found.");
            }
            var gameData = game.Data;
            int rowPosition = request.rowPosition, columnPosition = request.columnPosition;

            if (rowPosition >= game.RowSize || columnPosition >= game.ColumnSize)
            {
                _logger.LogError($"Invalid row or column position. row - {rowPosition}, column - {columnPosition}");
                throw new WebApiException(HttpStatusCode.BadRequest, 102, $"Invalid row/column position.");
            }

            if (gameData[rowPosition, columnPosition] != 0)
            {
                _logger.LogInformation($"Position already used. row - {rowPosition}, column - {columnPosition}");
                return new MoveResult { GameId = gameId, MoveNotAllowed = true };
            }
            gameData[rowPosition, columnPosition] = request.PlayerId;

            if (IsDraw(gameData, game.RowSize, game.ColumnSize))
            {
                game.IsDraw = true;
                game.IsGameOver = true;

            }
            if (IsGameOver(gameData, request.PlayerId, game.RowSize, game.ColumnSize))
            {
                game.IsDraw = false;
                game.IsGameOver = true;
            }
            await _gameRepository.UpdateAsync(game);
            return new MoveResult { GameId = gameId, IsDraw = game.IsDraw, IsGameOver = game.IsGameOver }; ;
        }

        public bool IsGameOver(int[,] gameData, int playerId, int rowsize, int columnSize)
        {
            bool Win = false;
            for (int i = 0; i < rowsize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    if (gameData[i, j] != 0)
                    {
                        //Vertical judgment
                        if (j < columnSize - 4)
                        {
                            if (gameData[i, j] == playerId && gameData[i, j + 1] == playerId && gameData[i, j + 2] == playerId && gameData[i, j + 3] == playerId && gameData[i, j + 4] == playerId)
                            {
                                return true;
                            }
                        }
                        //Horizontal judgment
                        if (i < rowsize - 4)
                        {
                            if (gameData[i, j] == playerId && gameData[i + 4, j] == playerId && gameData[i + 1, j] == playerId && gameData[i + 2, j] == playerId && gameData[i + 3, j] == playerId)
                            {
                                return true;
                            }
                        }
                        //Judge diagonally to the lower right
                        if (i < rowsize - 4 && j < columnSize - 4)
                        {
                            if (gameData[i, j] == playerId && gameData[i + 1, j + 1] == playerId && gameData[i + 2, j + 2] == playerId && gameData[i + 3, j + 3] == playerId && gameData[i + 4, j + 4] == playerId)
                            {
                                return true;
                            }
                        }
                        // Judging diagonally to the left
                        if (i >= 4 && j < columnSize - 4)
                        {
                            if (gameData[i, j] == playerId && gameData[i - 1, j + 1] == playerId && gameData[i - 2, j + 2] == playerId && gameData[i - 3, j + 3] == playerId && gameData[i - 4, j + 4] == playerId)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return Win;
        }

        private bool IsDraw(int[,] gameData, int rowSize, int columnSize)
        {
            bool isDraw = true;
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < columnSize; j++)
                {
                    if (gameData[i, j] == 0)
                        return false;
                }
            }
            return isDraw;
        }
    }
}
