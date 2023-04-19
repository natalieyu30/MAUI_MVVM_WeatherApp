using Newtonsoft.Json;
using PROG1442_WeatherApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PROG1442_WeatherApp.Services;

public class WeatherService
{
    HttpClient httpClient;
    Root weatherList = new();

    public WeatherService()
    {
        httpClient = new HttpClient();
    }

    public async Task<Root> GetWeatherData(string place)
    {
        var url = "https://api.weatherapi.com/v1/forecast.json";
        var response = await httpClient.GetAsync($"{url}?key=05478338a666499f80a113410230504&q={place}&days=3&aqi=no&alerts=no");
        
        if (response.IsSuccessStatusCode)
        {
            weatherList = await response.Content.ReadFromJsonAsync<Root>();
        }
        else
        {
            await Shell.Current.DisplayAlert("Error!", $"Unable to get weather", "OK");
        }
        return weatherList;
    }
}

