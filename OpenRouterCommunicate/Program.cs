using Microsoft.AspNetCore.Mvc;
using OpenRouterCommunicate.Service;
using OpenRouterCommunicate.Request;
using dotenv.net;
using OpenRouterCommunicate.Serilization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DotEnv.Load();

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.Configure<OpenRouterService>(builder.Configuration.GetSection("OpenRouter"));
builder.Services.AddHttpClient<OpenRouterService>(httpClient =>
{
    httpClient.Timeout = TimeSpan.FromSeconds(500);
});
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

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run("http://localhost:5018");
