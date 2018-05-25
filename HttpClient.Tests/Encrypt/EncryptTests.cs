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

namespace HttpClient.Tests.Encrypt
{
    [TestClass]
    public class EncryptTests
    {
        EnigmaMachineDto enigmaMachineDto = new EnigmaMachineDto()
        {
            MachineName = "foo",
            Text = "Encrypt Me",
            Rotor1 = new RotorDto()
            {
                RotorNum = 1,
                InitialDialSetting = 'a'
            },
            Rotor2 = new RotorDto()
            {
                RotorNum = 2,
                InitialDialSetting = 'b'
            },
            Rotor3 = new RotorDto()
            {
                RotorNum = 3,
                InitialDialSetting = 'c'
            },
            Plugboard = new PlugboardDto()
            {
                Wiring = new Dictionary<char, char>()
                    {   { 'a', 'b' }, { 'r', 'e' } }
            }

        };

        [TestMethod]
        public async Task Encrypt_ShouldReturn200OK_WithBasicDto()
        {
            // Arrange
            var controller = new EnigmaController(
                "http://localhost:61467/",
                "api/Enigma/Encrypt?leaveWhiteSpace=true",
                String.Empty);
            string textToEncrypt = "I am a jelly doughnut";

            EnigmaMachineDto enigmaMachineDto = new EnigmaMachineDto()
            {
                MachineName = "foo",
                Text = textToEncrypt,
                Rotor1 = new RotorDto()
                {
                    RotorNum = 1,
                    InitialDialSetting = 'a'
                },
                Rotor2 = new RotorDto()
                {
                    RotorNum = 2,
                    InitialDialSetting = 'b'
                },
                Rotor3 = new RotorDto()
                {
                    RotorNum = 3,
                    InitialDialSetting = 'c'
                },
                Plugboard = new PlugboardDto()
                {
                    Wiring = new Dictionary<char, char>()
                    {   { 'a', 'b' }, { 'r', 'e' } }
                }

            };

            // Act
            HttpResponseMessage response =
                await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string encryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            // Assert
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
        }

        [TestMethod]
        public async Task Encrypt_ShouldReturnEncryptText_WithBasicDto()
        {
            // Arrange
            var controller = new EnigmaController(
                "http://localhost:61467/",
                "api/Enigma/Encrypt?leaveWhiteSpace=true",
                String.Empty);
            string textToEncrypt = "I am a jelly doughnut";

            enigmaMachineDto.Text = textToEncrypt;

            // Act
            HttpResponseMessage response = 
                await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string encryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            enigmaMachineDto.Text = encryptedText;
            response = await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string DecryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            // Assert
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(textToEncrypt.ToLower(), DecryptedText);
        }

        [TestMethod]
        public async Task Encrypt_ShouldReturnEncryptText_WithNoWireSettings()
        {
            // Arrange
            var controller = new EnigmaController(
                "http://localhost:61467/",
                "api/Enigma/Encrypt?leaveWhiteSpace=true",
                String.Empty);
            string textToEncrypt = "I am a jelly doughnut";

            enigmaMachineDto.Text = textToEncrypt;
            enigmaMachineDto.Plugboard = new PlugboardDto() { };

            //EnigmaMachineDto enigmaMachineDto = new EnigmaMachineDto()
            //{
            //    MachineName = "foo",
            //    Text = textToEncrypt,
            //    Rotor1 = new RotorDto()
            //    {
            //        RotorNum = 1,
            //        InitialDialSetting = 'a'
            //    },
            //    Rotor2 = new RotorDto()
            //    {
            //        RotorNum = 2,
            //        InitialDialSetting = 'b'
            //    },
            //    Rotor3 = new RotorDto()
            //    {
            //        RotorNum = 3,
            //        InitialDialSetting = 'c'
            //    },
            //    Plugboard = new PlugboardDto()
            //    {
            //        Wiring = new Dictionary<char, char>() { }
            //    }

            //};

            // Act
            HttpResponseMessage response =
                await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string encryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            enigmaMachineDto.Text = encryptedText;
            response = await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string DecryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            // Assert
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(textToEncrypt.ToLower(), DecryptedText);
        }

