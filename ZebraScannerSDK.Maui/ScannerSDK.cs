namespace ZebraBarcodeScannerSDK;

public class ScannerSDK : IScannerSDK
{
    private AppEngine appEngine;
    private Scanners? scanners;

    private static IScannerSDK? implementationInstance;

    /// <summary>
    /// Provides the default implementation for static usage of this service.
    /// </summary>
    public static IScannerSDK Instance => implementationInstance ??= new ScannerSDK();

    internal ScannerSDK()
    {
        try
        {
            appEngine = new AppEngine();
            //#if IOS
            //            appEngine.CreateSdkAppInstance();
            //#endif
            appEngine.SetDelegate();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public Scanners ScannerManager => scanners ??= new Scanners(appEngine);

    public string? Version => appEngine.GetSdkVersion();

    public void Close() => appEngine.Close();

    public byte[]? GetBluetoothPairingBarcode(
      PairingBarcodeType barcodeType,
      BluetoothProtocol bluetoothProtocol,
      ScannerConfiguration scannerConfiguration,
      string bluetoothMAC)
    {
        return appEngine.GetBluetoothPairingBarcode(barcodeType, bluetoothProtocol, scannerConfiguration, bluetoothMAC);
    }

    public byte[]? GetBluetoothPairingBarcode(
      PairingBarcodeType barcodeType,
      BluetoothProtocol bluetoothProtocol,
      ScannerConfiguration scannerConfiguration)
    {
        return appEngine.GetBluetoothPairingBarcode(barcodeType, bluetoothProtocol, scannerConfiguration);
    }

    public byte[]? GetSNAPIUSBBarcodeWithImage() => appEngine.GetUSBSNAPIWithImagingBarcode();
}