using BoardGames.Gomoku.Repositories.Interfaces;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGames.Gomoku.Repositories
{
    public class CosmosDBRepository<T> : ICosmosDBRepository<T>
    {
        private Container _container;

        public CosmosDBRepository(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task<T> AddItemAsync(T item, string partitionKey)
        {
            var itemResponse = await this._container.CreateItemAsync<T>(item, new PartitionKey(partitionKey));
            return itemResponse.Resource;
        }

        public async Task DeleteItemAsync(string partitionKey)
        {
            await this._container.DeleteItemAsync<T>(partitionKey, new PartitionKey(partitionKey));
        }

        public async Task<T> GetItemAsync(string partitionKey)
        {
            try
            {
                ItemResponse<T> response = await this._container.ReadItemAsync<T>(partitionKey, new PartitionKey(partitionKey));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default;
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<T>(new QueryDefinition(queryString));
            List<T> results = new List<T>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateItemAsync(string partitionKey, T item)
        {
            await this._container.UpsertItemAsync<T>(item, new PartitionKey(partitionKey));
        }
    }
}
