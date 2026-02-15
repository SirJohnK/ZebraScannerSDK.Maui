using AsyncAwaitBestPractices;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Main view of the application for demonstrating the Zebra Scanner SDK functionality.
/// </summary>
public partial class MainView : ContentPage
{
    private readonly MainViewModel viewModel;

    public MainView(MainViewModel viewModel)
    {
        //Init
        this.viewModel = viewModel;
        BindingContext = viewModel;

        //Initialize
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        //Call base
        base.OnAppearing();

        //Notify ViewModel
        viewModel.OnAppearingAsync().SafeFireAndForget();
    }

    protected override void OnDisappearing()
    {
        //Call base
        base.OnDisappearing();

        //Notify ViewModel
        viewModel.OnDisappearingAsync().SafeFireAndForget();
    }
}