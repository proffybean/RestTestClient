using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpClient.Models;
using HttpClient.Controllers;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace HttpClient.Tests.SetRotor
{
    [TestClass]
    public class SetRotorTests
    {
        [TestMethod]
        public async Task SetRotor_ShouldReturn200Ok_WhenSettingRotor2ToZ()
        {
            // Arrange
            var controller = new EnigmaController("http://localhost:61467/", "api/Enigma/SetRotor/1", null);
            RotorDto rotorDto = new RotorDto()
            {
                RotorNum = 1,
                InitialDialSetting = 'a'
            };

            // Act
            HttpResponseMessage response = 
                await controller.ExecutePostSetRotor<RotorDto, HttpResponseMessage>(rotorDto);

            // Assert
            HttpStatusCode statusCode = response.StatusCode;
            string encryptedText = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
        }
    }
}
