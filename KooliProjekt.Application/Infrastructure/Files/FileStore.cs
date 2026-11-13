using System.IO;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public static class FileStore
    {
        // Juurkaust (FilesRoot alla)
        public const string Teams = "teams";

        // Iga tiimi failid asuvad eraldi kaustas: {FilesRoot}/teams/{teamId}/{failinimi}
        public static string ForTeam(int teamId)
        {
            return Path.Combine(Teams, teamId.ToString());
        }
    }
}
