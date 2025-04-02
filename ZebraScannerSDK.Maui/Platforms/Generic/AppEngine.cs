

namespace ZebraBarcodeScannerSDK;

/// <summary>
/// Generic AppEngine class.
/// </summary>
public class AppEngine
{

    internal event ScannerHasAppearedEventHandler? ScannerHasAppearedEvent;

    internal event ScannerHasDisappearedEventHandler? ScannerHasDisappearedEvent;

    internal event CommunicationSessionEstablishedEventHandler? CommunicationSessionEstablishedEvent;

    internal event AuxScannerAppearedEventHandler? AuxScannerAppearedEvent;

    internal event CommunicationSessionTerminatedEventHandler? CommunicationSessionTerminatedEvent;

    internal event BarcodeDataEventHandler? BarcodeDataEvent;

    internal event ImageEventHandler? ImageEvent;

    internal event VideoEventHandler? VideoEvent;

    internal event FirmwareUpdateEventHandler? FirmwareUpdateEvent;

    internal event ScannerDisconnectEventHandler? ScannerDisconnectEvent;

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

    internal void EnableAvailableScannersDetection(bool enable)
    {
        throw new NotImplementedException();
    }

    internal void EnableBluetoothScannerDiscovery(bool bluetoothScannerDetection)
    {
        throw new NotImplementedException();
    }

    internal string ExecuteCommand(OpCode opCode, string inXml, int id)
    {
        throw new NotImplementedException();
    }

    internal List<Scanner> GetAvailableScannerList()
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

    internal void SetOperationMode(OpMode opMode)
    {
        throw new NotImplementedException();
    }

    internal void SetSTCEnabledState(bool enableState)
    {
        throw new NotImplementedException();
    }

    internal void StartScanForTopologyChanges()
    {
        throw new NotImplementedException();
    }

    internal void UnsubscribeForEvents(int notificationsMask)
    {
        throw new NotImplementedException();
    }

    internal delegate void ScannerHasAppearedEventHandler(Scanner scannerInfo);

    internal delegate void ScannerHasDisappearedEventHandler(int scannerID);

    internal delegate void CommunicationSessionEstablishedEventHandler(Scanner scannerInfo);

    internal delegate void AuxScannerAppearedEventHandler(Scanner newTopology, Scanner scannerInfo);

    internal delegate void CommunicationSessionTerminatedEventHandler(int scannerID);

    internal delegate void BarcodeDataEventHandler(BarcodeData barcodeData, int scannerID);

    internal delegate void ImageEventHandler(byte[] imageData, int scannerID);

    internal delegate void VideoEventHandler(byte[] videoFrame, int scannerID);

    internal delegate void FirmwareUpdateEventHandler(FirmwareUpdateEvent firmwareUpdateEvent);

    internal delegate void ScannerDisconnectEventHandler();
}