using AsyncAwaitBestPractices;
using CommunityToolkit.Maui.Views;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Popup view that facilitates pairing with a Zebra scanner device.
/// </summary>
/// <remarks>
/// Displays and manages the scanner pairing workflow within the application.
/// </remarks>
public partial class ScannerPairingPopupView : Popup<IZebraScanner>
{
    private readonly ScannerPairingPopupViewModel viewModel;

    public ScannerPairingPopupView(ScannerPairingPopupViewModel viewModel)
    {
        //Init
        InitializeComponent();
        BindingContext = viewModel;
        this.viewModel = viewModel;
        Loaded += PopupView_Loaded;
        Unloaded += PopupView_Unloaded;
    }

    private void PopupView_Unloaded(object? sender, EventArgs e)
    {
        Unloaded -= PopupView_Unloaded;
        viewModel.OnUnloadedAsync().SafeFireAndForget();
    }

    private void PopupView_Loaded(object? sender, EventArgs e)
    {
        Loaded -= PopupView_Loaded;
        viewModel.OnLoadedAsync().SafeFireAndForget();
    }
}