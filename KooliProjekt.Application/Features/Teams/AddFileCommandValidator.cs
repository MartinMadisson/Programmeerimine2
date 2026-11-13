using FluentValidation;
using KooliProjekt.Application.Data;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Teams
{
    public class AddFileCommandValidator : AbstractValidator<AddFileCommand>
    {
        public AddFileCommandValidator(ApplicationDbContext dbContext)
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(1).WithMessage("Id must be greater than or equal to 1.")
                .MustAsync(async (id, ct) => await dbContext.Teams.AnyAsync(t => t.Id == id, ct))
                .WithMessage("The specified team does not exist.");

            RuleFor(x => x.FileName)
                .Cascade(CascadeMode.Stop)
                .ValidFileName()
                .Must(FileRules.HasAllowedExtension)
                .WithMessage("Only .jpg, .jpeg, .png, .gif and .webp files are allowed.");

            RuleFor(x => x.InputStream)
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("File is required.")
                .Must(s => !s.CanSeek || s.Length > 0).WithMessage("File must not be empty.")
                .Must(s => !s.CanSeek || s.Length <= FileRules.MaxFileSizeBytes)
                .WithMessage("File must not be larger than 2 MB.");
        }
    }
}
