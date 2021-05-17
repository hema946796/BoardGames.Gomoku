namespace BoardGames.Gomoku.Models
{
    public class CreateBoardRequest
    {
        public string GameName { get; set; }
        public int RowSize { get; set; }
        public int ColumnSize { get; set; }
    }
}
