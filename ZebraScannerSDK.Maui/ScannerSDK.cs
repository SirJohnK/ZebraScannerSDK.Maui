using System.Reflection;

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
            //Setup App Engine
            appEngine = new AppEngine();
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

    public static Stream? SetFactoryDefaultsBarcode => typeof(ScannerSDK).Assembly.GetManifestResourceStream("ZebraBarcodeScannerSDK.Resources.Images.zebra_set_factory_defaults.png");
    public static Stream? HostTriggerEventModeEnabledBarcode => typeof(ScannerSDK).Assembly.GetManifestResourceStream("ZebraBarcodeScannerSDK.Resources.Images.zebra_host_trigger_event_mode_enabled.png");
    public static Stream? HostTriggerEventModeDisabledBarcode => typeof(ScannerSDK).Assembly.GetManifestResourceStream("ZebraBarcodeScannerSDK.Resources.Images.zebra_host_trigger_event_mode_disabled.png");
}