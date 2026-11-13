using FluentValidation;

namespace KooliProjekt.Application.Features.Scoreboards
{
    public class GetScoreboardQueryValidator : AbstractValidator<GetScoreboardQuery>
    {
        public GetScoreboardQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(1).WithMessage("Id must be greater than or equal to 1.");
        }
    }
}
