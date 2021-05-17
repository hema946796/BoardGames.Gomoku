namespace BoardGames.Gomoku.Models
{
    public class UpdateMoveRequest
    {
        public int PlayerId { get; set; }
        public int rowPosition { get; set; }
        public int columnPosition { get; set; }
    }
}
