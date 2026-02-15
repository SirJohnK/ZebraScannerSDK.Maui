using CommunityToolkit.Mvvm.ComponentModel;
using ZebraBarcodeScannerSDK;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Represents a Zebra barcode scanner device and provides methods to manage its connection and access its properties.
/// </summary>
/// <remarks>
/// This class serves as a high-level abstraction for interacting with Zebra Technologies barcode
/// scanners. It exposes device information and connection management functionality.
/// </remarks>
public partial class ZebraScanner : ObservableObject, IZebraScanner
{
    private readonly Scanner scanner;

    public ZebraScanner(Scanner scanner)
    {
        //Init
        this.scanner = scanner;

        //Set Properties
        Name = scanner.Name ?? scanner.ScannerModelString ?? $"{Id}";
        Description = $"{ManufacturerName}, {Id}";
        if (!string.IsNullOrWhiteSpace(Name)) Description += $", {Name}";
        if (!string.IsNullOrWhiteSpace(DeviceName)) Description += $", {DeviceName}";
        if (!string.IsNullOrWhiteSpace(ModelNumber)) Description += $", {ModelNumber}";
        IsConnected = scanner.Active;
    }

    public int Id => scanner.Id;

    public string? Name { get; set; }

    public string? DeviceName => scanner.ScannerModelString ?? $"{Id}";

    public string? Description { get; set; }

    public string? ModelNumber => scanner.Model;

    public string ManufacturerName => "Zebra Technologies";

    [ObservableProperty]
    private bool isConnected;


    public Task<bool> Connect()
    {
        return Task.Run(() =>
        {
            try
            {
                //Attempt to Connect to Scanner!
                scanner.Connect();
            }
            catch (Exception)
            {
                //Return failed state
                return IsConnected = false;
            }

            //Return Scanner Connected Status
            return IsConnected = true;
        });
    }

    public Task<bool> Disconnect()
    {
        return Task.Run(() =>
        {
            try
            {
                //Attempt to Disconnect to Scanner!
                scanner.Disconnect();
            }
            catch (Exception)
            {
                //Return current state
                return IsConnected;
            }

            //Return Inverted Scanner Disconnected Status
            return IsConnected = false;
        });
    }
}