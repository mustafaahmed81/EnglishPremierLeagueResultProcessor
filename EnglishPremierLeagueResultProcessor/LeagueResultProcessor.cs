using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishPremierLeagueResultProcessor
{
    public class LeagueResultProcessor : ILeagueResultProcessor
    {
        private readonly ILeagueResultParser _leagueResultParser;

        public LeagueResultProcessor(ILeagueResultParser leagueResultParser)
        {
            _leagueResultParser = leagueResultParser;
        }

        public LeagueResult FindWinningTeam(FileOutput fileOutput)
        {
            IList<LeagueResult> leagueResults = _leagueResultParser.ReadIntoLeagueResults(fileOutput.HeaderColumnList, fileOutput.DataRows);
            LeagueResult winningTeam = FindWinningTeam(leagueResults);

            return winningTeam;
        }

        private LeagueResult FindWinningTeam(IList<LeagueResult> leagueResults)
        {
            var minDiffBetweenForAndAgainst = leagueResults.Min(league => Math.Abs(league.GoalsFor - league.GoalsAgainst));
            var winningTeam = leagueResults.First(league => minDiffBetweenForAndAgainst == Math.Abs(league.GoalsFor - league.GoalsAgainst));

            return winningTeam;
        }
    }

}

