using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpClient.Models;
using HttpClient.Controllers;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace HttpClient.Tests.ChooseRotors
{
    [TestClass]
    public class ChooseRotors
    {
        [DataRow("123")]
        [DataRow("135")]
        [DataRow("543")]
        [DataRow("514")]
        [DataTestMethod]
        public async Task ChooseRotors_ShouldReturn200Ok_WithBasicRequest(string rotor)
        {
            // Arrange
            var controller = new EnigmaController(
                "http://localhost:61467/", 
                "api/Enigma/ChooseRotors",
                "rotors=" + rotor);

            // Act
            var response = await controller.ExecutePostChooseRotors();

            // Assert
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [DataRow("1234")]
        [DataRow("")]
        [DataRow("5")]
        [DataRow("326")]
        [DataTestMethod]
        public async Task ChooseRotors_ShouldReturn400_WithInvalidRotors(string rotor)
        {
            // Arrange
            var controller = new EnigmaController(
                "http://localhost:61467/",
                "api/Enigma/ChooseRotors",
                "rotors=" + rotor);

            // Act
            var response = await controller.ExecutePostChooseRotors();

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
