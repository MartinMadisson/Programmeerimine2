namespace KooliProjekt.Application.Dto
{
    public class MatchDto
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public int TournamentId { get; set; }
        public int Teamt1Id { get; set; }
        public int Team2Id { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public int RoundId { get; set; }
        public System.Collections.Generic.IList<PredictionDto> Predictions { get; set; }
    }
}
