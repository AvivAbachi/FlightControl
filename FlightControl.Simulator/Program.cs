using FlightControl.Models;
using System.Text;
using System.Text.Json;

Random random = new Random();
var Url = "http";

while (true)
{
    await Task.Run(async () =>
    {
        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, Url);
        var flight = new Flight { Target = (Target)random.Next(1, 3) };
        var json = JsonSerializer.Serialize(flight);
        try
        {
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = stringContent;
            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        await Task.Delay(random.Next(5000, 10000));
    });
}