using Microsoft.AspNetCore.Mvc;
using OpenRouterCommunicate.Service;
using OpenRouterCommunicate.Request;
using dotenv.net;
using OpenRouterCommunicate.Serilization;
using System.Net.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DotEnv.Load();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient<OpenRouterService>(httpClient =>
{
    httpClient.Timeout = TimeSpan.FromSeconds(500);
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (sender, cert, chain, slPolicyErrors) => true,
        SslProtocols = System.Security.Authentication.SslProtocols.Tls12 |  System.Security.Authentication.SslProtocols.Tls13,
        CheckCertificateRevocationList = false,
        UseProxy = false,
        UseCookies = false
    }
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/prompt", async (OpenRouterService service, [FromForm] TextRequest request) =>
{
    ChatCompletionResponse? result =  await service.SendPrompt(request.text);
    if (result != null)
    {
        string message = result.choices[0].message.content;
        Console.WriteLine(message);
        return message;
    }
    return null;
}).DisableAntiforgery();


app.Run("http://localhost:5018");
