using BoardGames.Gomoku.Models;
using System.Threading.Tasks;

namespace BoardGames.Gomoku.Business.Interfaces
{
    public interface IBoardService
    {
        Task<Board> CreateBoardAsync(string gameName, int rowSize, int columnSize);
    }
}
