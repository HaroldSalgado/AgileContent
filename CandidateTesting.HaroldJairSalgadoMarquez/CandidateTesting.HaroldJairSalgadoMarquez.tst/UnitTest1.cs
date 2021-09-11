using NUnit.Framework;
using CandidateTesting.HaroldJairSalgadoMarquez.Business.Validators;
using CandidateTesting.HaroldJairSalgadoMarquez.Data.Facades;
using CandidateTesting.HaroldJairSalgadoMarquez.Business.Managers;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Collections.Generic;

namespace CandidateTesting.HaroldJairSalgadoMarquez.test
{
    public class Tests
    {
        private IConfiguration _mockAppConfiguration { get; set; }
        private string _validUrl { get; set; }
        private string _notValidURL { get; set; }
        public string _mockApiResponse { get; set; }
        [SetUp]
        public void Setup()
        {
            //Mock Values
            _validUrl = "https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt";
            _notValidURL = "Not a valid URL";
            _mockApiResponse = "312 | 200 | HIT |\"GET /robots.txt HTTP/1.1\"|100.2\r\n101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4\r\n199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9\r\n312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1\r\n";

            //Configuration
            var inMemorySettings = new Dictionary<string, string> {
            {"version", "1.0"},
            {"format", "\"{{provider}}\" {{http-method}} {{status-code}} {{uri-path}} {{time-taken}} {{response-size}} {{cache-status}}"},
            {"provider","MINHA CDN" } };

            _mockAppConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
        }

        [Test]
        public void It_is_A_Valid_URL()
        {
            //Arrange
            var validator = new SourceValidator();
            //Act
            var isValidUrl = validator.ValidateSourceString(_validUrl);
            //Assert
            Assert.AreEqual(true, isValidUrl);
        }

        [Test]
        public void It_Is_Not_A_Valid_URL()
        {
            //Arrange
            var validator = new SourceValidator();
            //Act
            var isValidUrl = validator.ValidateSourceString(_notValidURL);
            //Assert
            Assert.AreEqual(false, isValidUrl);
        }

        [Test]
        public void Facate_Gets_Result_From_Valid_URL()
        {
            //Arrange

            var datafacade = new DataFacade(_mockAppConfiguration);
            //Act
            var responseContent = datafacade.GetStringLogsFromURL(_validUrl);
            //Assert
            Assert.IsNotNull(responseContent);
        }

        [Test]
        public void Facate_Returns_NULL_Result_From_Invalid_URL()
        {
            //Arrange
            var datafacade = new DataFacade(_mockAppConfiguration);
            //Act
            var responseContent = datafacade.GetStringLogsFromURL(_notValidURL);
            //Assert
            Assert.IsNull(responseContent);
        }

        [Test]
        public void LogConverter_Gets_Correct_Number_Of_Results()
        {
            //Arrange
            var logConverter = new LogConverter(_mockAppConfiguration);
            //Act
            var result = logConverter.MapLogsFromString(_mockApiResponse);
            //Assert
            Assert.AreEqual(4,result.Count);
        }

        [Test]
        public void LogConverter_Does_Not_Return_Empty_Results()
        {
            //Arrange
            var logConverter = new LogConverter(_mockAppConfiguration);
            //Act
            var result = logConverter.MapLogsFromString(_mockApiResponse);
            //Assert
            Assert.AreEqual(0, result.Where(res=>res.Equals("")).ToList().Count);
        }
    }
}