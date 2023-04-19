using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PROG1442_WeatherApp.ViewModel;

public partial class BaseViewModel :ObservableObject
{
    public BaseViewModel() {}

    [ObservableProperty]
    bool isBusy;

    [ObservableProperty]
    string location;

    [ObservableProperty]
    string cityName;

    [ObservableProperty]
    string countryName;

    [ObservableProperty]
    string weatherImage;

    [ObservableProperty]
    string weatherCondition;

    [ObservableProperty]
    string temp;

    [ObservableProperty]
    string feelslikeTemp;
    public bool IsNotBusy => !IsBusy;

}
