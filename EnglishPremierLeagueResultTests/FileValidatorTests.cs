using System;
using System.Collections.Generic;
using System.IO;
using EnglishPremierLeagueResultProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EnglishPremierLeagueResultTests
{
    [TestClass]
    public class FileValidatorTests
    {
        private readonly string _filePath = @"C:\Mustafa Data\Professional\Projects\EnglishPremierLeagueResultProcessor\CsvFile\football.csv";

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void WhenFileNotExistThrowFileNotFoundException()
        {
            //Arrange
            var configReaderMock = new Mock<IConfigReader>();

            configReaderMock.Setup(x => x.FileName).Returns(It.IsAny<string>());
            configReaderMock.Setup(x => x.Delimiter).Returns(",");

            //Act
            var validator = new FileValidator();
            validator.ValidateIfFileExists(configReaderMock.Object.FileName);

            //Assert
        }

        [TestMethod]
        public void WhenFileExistReturnTrue()
        {
            //Arrange
            var configReaderMock = new Mock<IConfigReader>();

            configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            configReaderMock.Setup(x => x.Delimiter).Returns(",");

            //Act
            var validator = new FileValidator();
            
            //Assert
            Assert.IsTrue(validator.ValidateIfFileExists(configReaderMock.Object.FileName));
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void WhenRequiredHeaderNotExistThrowApplicationException()
        {
            //Arrange
            var columnList = new List<string> {"Header1", "Header2","Header3","Header4","Header5","Header6" };
            //var configReaderMock = new Mock<IConfigReader>();

            //configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            //configReaderMock.Setup(x => x.Delimiter).Returns(",");
            var validator = new FileValidator();

            //Act
            validator.ValidateHeader(columnList);

            //Assert
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void WhenRequiredHeaderExistMoreThanOneThrowApplicationException()
        {
            //Arrange
            var columnList = new List<string> { Constants.ColTeamName, "Header2", Constants.ColFor, Constants.ColAgainst, Constants.ColAgainst, "Header6" };
            //var configReaderMock = new Mock<IConfigReader>();

            //configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            //configReaderMock.Setup(x => x.Delimiter).Returns(",");
            var validator = new FileValidator();

            //Act
            validator.ValidateHeader(columnList);

            //Assert
        }


        [TestMethod]
        public void WhenRequiredHeaderExistShouldNotThrowApplicationException()
        {
            //Arrange
            var columnList = new List<string> { Constants.ColTeamName, "Header2", Constants.ColFor, "Header4", Constants.ColAgainst, "Header6" };
            //var configReaderMock = new Mock<IConfigReader>();

            //configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            //configReaderMock.Setup(x => x.Delimiter).Returns(",");
            var validator = new FileValidator();

            //Act
            try
            {
                validator.ValidateHeader(columnList);
            }
            //Assert
            catch (ApplicationException e)
            {
                Assert.Fail(e.Message);
            }
            
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void WhenIndexOfColGreaterThanDataRowValuesCountThenShouldReturnFalse()
        {
            //Arrange
            var columnList = new List<string> { Constants.ColTeamName, "Header2", "Header3", "Header4", "Header5", Constants.ColFor, "Header7", Constants.ColAgainst, "Header9" };
            //var configReaderMock = new Mock<IConfigReader>();

            //configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            //configReaderMock.Setup(x => x.Delimiter).Returns(",");
            var validator = new FileValidator();

            var dataRowValues = new List<string> { "1.Arsenal", "38", "26", "9", "3", "79", "-", "36", "87" };

            //Act
            var result = validator.ValidateDataRowValues(dataRowValues, 0, 5, 10);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenNullWhiteSpaceFoundOnRequiredIndexThenShouldReturnFalse()
        {
            //Arrange
            var columnList = new List<string> { Constants.ColTeamName, "Header2", "Header3", "Header4", "Header5", Constants.ColFor, "Header7", Constants.ColAgainst, "Header9" };
            //var configReaderMock = new Mock<IConfigReader>();

            //configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            //configReaderMock.Setup(x => x.Delimiter).Returns(",");
            var validator = new FileValidator();

            var dataRowValues = new List<string> { "1.Arsenal", "38", "26", "9", "3", " ", "-", "36", "87" };

            //Act
            var result = validator.ValidateDataRowValues(dataRowValues, 0, 5, 7);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenInvalidDataRowValuesProvidedThenShouldReturnFalse()
        {
            //Arrange
            var columnList = new List<string> { Constants.ColTeamName, "Header2", "Header3", "Header4", "Header5", Constants.ColFor, "Header7", Constants.ColAgainst, "Header9" };
            //var configReaderMock = new Mock<IConfigReader>();

            //configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            //configReaderMock.Setup(x => x.Delimiter).Returns(",");
            var validator = new FileValidator();

            var dataRowValues = new List<string> { "1.Arsenal", "38", "26", "9", "3", "79A", "-", "36", "87" };

            //Act
            var result = validator.ValidateDataRowValues(dataRowValues, 0, 5, 7);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void WhenValidDataRowValuesProvidedShouldReturnTrue()
        {
            //Arrange
            var columnList = new List<string> { Constants.ColTeamName, "Header2", "Header3", "Header4", "Header5", Constants.ColFor, "Header7", Constants.ColAgainst, "Header9" };
            //var configReaderMock = new Mock<IConfigReader>();

            //configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            //configReaderMock.Setup(x => x.Delimiter).Returns(",");
            var validator = new FileValidator();

            var dataRowValues = new List<string> { "1.Arsenal", "38", "26", "9", "3", "79", "-", "36", "87" };

            //Act
            var result = validator.ValidateDataRowValues(dataRowValues, 0, 5, 7);

            //Assert
            Assert.IsTrue(result);
        }
    }
}
