
namespace ZebraBarcodeScannerSDK;

/// <summary>
/// Generic AppEngine class.
/// </summary>
public class AppEngine
{
    internal void Close()
    {
        throw new NotImplementedException();
    }

    internal void ConnectScanner(int id)
    {
        throw new NotImplementedException();
    }

    internal void DisconnectScanner(int id)
    {
        throw new NotImplementedException();
    }

    internal void EnableAutomaticSessionReestablishment(bool reconnection, int id)
    {
        throw new NotImplementedException();
    }

    internal string ExecuteCommand(OpCode opCode, string inXml, int id)
    {
        throw new NotImplementedException();
    }

    internal byte[]? GetBluetoothPairingBarcode(PairingBarcodeType barcodeType, BluetoothProtocol bluetoothProtocol, ScannerConfiguration scannerConfiguration, string bluetoothMAC)
    {
        throw new NotImplementedException();
    }

    internal byte[]? GetBluetoothPairingBarcode(PairingBarcodeType barcodeType, BluetoothProtocol bluetoothProtocol, ScannerConfiguration scannerConfiguration)
    {
        throw new NotImplementedException();
    }

    internal string? GetSdkVersion()
    {
        throw new NotImplementedException();
    }

    internal byte[]? GetUSBSNAPIWithImagingBarcode()
    {
        throw new NotImplementedException();
    }

    internal void SetDelegate()
    {
        throw new NotImplementedException();
    }
}