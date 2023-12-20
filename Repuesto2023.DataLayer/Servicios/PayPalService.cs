using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Repuestos2023.DataLayer.Servicios.Interfaces;

public class PayPalService : IPayPalService
{
    private readonly IConfiguration _configuration;
    private readonly string _paypalApiBaseUrl = "https://api.paypal.com/v2/checkout/orders";

    public PayPalService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> CreateOrder(double orderTotal)
    {
        var clientId = _configuration["PayPal:ClientId"];
        var clientSecret = _configuration["PayPal:ClientSecret"];

        var accessToken = await GetPayPalAccessToken(clientId, clientSecret);

        var orderRequest = new
        {
            intent = "CAPTURE",
            purchase_units = new List<object>
            {
                new
                {
                    amount = new
                    {
                        currency_code = "USD",
                        value = orderTotal.ToString("F2"),
                    }
                }
            }
        };

        var jsonOrderRequest = JsonConvert.SerializeObject(orderRequest);

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var response = await client.PostAsync(_paypalApiBaseUrl, new StringContent(jsonOrderRequest, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var orderResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);

            // Obtén la URL de aprobación de PayPal desde la respuesta
            return orderResponse.links[1].href;
        }
    }

    private async Task<string> GetPayPalAccessToken(string clientId, string clientSecret)
    {
        var tokenEndpoint = "https://api.paypal.com/v1/oauth2/token";
        var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));

        using (var client = new HttpClient())
        {
            var data = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

            client.DefaultRequestHeaders.Add("Authorization", $"Basic {credentials}");

            var response = await client.PostAsync(tokenEndpoint, new FormUrlEncodedContent(data));

            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);

            return tokenResponse.access_token;
        }
    }

    public async Task<bool> CapturePayment(string orderId)
    {
        var clientId = _configuration["PayPal:ClientId"];
        var clientSecret = _configuration["PayPal:ClientSecret"];

        var accessToken = await GetPayPalAccessToken(clientId, clientSecret);

        var captureEndpoint = $"https://api.paypal.com/v2/checkout/orders/{orderId}/capture";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");

            var response = await client.PostAsync(captureEndpoint, null);

            response.EnsureSuccessStatusCode();

            // Si llegamos aquí, la captura fue exitosa
            return true;
        }
    }

}
