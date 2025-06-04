using Microsoft.Extensions.AI;
using System.ComponentModel;

var ollamaEndpoint = "http://192.168.1.251:11434";
var chatModel = "llama3.2";

IChatClient client = new OllamaChatClient(
    endpoint: ollamaEndpoint,
    modelId: chatModel)
    .AsBuilder()
    .UseFunctionInvocation()
    .Build();

ChatOptions options = new ChatOptions
{
    Tools = [
        AIFunctionFactory.Create(GetTheWeather)
    ]    
};

// var question = "Solve 2+2. Provide an accurate and short answer";
// Console.WriteLine($"question: {question}");
// var response = await client.GetResponseAsync(question, options);
// Console.WriteLine($"response: {response}");

Console.WriteLine();

var question = "Do I need an umbrella today?. Answer simply with 'yes' or 'no'. If you don't know, say 'I don't know'";
Console.WriteLine($"question: {question}");
var response = await client.GetResponseAsync(question, options);
Console.WriteLine($"response: {response}");



[Description("Get the weather")]
static string GetTheWeather()
{
    Console.WriteLine("\tGetTheWeather function invoked.");

    var temperature = Random.Shared.Next(5, 20);
    var conditions = Random.Shared.Next(0, 4) <= 1 ? "sunny" : "rainy";
    var weather = $"The weather is {temperature} degrees C and {conditions}.";
    Console.WriteLine($"\tGetTheWeather result: {weather}.");
    return weather;
}