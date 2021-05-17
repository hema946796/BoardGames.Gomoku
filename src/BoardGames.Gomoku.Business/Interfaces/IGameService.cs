using BoardGames.Gomoku.Models;
using System.Threading.Tasks;

namespace BoardGames.Gomoku.Business.Interfaces
{
    public interface IGameService
    {
        Task<MoveResult> UpdateMoveAsync(string gameId, UpdateMoveRequest request);
    }
}
