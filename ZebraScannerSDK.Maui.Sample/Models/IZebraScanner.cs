namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Represents a Zebra barcode scanner device and provides methods to manage its connection and properties.
/// </summary>
/// <remarks>
/// This interface defines the contract for interacting with a Zebra scanner, including retrieving device
/// information and managing the connection state.
/// </remarks>
public interface IZebraScanner
{
    public int Id { get; }
    public string? Name { get; set; }
    public string? DeviceName { get; }
    public string? Description { get; set; }
    public string? ModelNumber { get; }
    public string ManufacturerName { get; }
    public bool IsConnected { get; set; }
    public Task<bool> Connect();
    public Task<bool> Disconnect();
}