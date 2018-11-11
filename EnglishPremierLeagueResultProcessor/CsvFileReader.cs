using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnglishPremierLeagueResultProcessor
{
    public class CsvFileReader : IFileReader
    {
        private readonly IConfigReader _configReader;
        private readonly IFileValidator _validator;

        public CsvFileReader(IConfigReader configReader, IFileValidator validator)
        {
            _configReader = configReader;
            _validator = validator;
        }

        public FileOutput ReadFileData()
        {
            _validator.ValidateIfFileExists(_configReader.FileName);

            var headerAndRows = ReadHeaderAndRows();
            var header = headerAndRows.Item1;

            List<string> headerColumnList = header
                .Split(_configReader.Delimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

            _validator.ValidateHeader(headerColumnList);

            return new FileOutput() {HeaderColumnList = headerColumnList, DataRows = headerAndRows.Item2};
        }

        private Tuple<string, List<string>> ReadHeaderAndRows()
        {
            using (var reader = new StreamReader(_configReader.FileName))
            {
                string header = null;
                List<string> dataRows = new List<string>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (header == null) // first time reading
                        header = line;
                    else
                        dataRows.Add(line);
                }

                return new Tuple<string, List<string>>(header, dataRows);
            }
        }
    }
}
