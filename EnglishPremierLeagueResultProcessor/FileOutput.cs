using System.Collections.Generic;

namespace EnglishPremierLeagueResultProcessor
{
    public class FileOutput
    {
        public IList<string> HeaderColumnList { get; set; }
        public IList<string> DataRows { get; set; }
    }
}