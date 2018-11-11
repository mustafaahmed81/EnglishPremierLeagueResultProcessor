using System.Collections.Generic;

namespace EnglishPremierLeagueResultProcessor
{
    public interface ILeagueResultParser
    {
        IList<LeagueResult> ReadIntoLeagueResults(IList<string> headerColumnList, IList<string> dataRows);
    }
}