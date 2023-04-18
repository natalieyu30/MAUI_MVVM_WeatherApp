using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PROG1442_WeatherApp.Models;
using PROG1442_WeatherApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG1442_WeatherApp.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    WeatherService weatherService;
    public ObservableCollection<Forecastday> Forecastdays { get; } = new();
    public ObservableCollection<Hour> ForecastPeriod { get; } = new();
    public MainViewModel(WeatherService weatherService) 
    {
        Location = "Niagara Falls";
        this.weatherService = weatherService;
    }

    [RelayCommand]
    async Task GetWeatherAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            var weatherData = await weatherService.GetWeatherData(Location);


            CityName = weatherData.location.name + ", " + weatherData.location.country;
            WeatherImage = "https:" + weatherData.current.condition.icon;
            WeatherCondition = weatherData.current.condition.text;
            Temp = Math.Round(weatherData.current.temp_c).ToString() + "°C";
            FeelslikeTemp = "Feels like " + Math.Round(weatherData.current.feelslike_c).ToString() + "°C";

            // forecast for next 6, 12, 18 hours
            //ForecastPeriod.Clear();
            //foreach (var item in weatherData.forecast.forecastday[0].hour)
            //{
            //    ForecastPeriod.Add(item);
            //}

            for (int i = 5; i < 18; i += 6)
            {
                ForecastPeriod.Add(weatherData.forecast.forecastday[0].hour[i]);
            }

            // forecast for next three days
            Forecastdays.Clear();
            for (int i = 1; i <= 3; i++)
            {
                Forecastdays.Add(weatherData.forecast.forecastday[i]);
            }

            //foreach (var item in weatherData.forecast.forecastday)
            //{
            //    Forecastdays.Add(item);
            //}
            Location = "";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get weather: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
