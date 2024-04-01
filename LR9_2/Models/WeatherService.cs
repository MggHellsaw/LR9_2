using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class WeatherService
{
    private readonly HttpClient _httpClient;

    public WeatherService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<WeatherResponse> GetWeatherAsync(double lat, double lon)
    {
        string apiKey = "a884dace1617d4cf8d11f4848a5b72f9";
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={apiKey}&units=metric";

        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            WeatherResponse weather = JsonConvert.DeserializeObject<WeatherResponse>(json);
            return weather;
        }
        else
        {
            throw new Exception("Failed to retrieve weather data");
        }
    }
}

public class WeatherResponse
{
    [JsonProperty("main")]
    public WeatherInfo Main { get; set; }
}

public class WeatherInfo
{
    [JsonProperty("temp")]
    public double Temperature { get; set; }
}
