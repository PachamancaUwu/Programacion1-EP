using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace examenparcial.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Método para obtener el precio del Bitcoin en USD
        public async Task<decimal> ObtenerPrecioBitcoinUSD()
        {
            string apiUrl = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd";
            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode(); // Asegura que la respuesta sea exitosa
            var jsonString = await response.Content.ReadAsStringAsync();

            // Deserializar el JSON y obtener el precio de Bitcoin en USD
            var json = JObject.Parse(jsonString);
            var bitcoinPrice = json["bitcoin"]["usd"].Value<decimal>();

            return bitcoinPrice; // Retornar el precio de Bitcoin en dólares
        }
    }
}