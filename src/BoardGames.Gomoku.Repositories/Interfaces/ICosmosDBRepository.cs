using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoardGames.Gomoku.Repositories.Interfaces
{
    public interface ICosmosDBRepository<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string query);
        Task<T> GetItemAsync(string partitionKey);
        Task<T> AddItemAsync(T item, string partitionKey);
        Task UpdateItemAsync(string partitionKey, T item);
        Task DeleteItemAsync(string partitionKey);
    }
}
