using ZebraBarcodeScannerSDK;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Managing Zebra barcode scanners, including detection, connection events, and pairing barcode generation.
/// </summary>
/// <remarks>
/// Provides mechanisms to detect Zebra scanners, handle scanner connection and disconnection events,
/// retrieve available scanners, and generate pairing barcodes. Supports asynchronous operations and event-driven scanner management.
/// All members are intended to be used in environments where multiple scanners may be present and dynamically connected or disconnected.
/// </remarks>
public class ZebraScannerManager : IZebraScannerManager, IDisposable
{
    private readonly IScannerSDK scannerSDK;
    private readonly ZebraScannerOptions.Options options;
    private readonly IDictionary<int, IZebraScanner> scanners;

    private const int eventNotificationsMask = (int)(
                    Notifications.EVENT_SCANNER_APPEARANCE |
                    Notifications.EVENT_SCANNER_DISAPPEARANCE |
                    Notifications.EVENT_SESSION_ESTABLISHMENT |
                    Notifications.EVENT_SESSION_TERMINATION |
                    Notifications.EVENT_BARCODE);

    /// <summary>
    /// Event Invoked when Scanner Appears.
    /// </summary>
    public event EventHandler<IZebraScanner>? Appeared;

    /// <summary>
    /// Event Invoked when Barcode being Scanned.
    /// </summary>
    public event EventHandler<BarcodeData>? BarcodeScanned;

    /// <summary>
    /// Event Invoked when Scanner Connected.
    /// </summary>
    public event EventHandler<IZebraScanner>? Connected;

    /// <summary>
    /// Event Invoked when Scanner Disappears.
    /// </summary>
    public event EventHandler<int>? Disappeared;

    /// <summary>
    /// Event Invoked when Scanner Disconnected.
    /// </summary>
    public event EventHandler<int>? Disconnected;

    /// <summary>
    /// Gets a value indicating whether a MAC address is required for generating a pairing barcode.
    /// </summary>
    public bool IsMacAddressRequired => options.PairingBarcodeOptions.MacAddressRequired;

    public ZebraScannerManager(IScannerSDK scannerSDK)
    {
        //Init
        this.scannerSDK = scannerSDK;
        this.options = ZebraScannerOptions.PlatformOptions.GetValueOrDefault(DeviceInfo.Platform);
        scanners = new Dictionary<int, IZebraScanner>();

        //Init Scanner SDK
        scannerSDK.ScannerManager.SetSTCEnabledState(true);
        scannerSDK.ScannerManager.SetOperationMode(this.options.OpMode);
        scannerSDK.ScannerManager.SubscribeForEvents(eventNotificationsMask);

        //Setup Event handlers
        scannerSDK.ScannerManager.BarcodeData += ScannerBarcodeData;
        scannerSDK.ScannerManager.Appeared += ScannerAppeared;
        scannerSDK.ScannerManager.Disappeared += ScannerDisappeared;
        scannerSDK.ScannerManager.Connected += ScannerConnected;
        scannerSDK.ScannerManager.Disconnected += ScannerDisconnected;
    }

    private IEnumerable<IZebraScanner> UpdateScanners()
    {
        //Get Scanners
        scanners.Clear();
        scannerSDK.ScannerManager.GetAvailableScanners().ForEach(scanner => scanners[scanner.Id] = new ZebraScanner(scanner));
        scannerSDK.ScannerManager.GetActiveScanners().ForEach(scanner => scanners[scanner.Id] = new ZebraScanner(scanner));

        //Return updated list
        return scanners.Values;
    }

    private void ScannerBarcodeData(BarcodeData barcodeData, int scannerID)
    {
        //Trigger BarcodeScanned Event
        BarcodeScanned?.Invoke(this, barcodeData);
    }

    private void ScannerAppeared(Scanner scannerInfo)
    {
        //Update scanners dictionary
        var device = new ZebraScanner(scannerInfo);
        scanners[scannerInfo.Id] = device;

        //Invoke Appeared Event
        Appeared?.Invoke(this, device);
    }

