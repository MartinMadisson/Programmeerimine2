namespace KooliProjekt.Application.Dto
{
    public class ScoreboardItemDto
    {
        public int Id { get; set; }
        public int TournamentId { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
    }
}
