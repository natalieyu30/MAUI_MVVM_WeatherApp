using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PROG1442_WeatherApp.Models;
using PROG1442_WeatherApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG1442_WeatherApp.ViewModel;

public partial class MainViewModel : BaseViewModel
{
    WeatherService weatherService;
    public ObservableCollection<Hour> ForecastPeriod { get; } = new();
    public ObservableCollection<Forecastday> Forecastdays { get; } = new();

    IConnectivity connectivity;
    public MainViewModel(WeatherService weatherService, IConnectivity connectivity) 
    {
        this.weatherService = weatherService;
        this.connectivity = connectivity;
        GetCurrentLocation();
    }

    [RelayCommand]
    async Task GetCurrentLocation()
    {
        var currentLocation = await Geolocation.GetLocationAsync();
        double latitude = currentLocation.Latitude;
        double longitude = currentLocation.Longitude;
        Location = latitude.ToString() + "," + longitude.ToString();
        await GetWeatherAsync();
    }

    [RelayCommand]
    async Task GoToDetailsAsync(Forecastday forecastday)
    {
        if (forecastday is null) return;

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, 
            new Dictionary<string, object>
            {
                {"Forecastday", forecastday },
                { "CityName", CityName}
            });
    }

    [RelayCommand]
    async Task GetWeatherAsync()
    {
        if (IsBusy) return;

        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Internet Connection", $"Check your internet and try again.", "OK");
                return;
            }

            IsBusy = true;
            var weatherData = await weatherService.GetWeatherData(Location);


            CityName = weatherData.location.name + ", " + weatherData.location.region;
            CountryName = weatherData.location.country;
            WeatherImage = "https:" + weatherData.current.condition.icon;
            WeatherCondition = weatherData.current.condition.text;
            Temp = Math.Round(weatherData.current.temp_c).ToString() + "°C";
            FeelslikeTemp = "Feels like " + Math.Round(weatherData.current.feelslike_c).ToString() + "°C";

            // forecast for next 4, 8, 12, 16, 20 hours
            ForecastPeriod.Clear();
            for (int i = 4; i < 24; i += 4)
            {
                ForecastPeriod.Add(weatherData.forecast.forecastday[0].hour[i]);
            }

            // forecast for next three days
            Forecastdays.Clear();
            for (int i = 0; i < 3; i++)
            {
                Forecastdays.Add(weatherData.forecast.forecastday[i]);
            }
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
