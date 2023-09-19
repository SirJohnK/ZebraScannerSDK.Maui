namespace ZebraBarcodeScannerSDK;

public interface IScannerSDK
{
    Scanners ScannerManager { get; }

    string? Version { get; }

    void Close();

    byte[]? GetBluetoothPairingBarcode(PairingBarcodeType barcodeType, BluetoothProtocol bluetoothProtocol, ScannerConfiguration scannerConfiguration, string bluetoothMAC);

    byte[]? GetBluetoothPairingBarcode(PairingBarcodeType barcodeType, BluetoothProtocol bluetoothProtocol, ScannerConfiguration scannerConfiguration);

    byte[]? GetSNAPIUSBBarcodeWithImage();
}