using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HttpClient.Models;
using HttpClient.Controllers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace HttpClient.Tests.SetPlugboard
{
    [TestClass]
    public class SetPluboardTests
    {
        [TestMethod]
        public async Task SetPlugboard_ShouldReturn200Ok_WhenSettingAWiring()
        {
            // Arrange
            var controller = new EnigmaController(
                "http://localhost:61467/",
                "api/Enigma/SetPlugboard",
                String.Empty);

            PlugboardDto plugboardDto = new PlugboardDto()
            {
                Wiring = new Dictionary<char, char>(){ { 'a', 'b'} }
            };

            // Act
            HttpResponseMessage response = 
                await controller.ExecutePostSetPlugboard<PlugboardDto, HttpResponseMessage>(plugboardDto);

            // Assert
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
        }
    }
}
