using System.Collections.Generic;

namespace EnglishPremierLeagueResultProcessor
{
    public interface IFileValidator
    {
        bool ValidateIfFileExists(string fileName);
        void ValidateHeader(List<string> columnList);
        bool ValidateDataRowValues(List<string> dataRowValues, int indexOfTeamCol, int indexOfForCol, int indexOfAgainstCol);
    }
}