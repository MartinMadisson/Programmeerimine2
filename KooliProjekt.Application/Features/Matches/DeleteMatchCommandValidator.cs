using FluentValidation;

namespace KooliProjekt.Application.Features.Matches
{
    public class DeleteMatchCommandValidator : AbstractValidator<DeleteMatchCommand>
    {
        public DeleteMatchCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(1).WithMessage("Id must be greater than or equal to 1.");
        }
    }
}
