namespace EnglishPremierLeagueResultProcessor
{
    public interface IConfigReader
    {
        string FileName { get; }
        string Delimiter { get; }
    }
}