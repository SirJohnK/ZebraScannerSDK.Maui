using ZebraBarcodeScannerSDK;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Defines an interface for managing Zebra barcode scanners, including detection, connection events, and pairing barcode generation.
/// </summary>
/// <remarks>
/// Implementations of this interface provide mechanisms to detect Zebra scanners, handle scanner
/// connection and disconnection events, retrieve available scanners, and generate pairing barcodes. The interface is
/// designed to support asynchronous operations and event-driven scanner management. All members are intended to be used
/// in environments where multiple scanners may be present and dynamically connected or disconnected.
/// </remarks>
public interface IZebraScannerManager : IDisposable
{
    /// <summary>
    /// Enables scanner detection and will trigger scanner events.
    /// </summary>
    void EnableDetection();

    /// <summary>
    /// Disables scanner detection and will stop triggering of scanner events.
    /// </summary>
    void DisableDetection();

    /// <summary>
    /// Event Invoked when Scanner Appears.
    /// </summary>
    event EventHandler<IZebraScanner>? Appeared;

    /// <summary>
    /// Event Invoked when Barcode being Scanned.
    /// </summary>
    event EventHandler<BarcodeData>? BarcodeScanned;

    /// <summary>
    /// Event Invoked when Scanner Connected.
    /// </summary>
    event EventHandler<IZebraScanner>? Connected;

    /// <summary>
    /// Event Invoked when Scanner Disappears.
    /// </summary>
    event EventHandler<int>? Disappeared;

    /// <summary>
    /// Event Invoked when Scanner Disconnected.
    /// </summary>
    event EventHandler<int>? Disconnected;

    /// <summary>
    /// Asynchronously retrieves a collection of available Zebra scanners.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a dictionary mapping scanner IDs to
    /// their corresponding <see cref="IZebraScanner"/> instances. The dictionary is empty if no scanners are found.
    /// </returns>
    Task<IDictionary<int, IZebraScanner>> GetScanners();

    /// <summary>
    /// Gets a value indicating whether a MAC address is required for generating a pairing barcode.
    /// </summary>
    bool IsMacAddressRequired { get; }

    /// <summary>
    /// Generates a barcode image used for device pairing.
    /// </summary>
    /// <param name="macAdress">The MAC address of the device to pair, or null to use the default device. If specified, must be a valid MAC address format.</param>
    /// <returns>
    /// An ImageSource representing the pairing barcode, or null if the barcode could not be generated.
    /// </returns>
    ImageSource? GetPairingBarcode(string? macAdress = null);
}