using PROG1442_WeatherApp.ViewModel;

namespace PROG1442_WeatherApp;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}