        [TestMethod]
        public async Task Encrypt_ShouldReturnEncryptText_WithNoWire10Settings()
        {
            // Arrange
            var controller = new EnigmaController(
                "http://localhost:61467/",
                "api/Enigma/Encrypt?leaveWhiteSpace=true",
                String.Empty);
            string textToEncrypt = "attack at dawn";

            enigmaMachineDto.Text = textToEncrypt;
            enigmaMachineDto.Plugboard = new PlugboardDto()
            {
                Wiring = new Dictionary<char, char>()
                    {   { 'a', 'b' }, { 'r', 'e' }, { 'c', 'd'}, { 'f', 'g'}, { 'h', 'i'},
                        { 'j', 'k' }, { 'l', 'm' }, { 'n', 'o'}, { 'p', 'q'}, { 's', 't'} }
            };



            // Act
            HttpResponseMessage response =
                await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string encryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            enigmaMachineDto.Text = encryptedText;
            response = await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string DecryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            // Assert
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(textToEncrypt.ToLower(), DecryptedText);
        }

        [TestMethod]
        public async Task Encrypt_ShouldReturnEncryptText_WithNoWire10Settings_AndNoWhiteSpace()
        {
            // Arrange
            var controller = new EnigmaController(
                "http://localhost:61467/",
                "api/Enigma/Encrypt?leaveWhiteSpace=false",
                String.Empty);
            string textToEncrypt = "Where is my bmw";

            enigmaMachineDto.Text = textToEncrypt;
            enigmaMachineDto.Plugboard = new PlugboardDto()
            {
                Wiring = new Dictionary<char, char>()
                    {   { 'a', 'b' }, { 'r', 'e' }, { 'c', 'd'}, { 'f', 'g'}, { 'h', 'i'},
                        { 'j', 'k' }, { 'l', 'm' }, { 'n', 'o'}, { 'p', 'q'}, { 's', 't'} }
            };

            // Act
            HttpResponseMessage response =
                await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string encryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            enigmaMachineDto.Text = encryptedText;
            response = await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string DecryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            // Assert
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(textToEncrypt.ToLower().Replace(" ", ""), DecryptedText);
        }

        [TestMethod]
        public async Task Encrypt_ShouldReturnEncryptText_With10Wire10Settings_AndPunctuation()
        {
            // Arrange
            var controller = new EnigmaController(
                "http://localhost:61467/",
                "api/Enigma/Encrypt?leaveWhiteSpace=false",
                String.Empty);
            string textToEncrypt = "Where's my bmw";

            enigmaMachineDto.Text = textToEncrypt;
            enigmaMachineDto.Plugboard = new PlugboardDto()
            {
                Wiring = new Dictionary<char, char>()
                    {   { 'a', 'b' }, { 'r', 'e' }, { 'c', 'd'}, { 'f', 'g'}, { 'h', 'i'},
                        { 'j', 'k' }, { 'l', 'm' }, { 'n', 'o'}, { 'p', 'q'}, { 's', 't'} }
            };

            // Act
            HttpResponseMessage response =
                await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string encryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            enigmaMachineDto.Text = encryptedText;
            response = await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string DecryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            // Assert
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(textToEncrypt.ToLower().Replace(" ", "").Replace("'", ""), DecryptedText);
        }

        [TestMethod]
        public async Task Encrypt_ShouldReturnEncryptText_WithRotors523()
        {
            // Arrange
            var controller = new EnigmaController(
                "http://localhost:61467/",
                "api/Enigma/Encrypt?leaveWhiteSpace=false",
                String.Empty);
            string textToEncrypt = "Attack at Dawn";

            enigmaMachineDto.Text = textToEncrypt;
            enigmaMachineDto.Rotor1 = new RotorDto { RotorNum = 5, InitialDialSetting = 'k' };
            enigmaMachineDto.Plugboard = new PlugboardDto()
            {
                Wiring = new Dictionary<char, char>()
                    {   { 'a', 'b' }, { 'r', 'e' }, { 'c', 'd'}, { 'f', 'g'}, { 'h', 'i'},
                        { 'j', 'k' }, { 'l', 'm' }, { 'n', 'o'}, { 'p', 'q'}, { 's', 't'} }
            };

            // Act
            HttpResponseMessage response =
                await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string encryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            enigmaMachineDto.Text = encryptedText;
            response = await controller.ExecutePostEncrypt<EnigmaMachineDto, HttpResponseMessage>(enigmaMachineDto);
            string DecryptedText = (await response.Content.ReadAsStringAsync()).Replace("\"", "");

            // Assert
            HttpStatusCode statusCode = response.StatusCode;
            Assert.AreEqual(textToEncrypt.ToLower().Replace(" ", "").Replace("'", ""), DecryptedText);
        }
    }
}
