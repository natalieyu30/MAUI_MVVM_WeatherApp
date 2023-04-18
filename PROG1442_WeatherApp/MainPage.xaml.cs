using PROG1442_WeatherApp.Models;
using PROG1442_WeatherApp.Services;
using PROG1442_WeatherApp.ViewModel;

namespace PROG1442_WeatherApp;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

