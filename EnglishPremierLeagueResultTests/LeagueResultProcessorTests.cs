using System.Collections.Generic;
using EnglishPremierLeagueResultProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EnglishPremierLeagueResultTests
{
    [TestClass]
    public class LeagueResultProcessorTests
    {
        [TestMethod]
        public void WhenValidCsvFileOutputProvidedShouldReturnSingleLeagueResultObject()
        {
            //Arrange
            IList<LeagueResult> leagueResultList = new List<LeagueResult>
            {
                new LeagueResult {TeamName = "1. Arsenal", GoalsFor = 79, GoalsAgainst = 36},
                new LeagueResult {TeamName = "2. Liverpool", GoalsFor = 67, GoalsAgainst = 30},
                new LeagueResult {TeamName = "3. Manchester_U", GoalsFor = 87, GoalsAgainst = 45},
                new LeagueResult {TeamName = "4. Newcastle", GoalsFor = 74, GoalsAgainst = 52},
                new LeagueResult {TeamName = "5. Leeds", GoalsFor = 79, GoalsAgainst = 37},
                new LeagueResult {TeamName = "6. Chelsea", GoalsFor = 66, GoalsAgainst = 38},
                new LeagueResult {TeamName = "7. West_Ham", GoalsFor = 48, GoalsAgainst = 57}
            };
            var winningTeamExpected = new LeagueResult { TeamName = "7. West_Ham", GoalsFor = 48, GoalsAgainst = 57 };

            var leagueResultParserMock = new Mock<ILeagueResultParser>();
            leagueResultParserMock.Setup(x => x.ReadIntoLeagueResults(It.IsAny<List<string>>(), It.IsAny<List<string>>())).Returns(leagueResultList);
            
            //Act
            var leagueResultProcessor = new LeagueResultProcessor(leagueResultParserMock.Object);
            var winningTeam = leagueResultProcessor.FindWinningTeam(new FileOutput());

            //Assert
            Assert.IsNotNull(winningTeam);
            Assert.AreEqual(winningTeamExpected.TeamName, winningTeam.TeamName);
            Assert.AreEqual(winningTeam.GoalsFor, winningTeamExpected.GoalsFor);
            Assert.AreEqual(winningTeam.GoalsAgainst, winningTeamExpected.GoalsAgainst);
        }
    }
}
