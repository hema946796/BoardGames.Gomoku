using BoardGames.Gomoku.Models;
using FluentValidation;

namespace BoardGames.Gomoku.API.Validators
{
    public class GameValidator : AbstractValidator<UpdateMoveRequest>
    {
        public GameValidator()
        {
            RuleFor(m => m.PlayerId).NotEmpty().WithMessage("GameId is missing.");
            RuleFor(m => m.rowPosition).NotEmpty().WithMessage("Invalid Move. Row position is missing.");
            RuleFor(m => m.columnPosition).NotEmpty().WithMessage("Invalid Move. Column position is missing.");
        }
    }
}
