using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZebraBarcodeScannerSDK;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Viewmodel for the main page, managing Zebra scanner detection, connection, and user interaction logic.
/// </summary>
/// <remarks>
/// MainViewModel coordinates scanner discovery, connection management, and event tracking for Zebra
/// scanners. It exposes properties and commands for UI binding, and handles permission checks, event subscriptions, and
/// scanner state updates.
/// </remarks>
public partial class MainViewModel : ObservableObject
{
    private readonly IZebraScannerManager scannerManager;
    private readonly IPopupService popupService;

    public MainViewModel(IZebraScannerManager scannerManager, IPopupService popupService)
    {
        //Init
        this.scannerManager = scannerManager;
        this.popupService = popupService;
    }

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ShowPairingDialogCommand))]
    private bool isBusy;

    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    private ObservableDictionary<int, IZebraScanner>? scanners;

    [ObservableProperty]
    private string scannerEvents = string.Empty;

    public async Task OnAppearingAsync()
    {
        //Ensure Permissions is Granted!
        var status = await Permissions.CheckStatusAsync<Permissions.Bluetooth>();
        if (status != PermissionStatus.Granted)
        {
            status = await Permissions.RequestAsync<Permissions.Bluetooth>();
        }

        //Subscribe to Scanner Detection Events
        scannerManager.Appeared += Scanner_Appeared;
        scannerManager.BarcodeScanned += Scanner_BarcodeScanned;
        scannerManager.Connected += Scanner_Connected;
        scannerManager.Disappeared += Scanner_Disappeared;
        scannerManager.Disconnected += Scanner_Disconnected;

        //Enable Scanner Detection
        scannerManager.EnableDetection();

        //Get Connected Scanners
        Scanners = [.. await scannerManager.GetScanners()];
    }

    public Task OnDisappearingAsync()
    {
        //Disable Scanner Detection
        scannerManager.DisableDetection();

        //Unsubscribe from Scanner Detection Events
        scannerManager.Appeared -= Scanner_Appeared;
        scannerManager.BarcodeScanned -= Scanner_BarcodeScanned;
        scannerManager.Connected -= Scanner_Connected;
        scannerManager.Disappeared -= Scanner_Disappeared;
        scannerManager.Disconnected -= Scanner_Disconnected;

        //Return Completed Task
        return Task.CompletedTask;
    }

    private void Scanner_Disconnected(object? sender, int id)
    {
        //Add Scanner Event
        ScannerEvents +=
            $"Scanner Disconnected, Id:{id}{Environment.NewLine}" +
            $"*********************************{Environment.NewLine}";

        //Update Scanner Connected Status
        if (Scanners?.TryGetValue(id, out var scanner) ?? false)
            scanner.IsConnected = false;
    }

    private void Scanner_Disappeared(object? sender, int id)
    {
        //Add Scanner Event
        ScannerEvents +=
            $"Scanner Disappeared, Id:{id}{Environment.NewLine}" +
            $"*********************************{Environment.NewLine}";

        //Update Scanner Connected Status
        Scanners?.Remove(id);
    }

    private void Scanner_Connected(object? sender, IZebraScanner scanner)
    {
        //Add Scanner Event
        ScannerEvents +=
            $"Scanner Connected, Id:{scanner.Id}, Name:{scanner.Name}{Environment.NewLine}" +
            $"*********************************{Environment.NewLine}";

        //Add/Update to list of scanners
        Scanners ??= new();
        scanner.IsConnected = true;
        Scanners[scanner.Id] = scanner;
    }

    private void Scanner_BarcodeScanned(object? sender, BarcodeData barcodeData)
    {
        //Add Scanner Event
        ScannerEvents +=
            $"Barcode Scanned{Environment.NewLine}" +
            $"Type: {barcodeData.Type}{Environment.NewLine}" +
            $"Data: {barcodeData.Barcode}{Environment.NewLine}" +
            $"*********************************{Environment.NewLine}";
    }

    private void Scanner_Appeared(object? sender, IZebraScanner scanner)
    {
        //Add Scanner Event
        ScannerEvents +=
            $"Scanner Appeared, Id:{scanner.Id}, Name:{scanner.Name}{Environment.NewLine}" +
            $"*********************************{Environment.NewLine}";

        //Add/Update to list of scanners
        Scanners ??= new();
        Scanners[scanner.Id] = scanner;
    }

    [RelayCommand(CanExecute = nameof(IsNotBusy))]
    public async Task ToggleConnection(IZebraScanner scanner)
    {
        //Set Busy
        IsBusy = true;

        //Add Scanner Event
        var action = scanner.IsConnected ? "Disconnecting" : "Connecting";
        ScannerEvents +=
            $"{action} Scanner, Id:{scanner.Id}, Name:{scanner.Name}{Environment.NewLine}" +
            $"*********************************{Environment.NewLine}";

        //Attempt to Connect/Disconnect Scanner
        if (scanner.IsConnected)
            await scanner.Disconnect();
        else
            await scanner.Connect();

        //Set Not Busy
        IsBusy = false;
    }

    [RelayCommand(CanExecute = nameof(IsNotBusy))]
    public async Task ShowPairingDialog()
    {
        //Show Pairing Dialog
        var pairingResult = await popupService.ShowPopupAsync<ScannerPairingPopupView, IZebraScanner>(Shell.Current);

        //Add Scanner Event
        if (pairingResult?.Result is IZebraScanner scanner)
        {
            ScannerEvents +=
            $"Scanner Paired, Id:{scanner.Id}, Name:{scanner.Name}{Environment.NewLine}" +
            $"*********************************{Environment.NewLine}";

            //Add/Update to list of scanners
            Scanners ??= new();
            scanner.IsConnected = true;
            Scanners[scanner.Id] = scanner;
        }
    }

}