    private void ScannerDisappeared(int scannerID)
    {
        //Update scanners dictionary
        scanners.Remove(scannerID);

        //Invoke Disappeared Event
        Disappeared?.Invoke(this, scannerID);
    }

    private void ScannerConnected(Scanner scannerInfo)
    {
        //Update scanners dictionary
        var device = new ZebraScanner(scannerInfo);
        scanners[scannerInfo.Id] = device;

        //Invoke Connected Event
        Connected?.Invoke(this, device);
    }

    private void ScannerDisconnected(int scannerID)
    {
        //Attempt to get scanner
        scanners.TryGetValue(scannerID, out var device);

        //Update scanners dictionary
        scanners.Remove(scannerID);

        //Invoke Disconnected Event
        Disconnected?.Invoke(this, scannerID);
    }

    /// <summary>
    /// Generates a barcode image used for device pairing.
    /// </summary>
    /// <param name="macAdress">The MAC address of the device to pair, or null to use the default device. If specified, must be a valid MAC address format.</param>
    /// <returns>
    /// An ImageSource representing the pairing barcode, or null if the barcode could not be generated.
    /// </returns>
    public ImageSource? GetPairingBarcode(string? macAdress = null)
    {
        //Init
        byte[]? barcode = null;

        //Verify Mac Address Requirement
        if (IsMacAddressRequired)
        {
            //Mac Address Supplied?
            if (string.IsNullOrEmpty(macAdress)) throw new ArgumentNullException(nameof(macAdress), "GetPairingBarcode: Mac address is required!");

            //Get Pairing Barcode with Mac Address
            barcode = scannerSDK.GetBluetoothPairingBarcode(options.PairingBarcodeOptions.Type, options.PairingBarcodeOptions.BluetoothProtocol, options.PairingBarcodeOptions.ScannerConfiguration, macAdress);
        }
        else
        {
            //Get Pairing Barcode without Mac Address
            barcode = scannerSDK.GetBluetoothPairingBarcode(options.PairingBarcodeOptions.Type, options.PairingBarcodeOptions.BluetoothProtocol, options.PairingBarcodeOptions.ScannerConfiguration);
        }

        //Got Barcode?
        return (barcode is not null) ? ImageSource.FromStream(() => new MemoryStream(barcode)) : null;
    }

    /// <summary>
    /// Asynchronously retrieves a collection of available Zebra scanners.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a dictionary mapping scanner IDs to
    /// their corresponding <see cref="IZebraScanner"/> instances. The dictionary is empty if no scanners are found.
    /// </returns>
    public Task<IDictionary<int, IZebraScanner>> GetScanners()
    {
        return Task.Run(() =>
        {
            //Get Scanners, if not already done
            if (scanners.Count == 0)
                UpdateScanners();

            //Return Scanners
            return scanners;
        });
    }

    /// <summary>
    /// Enables scanner detection and will trigger scanner events.
    /// </summary>
    public void EnableDetection()
    {
        UpdateScanners();
        scannerSDK.ScannerManager.EnableAvailableScannersDetection(true);
        scannerSDK.ScannerManager.EnableBluetoothScannerDiscovery();
    }

    /// <summary>
    /// Disables scanner detection and will stop triggering of scanner events.
    /// </summary>
    public void DisableDetection()
    {
        scannerSDK.ScannerManager.EnableAvailableScannersDetection(false);
        scannerSDK.ScannerManager.DisableBluetoothScannerDiscovery();
    }

    public void Dispose()
    {
        //Clean up
        DisableDetection();
        scannerSDK.ScannerManager.UnSubscribeForEvents(eventNotificationsMask);
        scannerSDK.ScannerManager.BarcodeData -= ScannerBarcodeData;
        scannerSDK.ScannerManager.Appeared -= ScannerAppeared;
        scannerSDK.ScannerManager.Disappeared -= ScannerDisappeared;
        scannerSDK.ScannerManager.Connected -= ScannerConnected;
        scannerSDK.ScannerManager.Disconnected -= ScannerDisconnected;
        scannerSDK.Close();
    }
}