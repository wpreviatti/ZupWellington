﻿using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ClientAdministrationView.Utils
{
    public class APIRequisicao : ControllerBase
    {
        public static HttpClient Client = null;
        public static IConfiguration _configuration;
        public APIRequisicao(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static void ConfigRequest(string urlApi)
        {
            //Uri UrlServiceLayer = new Uri(@"https://localhost:44390/api/");
            Uri UrlServiceLayer = new Uri(urlApi);
            HttpClientHandler httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            Client = new HttpClient(httpClientHandler);
            Client.BaseAddress = UrlServiceLayer;
            Client.DefaultRequestHeaders.ExpectContinue = false;
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Add("Cache-Control", "no-cache, no-store, max-age=0, must-revalidate");
        }

        public static HttpResponseMessage SendRequest(HttpMethod metodo, string url, object objeto = null)
        {
            HttpResponseMessage resposta = new HttpResponseMessage();
            HttpRequestMessage request = new HttpRequestMessage(metodo, url);
            if (objeto != null)
                request.Content = new StringContent(JsonConvert.SerializeObject(objeto,Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore}), Encoding.UTF8, "application/json");

            Task.Run(async () =>
            {
                try
                {
                    resposta = await Client.SendAsync(request);
                    string retornoRequis = await resposta.Content.ReadAsStringAsync();
                }
                catch (Exception er)
                {
                    Client = null;
                    throw er;
                }

            }).Wait();

            return resposta;

        }
    }
}
