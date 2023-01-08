using FlightControl.Models;
using System.Text;
using System.Text.Json;

Random random = new();
var Url = "https://localhost:5001/api/airport";

var airlines = new string[] { "Qatar Airways", "Singapore Airlines", "Emirates", "ANA All Nippon Airways", "Qantas Airways", "Japan Airlines", "Turkish Airlines", "Air France", "Korean Air", "Swiss International Air Lines" };
var cities = new string[] { "Paris", "Maui", "London", "Rome", "Tokyo", "Maldives", "Barcelona", "New York", "Sydney", "Dubai", "Amsterdam", "Vancouver", "Prague" };

while (true)
{
    await Task.Run(async () =>
    {
        using var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Post, Url);
        var flight = new Flight
        {
            Airline = airlines[random.Next(0, airlines.Length)],
            Airport = cities[random.Next(0, cities.Length)],
            //Target=Target.Arrival
            Target = (Target)random.Next(1, 3)
        };
        var json = JsonSerializer.Serialize(flight);
        try
        {
            using var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            request.Content = stringContent;
            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(response.StatusCode.ToString());
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        await Task.Delay(random.Next(10, 2000));
    });
}
