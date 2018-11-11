using System;
using System.Collections.Generic;
using System.Linq;

namespace EnglishPremierLeagueResultProcessor
{
    public class LeagueResultParser : ILeagueResultParser
    {
        private readonly IConfigReader _configReader;
        private readonly IFileValidator _validator;

        public LeagueResultParser(IConfigReader configReader, IFileValidator validator)
        {
            _configReader = configReader;
            _validator = validator;
        }

        public IList<LeagueResult> ReadIntoLeagueResults(IList<string> headerColumnList, IList<string> dataRows)
        {
            int indexOfTeamCol = GetIndexOfColumn(headerColumnList, Constants.ColTeamName);
            int indexOfForCol = GetIndexOfColumn(headerColumnList, Constants.ColFor);
            int indexOfAgainstCol = GetIndexOfColumn(headerColumnList, Constants.ColAgainst);

            List < LeagueResult > leagueResults = new List<LeagueResult>();
            foreach (string dataRow in dataRows)
            {
                List<string> dataRowValues = dataRow.Split(_configReader.Delimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
                if(!_validator.ValidateDataRowValues(dataRowValues, indexOfTeamCol, indexOfForCol, indexOfAgainstCol))
                    continue;
                var leagueResult = new LeagueResult
                {
                    TeamName = dataRowValues[indexOfTeamCol].Trim(),
                    GoalsFor = int.Parse(dataRowValues[indexOfForCol]),
                    GoalsAgainst = int.Parse(dataRowValues[indexOfAgainstCol])
                };

                leagueResults.Add(leagueResult);
            }

            return leagueResults;
        }

        private static int GetIndexOfColumn(IList<string> columnList, string columnName)
        {
            return columnList.IndexOf(columnName);
        }
    }
}
