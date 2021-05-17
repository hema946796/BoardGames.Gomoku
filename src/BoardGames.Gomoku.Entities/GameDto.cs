using Microsoft.Azure.CosmosRepository;
using Newtonsoft.Json;

namespace BoardGames.Gomoku.Entities
{
    public class GameDto : Item
    {
        [JsonProperty(PropertyName = "gamename")]
        public string GameName { get; set; }

        [JsonProperty(PropertyName = "rowSize")]
        public int RowSize { get; set; }

        [JsonProperty(PropertyName = "cloumnSize")]
        public int ColumnSize { get; set; }

        [JsonProperty(PropertyName = "data")]
        public int[,] Data { get; set; }

        [JsonProperty(PropertyName = "isGameOver")]
        public bool IsGameOver { get; set; }

        [JsonProperty(PropertyName = "isDraw")]
        public bool IsDraw { get; set; }
    }
}
