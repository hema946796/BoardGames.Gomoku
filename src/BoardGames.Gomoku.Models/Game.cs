using Newtonsoft.Json;

namespace BoardGames.Gomoku.Models
{
    public class Game
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "boardId")]
        public int BoardId { get; set; }

        [JsonProperty(PropertyName = "gamename")]
        public string Gamename { get; set; }

        [JsonProperty(PropertyName = "data")]
        public int[,] Data { get; set; }
    }
}
