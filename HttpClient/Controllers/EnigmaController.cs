using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using HttpClient.Models;

namespace HttpClient.Controllers
{
    public class EnigmaController : ControllerBase
    {
        //TODO: move to abstract class

        public EnigmaController(string BaseUrl, string Endpoint, string QueryString)
        {
            // sets properties in the base class
            this.BaseUrl = BaseUrl;
            this.Endpoint = Endpoint;
            this.QueryString = QueryString;
        }

        public async Task<HttpResponseMessage> ExecutePostEncrypt<Tin, Tout>(Tin o)
        {
            return await SendPostRequest<Tin, Tout>(o);
        }

        public async Task<HttpResponseMessage> ExecutePostChooseRotors()
        {
            return await SendPostRequest<string>();
        }

        public async Task<HttpResponseMessage> ExecutePostSetRotor<Tin, Tout>(Tin rotorDto)
        {
            return await SendPostRequest<Tin, Tout>(rotorDto);
        }

        public async Task<HttpResponseMessage> ExecutePostSetPlugboard<Tin, TReturn>(Tin plugboardDto)
        {
            return await SendPostRequest<Tin, TReturn>(plugboardDto);
        }

        public TReturn ExecutePostEncryptStatic<TReturn>(object o)
        {
            // calls SendPOSTRequest in ApiController Base
            throw new NotImplementedException();
        }

    }
}
