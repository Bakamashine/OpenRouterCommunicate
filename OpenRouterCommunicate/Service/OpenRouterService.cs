using OpenRouterCommunicate.Serilization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Microsoft.Extensions.Options;
using dotenv.net;
using dotenv.net.Utilities;

namespace OpenRouterCommunicate.Service
{
    public class OpenRouterService
    {
        private string ApiKey { set; get; }

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
            this.ApiKey = EnvReader.GetStringValue("key");
            Console.WriteLine($"OpenRouterServiceKey: {ApiKey}");
        }
        public async Task<ChatCompletionResponse?> SendPrompt([FromBody] string message)
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
            request.Content = new StringContent(JsonSerializer.Serialize(prompt));

            var response = await Client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            ChatCompletionResponse? result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();
            return result;
        }
    }
}
