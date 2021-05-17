using BoardGames.Gomoku.Common;
using BoardGames.Gomoku.Models;
using FluentValidation;

namespace BoardGames.Gomoku.API.Validators
{
    public class BoardValidator : AbstractValidator<CreateBoardRequest>
    {
        public BoardValidator()
        {
            RuleFor(m => m.RowSize).ExclusiveBetween(Constants.MIN_ROW_SIZE, Constants.MAX_ROW_SIZE)
                                    .WithMessage($"Invalid row size. Must be between {Constants.MIN_ROW_SIZE} and {Constants.MAX_ROW_SIZE}");
            RuleFor(m => m.ColumnSize).ExclusiveBetween(Constants.MIN_COLUMN_SIZE, Constants.MAX_COLUMN_SIZE)
                                    .WithMessage($"Invalid column size. Must be between {Constants.MIN_COLUMN_SIZE} and {Constants.MAX_COLUMN_SIZE}");
        }
    }
}
