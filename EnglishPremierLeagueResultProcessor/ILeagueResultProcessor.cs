namespace EnglishPremierLeagueResultProcessor
{
    public interface ILeagueResultProcessor
    {
        LeagueResult FindWinningTeam(FileOutput fileOutput);
    }
}