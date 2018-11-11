using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnglishPremierLeagueResultProcessor
{
    public class FileValidator : IFileValidator
    {
        public bool ValidateIfFileExists(string fileName)
        {
            if (File.Exists(fileName))
                return true;

            throw new FileNotFoundException(fileName + " does not exist.");

        }

        public void ValidateHeader(List<string> columnList)
        {
            if(!columnList.Contains(Constants.ColTeamName))
                throw new ApplicationException(ErrorMessages.ColTeamMissing);
            if (!columnList.Contains(Constants.ColFor))
                throw new ApplicationException(ErrorMessages.ColForMissing);
            if (!columnList.Contains(Constants.ColAgainst))
                throw new ApplicationException(ErrorMessages.ColAgainstMissing);


            if (columnList.Count( col => col == Constants.ColTeamName) > 1)
                throw new ApplicationException(ErrorMessages.ColTeamMoreThanOne);
            if (columnList.Count(col => col == Constants.ColFor) > 1)
                throw new ApplicationException(ErrorMessages.ColForMoreThanOne);
            if (columnList.Count(col => col == Constants.ColAgainst) > 1)
                throw new ApplicationException(ErrorMessages.ColAgainstMoreThanOne);
        }

        public bool ValidateDataRowValues(List<string> dataRowValues, int indexOfTeamCol, int indexOfForCol, int indexOfAgainstCol)
        {
            if (dataRowValues.Count < indexOfTeamCol + 1)
                return false;
            if (dataRowValues.Count < indexOfForCol + 1)
                return false;
            if (dataRowValues.Count < indexOfAgainstCol + 1)
                return false;

            if (string.IsNullOrWhiteSpace(dataRowValues[indexOfTeamCol]))
                return false;
            if (string.IsNullOrWhiteSpace(dataRowValues[indexOfForCol]))
                return false;
            if (string.IsNullOrWhiteSpace(dataRowValues[indexOfAgainstCol]))
                return false;

            if (!int.TryParse(dataRowValues[indexOfForCol], out _))
                return false;
            if (!int.TryParse(dataRowValues[indexOfAgainstCol], out _))
                return false;

            return true;
        }
    }
}
