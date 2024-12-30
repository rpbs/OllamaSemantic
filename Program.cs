using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.TextGeneration;
using OllamaSemanticApi.Models;
using OllamaSemanticApi.Plugins;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var kernelBuilder = Kernel.CreateBuilder();
var modelId = "llama3.2";
var endpoint = new Uri("http://localhost:11434");

#pragma warning disable SKEXP0070

kernelBuilder.Services.AddOllamaChatCompletion(modelId, endpoint);

#pragma warning restore SKEXP0070

kernelBuilder.Plugins
    .AddFromType<ClassesPlugin>()
    .AddFromType<StudentsPlugin>()
    .AddFromType<TeachersPlugin>()
    .AddFromType<StudentClassPlugin>();

kernelBuilder.Services.AddDbContext<DatabaseTestContext>();

var kernel = kernelBuilder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

#pragma warning disable SKEXP0070
var settings = new OllamaPromptExecutionSettings { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };
#pragma warning restore SKEXP0070

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "We are on").WithName("index");

app.MapGet("/ask", async (string request) =>
{
    var response = await chatCompletionService.GetChatMessageContentsAsync(request, settings, kernel);
    return response;
}).WithName("Ask");


app.Run();

