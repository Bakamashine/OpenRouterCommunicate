using OpenRouterCommunicate.Serilization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace OpenRouterCommunicate.Service
{
    public class OpenRouterService
    {
        private string ApiKey { set; get; } = "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";

        private readonly string Url = "https://openrouter.ai/api/v1/chat/completions";
        private readonly string Model = "xiaomi/mimo-v2-flash:free";


        private readonly HttpClientHandler handler = new()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, slPolicyErrors) => true
        };

        // private readonly HttpClient client = new HttpClient(handler);
        private readonly HttpClient Client;
        public OpenRouterService(HttpClient client)
        {
            this.Client = client;
        }
        public async Task SendPrompt([FromBody] string message)
        {
            // HttpClient client = new HttpClient(this.handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", this.ApiKey);

            var requestUser = new List<Rule>
        {
            new() {role = "user", content = message}
        };
            var prompt = new Prompt(this.Model) { messages = requestUser };
            request.Content = new StringContent(JsonSerializer.Serialize(prompt), System.Text.Encoding.UTF8, "application/json");

            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);
            //return null;
        }
    }
}
