using System.Configuration;

namespace EnglishPremierLeagueResultProcessor
{
    public class ConfigReader : IConfigReader
    {
        public string FileName => ConfigurationManager.AppSettings["FileNameWithPath"];
        public string Delimiter => ConfigurationManager.AppSettings["Delimiter"];
    }
}
