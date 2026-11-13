using FluentValidation;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class DeleteScoreboardCommandValidator : AbstractValidator<DeleteScoreboardCommand>
    {
        public DeleteScoreboardCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(1).WithMessage("Id must be greater than or equal to 1.");
        }
    }
}
