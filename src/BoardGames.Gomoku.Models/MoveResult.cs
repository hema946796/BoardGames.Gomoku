namespace BoardGames.Gomoku.Models
{
    public class MoveResult
    {
        public string GameId { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsDraw { get; set; }
        public bool MoveNotAllowed { get; set; }
    }
}
