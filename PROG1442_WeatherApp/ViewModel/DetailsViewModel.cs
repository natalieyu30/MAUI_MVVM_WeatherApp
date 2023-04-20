using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PROG1442_WeatherApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROG1442_WeatherApp.ViewModel;

[QueryProperty("Forecastday", "Forecastday")]
[QueryProperty("CityName", "CityName")]
public partial class DetailsViewModel : BaseViewModel
{
    public DetailsViewModel() 
    {
    }

    [ObservableProperty]
    Forecastday forecastday;

}
