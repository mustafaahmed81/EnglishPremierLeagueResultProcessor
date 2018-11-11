using System;
using System.Collections.Generic;
using System.IO;
using EnglishPremierLeagueResultProcessor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EnglishPremierLeagueResultTests
{
    [TestClass]
    public class CsvFileReaderTests
    {
        private readonly string _filePath = @"C:\Mustafa Data\Professional\Projects\EnglishPremierLeagueResultProcessor\CsvFile\football.csv";
        
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void WhenCsvFileNotExistShouldThrowFileNotFoundException()
        {
            //Arrange
            var configReaderMock = new Mock<IConfigReader>();

            configReaderMock.Setup(x => x.FileName).Returns(It.IsAny<string>());
            configReaderMock.Setup(x => x.Delimiter).Returns(",");

            var fileValidatorMock = new Mock<IFileValidator>();

            fileValidatorMock.Setup(x => x.ValidateIfFileExists(configReaderMock.Object.FileName)).Throws<FileNotFoundException>();

            //Act
            var csvFileReader = new CsvFileReader(configReaderMock.Object, fileValidatorMock.Object);

            //Assert
            csvFileReader.ReadFileData();
        }

        [TestMethod]
        [ExpectedException(typeof(ApplicationException))]
        public void WhenCsvFileHasInvalidHeaderShouldThrowApplicationException()
        {
            //Arrange
            var configReaderMock = new Mock<IConfigReader>();

            configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            configReaderMock.Setup(x => x.Delimiter).Returns(",");

            var fileValidatorMock = new Mock<IFileValidator>();

            fileValidatorMock.Setup(x => x.ValidateIfFileExists(configReaderMock.Object.FileName)).Returns(true);
            fileValidatorMock.Setup(x => x.ValidateHeader(It.IsAny<List<string>>())).Throws<ApplicationException>();

            //Act
            var csvFileReader = new CsvFileReader(configReaderMock.Object, fileValidatorMock.Object);

            //Assert
            csvFileReader.ReadFileData();
        }

        [TestMethod]
        public void WhenValidCsvFileProvidedShouldReturnFileOutput()
        {
            //Arrange
            var configReaderMock = new Mock<IConfigReader>();

            configReaderMock.Setup(x => x.FileName).Returns(_filePath);
            configReaderMock.Setup(x => x.Delimiter).Returns(",");

            var fileValidatorMock = new Mock<IFileValidator>();
            fileValidatorMock.Setup(x => x.ValidateIfFileExists(configReaderMock.Object.FileName)).Returns(true);
            var csvFileReader = new CsvFileReader(configReaderMock.Object, fileValidatorMock.Object);

            //Act
            var fileOutput = csvFileReader.ReadFileData();

            //Assert
            Assert.IsNotNull(fileOutput);
            Assert.IsInstanceOfType(fileOutput, fileOutput.GetType());
        }
    }
}
