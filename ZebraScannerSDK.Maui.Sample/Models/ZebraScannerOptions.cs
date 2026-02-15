using ZebraBarcodeScannerSDK;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Provides configuration options for Zebra barcode scanners, including platform-specific settings and pairing barcode parameters.
/// </summary>
/// <remarks>
/// This class exposes default options for supported device platforms, allowing applications to retrieve
/// and apply recommended scanner configurations for Android and iOS devices. The options include operational modes and
/// pairing barcode settings tailored to each platform.
/// </remarks>
public class ZebraScannerOptions
{
    public struct PairingBarcodeOptions
    {
        public PairingBarcodeType Type { get; set; }
        public BluetoothProtocol BluetoothProtocol { get; set; }
        public ScannerConfiguration ScannerConfiguration { get; set; }
        public bool MacAddressRequired { get; set; }
    }

    public struct Options
    {
        public OpMode OpMode { get; set; }

        public PairingBarcodeOptions PairingBarcodeOptions { get; set; }
    }

    public static Dictionary<DevicePlatform, Options> PlatformOptions { get; } = new()
    {
        {DevicePlatform.Android, new()
            {
                OpMode = OpMode.OPMODE_SSI,
                PairingBarcodeOptions = new() { Type = PairingBarcodeType.BARCODE_TYPE_STC, BluetoothProtocol = BluetoothProtocol.SSI_BT_CRADLE_HOST, ScannerConfiguration = ScannerConfiguration.KEEP_CURRENT, MacAddressRequired = true }
            }
        },
        {DevicePlatform.iOS, new()
            {
                OpMode = OpMode.OPMODE_MFI_BTLE,
                PairingBarcodeOptions = new() { Type = PairingBarcodeType.BARCODE_TYPE_STC, BluetoothProtocol = BluetoothProtocol.SSI_BT_LE, ScannerConfiguration = ScannerConfiguration.KEEP_CURRENT, MacAddressRequired = false }
            }
        },
    };
}