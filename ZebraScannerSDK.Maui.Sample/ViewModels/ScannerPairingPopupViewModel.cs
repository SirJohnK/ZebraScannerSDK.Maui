using AsyncAwaitBestPractices;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ZebraBarcodeScannerSDK;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Viewmodel for the scanner pairing popup, providing properties and commands to facilitate pairing
/// Zebra scanners via barcode and Bluetooth MAC address entry.
/// </summary>
/// <remarks>
/// Manages the workflow for pairing Zebra scanners, including handling Bluetooth MAC
/// address requirements, displaying pairing barcodes, and responding to scanner connection events. It exposes commands
/// for updating the Bluetooth address and canceling the pairing process. The view model subscribes to scanner events
/// when loaded and ensures proper cleanup when unloaded.</remarks>
/// <param name="scannerManager">The scanner manager used to control scanner detection and retrieve pairing barcodes.</param>
/// <param name="popupService">The popup service used to display dialogs and manage popup interactions.</param>
public partial class ScannerPairingPopupViewModel(IZebraScannerManager scannerManager, IPopupService popupService) : ObservableObject
{
    private const string BluetoothMacAddress = nameof(BluetoothMacAddress);

    [ObservableProperty]
    private string? macAddress;

    [ObservableProperty]
    private ImageSource? pairingBarcode;

    public bool IsMacAddressRequired => scannerManager.IsMacAddressRequired;

    public ImageSource SetFactoryDefaultsBarcode => ImageSource.FromStream(() => ScannerSDK.SetFactoryDefaultsBarcode);
    public ImageSource HostTriggerEventModeEnabledBarcode => ImageSource.FromStream(() => ScannerSDK.HostTriggerEventModeEnabledBarcode);
    public ImageSource HostTriggerEventModeDisabledBarcode => ImageSource.FromStream(() => ScannerSDK.HostTriggerEventModeDisabledBarcode);

    private async Task<string?> GetBluetoothMacAddress(string? inputAddress = null, bool forceDialog = false)
    {
        //Init
        var macAddress = Preferences.Default.Get<string?>(BluetoothMacAddress, null);

        //Attempt to get mac address from settings
        if (forceDialog || macAddress is null)
        {
            //Request Bluetooth Mac Address from User'
            macAddress = (await popupService.ShowPopupAsync<BluetoothAddressPopupView, string?>(Shell.Current, shellParameters: new Dictionary<string, object>() { ["Input"] = macAddress! }))?.Result;
        }

        //Store Address in Settings
        if (!string.IsNullOrWhiteSpace(macAddress))
        {
            macAddress = macAddress.Replace(":", string.Empty);
            Preferences.Set(BluetoothMacAddress, macAddress);
        }

        //Return Address
        return macAddress;
    }

    public async Task OnLoadedAsync()
    {
        //Enable Detection
        scannerManager.EnableDetection();

        //Subscribe to Scanner Events
        scannerManager.Connected += ScannerConnected;

        //Check if Bluetooth Mac Address is required
        if (scannerManager.IsMacAddressRequired)
        {
            //Get Bluetooth Address
            var macAddress = await GetBluetoothMacAddress();
            if (macAddress is not null) MacAddress = macAddress;
        }

        //Get Pairing Barcode
        if (!scannerManager.IsMacAddressRequired || !string.IsNullOrWhiteSpace(MacAddress))
            PairingBarcode = scannerManager.GetPairingBarcode(MacAddress);
    }

    private void ScannerConnected(object? sender, IZebraScanner scanner)
    {
        popupService.ClosePopupAsync(Shell.Current, scanner).SafeFireAndForget();
    }

    public Task OnUnloadedAsync()
    {
        //Clean up
        scannerManager.Connected -= ScannerConnected;
        scannerManager.DisableDetection();

        return Task.CompletedTask;
    }

    /// <summary>
    /// Dialog CANCEL button command to close dialog. (Overrideable)
    /// </summary>
    /// <remarks>Will close dialog with <see langword="false"/> value respons.</remarks>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    [RelayCommand]
    public Task CancelButton() => popupService.ClosePopupAsync(Shell.Current);

    [RelayCommand]
    public async Task UpdateBluetoothAddress()
    {
        //Get Bluetooth Address
        var macAddress = await GetBluetoothMacAddress(MacAddress, true);
        if (macAddress is not null)
        {
            //Update Mac Address
            MacAddress = macAddress;

            //Get Pairing Barcode
            PairingBarcode = scannerManager.GetPairingBarcode(MacAddress);
        }
    }
}