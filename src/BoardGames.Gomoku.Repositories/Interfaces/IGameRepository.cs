using System.Threading.Tasks;

namespace BoardGames.Gomoku.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task UpdateMoveAsync(string gameName, int playerId, int rowPosition, int columnPosition);
    }
}
