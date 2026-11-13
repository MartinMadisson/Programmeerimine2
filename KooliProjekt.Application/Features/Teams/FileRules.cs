using System;
using System.IO;
using System.Linq;
using FluentValidation;

namespace KooliProjekt.Application.Features.Teams
{
    public static class FileRules
    {
        public static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        public const long MaxFileSizeBytes = 2 * 1024 * 1024; // 2 MB
        public const int MaxFileNameLength = 100;

        public static bool HasAllowedExtension(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            return AllowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }

        // Faili nimi ei tohi sisaldada kaustateid (../, / või \)
        public static bool IsSafeFileName(string fileName)
        {
            return !string.IsNullOrWhiteSpace(fileName)
                && fileName == Path.GetFileName(fileName)
                && fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
        }

        public static IRuleBuilderOptions<T, string> ValidFileName<T>(this IRuleBuilder<T, string> rule)
        {
            return rule
                .NotEmpty().WithMessage("FileName is required.")
                .MaximumLength(MaxFileNameLength).WithMessage($"FileName must not exceed {MaxFileNameLength} characters.")
                .Must(IsSafeFileName).WithMessage("FileName must not contain path characters.");
        }
    }
}
