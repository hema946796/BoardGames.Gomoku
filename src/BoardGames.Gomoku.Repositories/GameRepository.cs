using BoardGames.Gomoku.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace BoardGames.Gomoku.Repositories
{
    public class GameRepository : IGameRepository
    {
        public Task UpdateMoveAsync(string gameName, int playerId, int rowPosition, int columnPosition)
        {
            throw new NotImplementedException();
        }
    }
}
