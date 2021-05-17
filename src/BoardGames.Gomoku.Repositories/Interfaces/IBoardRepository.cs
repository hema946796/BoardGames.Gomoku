using BoardGames.Gomoku.Entities;
using Microsoft.Azure.CosmosRepository;

namespace BoardGames.Gomoku.Repositories.Interfaces
{
    public interface IBoardRepository : IRepository<GameDto>
    {
        //Task AddNewBoardAsync(string gameName, int rowSize, int columnSize);
    }
}
