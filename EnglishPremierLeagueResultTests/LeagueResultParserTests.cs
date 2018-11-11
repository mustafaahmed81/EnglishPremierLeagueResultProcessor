using System.Collections.Generic;
using EnglishPremierLeagueResultProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EnglishPremierLeagueResultTests
{
    [TestClass]
    public class LeagueResultParserTests
    {
        private readonly string _filePath = @"C:\Mustafa Data\Professional\Projects\EnglishPremierLeagueResultProcessor\CsvFile\football.csv";

        [TestMethod]
        public void WhenInvalidDataIsProvidedInCsvFileThenItShouldBeIgnored()
        {
            //Arrange
            var configReaderMock = new Mock<IConfigReader>();

            configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            configReaderMock.Setup(x => x.Delimiter).Returns(",");

            var fileValidator = new FileValidator();

            //Act
            var leagueResultParser = new LeagueResultParser(configReaderMock.Object, fileValidator);

            IList<string> headerColumnList = new List<string> { "Team","P", "W", "L", "D", "F", "-", "A", "Pts"};
            IList<string> dataRows = new List<string>
            {
                "1. Arsenal,38,26,9,3,79,-,36,87",
                "2. Liverpool,38,24,8,6,67,-,30,80",
                "3. Manchester_U,38,24,5,9,87,-,45,77",
                "4. Newcastle,38,21,8,9,74,-,52,71",
                "5.------------------, , , , , , , ,"
            };
            var leagueResultList = leagueResultParser.ReadIntoLeagueResults(headerColumnList, dataRows);
            
            //Assert
            Assert.IsNotNull(leagueResultList);
            Assert.AreNotEqual(dataRows.Count, leagueResultList.Count);
        }

        [TestMethod]
        public void WhenValidDataIsProvidedInCsvFileThenItShouldBeInLeagueResult()
        {
            //Arrange
            var configReaderMock = new Mock<IConfigReader>();

            configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            configReaderMock.Setup(x => x.Delimiter).Returns(",");

            var fileValidator = new FileValidator();

            //Act
            var leagueResultParser = new LeagueResultParser(configReaderMock.Object, fileValidator);

            IList<string> headerColumnList = new List<string> { "Team", "P", "W", "L", "D", "F", "-", "A", "Pts" };
            IList<string> dataRows = new List<string>
            {
                "1. Arsenal,38,26,9,3,79,-,36,87",
                "2. Liverpool,38,24,8,6,67,-,30,80",
                "3. Manchester_U,38,24,5,9,87,-,45,77",
                "4. Newcastle,38,21,8,9,74,-,52,71"
            };
            var leagueResultList = leagueResultParser.ReadIntoLeagueResults(headerColumnList, dataRows);

            //Assert
            Assert.IsNotNull(leagueResultList);
            Assert.AreEqual(dataRows.Count, leagueResultList.Count);
        }
    }
}
