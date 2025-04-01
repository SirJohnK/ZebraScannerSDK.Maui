namespace ZebraBarcodeScannerSDK;

public class Scanners
{
    private AppEngine appEngine;

    public event ScannerAppearedEventHandler? Appeared;

    public event ScannerDisappearedEventHandler? Disappeared;

    public event ScannerConnectedEventHandler? Connected;

    public event ScannerDisconnectedEventHandler? Disconnected;

    public event AuxScannerAppearedEventHandler? AuxScannerAppeared;

    public event BarcodeDataEventHandler? BarcodeData;

    public event ImageEventHandler? ImageData;

    public event VideoEventHandler? VideoFrame;

    public event FirmwareUpdateEventHandler? FirmwareUpdate;

    public event ScannerDisconnectEventHandler? ScannerDisconnect;

    internal Scanners(AppEngine appEngine)
    {
        this.appEngine = appEngine;
        this.appEngine.ScannerHasAppearedEvent += new AppEngine.ScannerHasAppearedEventHandler(AppEnginScannerHasAppearedEvent);
        this.appEngine.ScannerHasDisappearedEvent += new AppEngine.ScannerHasDisappearedEventHandler(AppEnginScannerHasDisappearedEvent);
        this.appEngine.CommunicationSessionEstablishedEvent += new AppEngine.CommunicationSessionEstablishedEventHandler(AppEnginCommunicationSessionEstablishedEvent);
        this.appEngine.CommunicationSessionTerminatedEvent += new AppEngine.CommunicationSessionTerminatedEventHandler(AppEnginCommunicationSessionTerminatedEvent);
        this.appEngine.BarcodeDataEvent += new AppEngine.BarcodeDataEventHandler(AppEnginBarcodeDataEvent);
        this.appEngine.FirmwareUpdateEvent += new AppEngine.FirmwareUpdateEventHandler(AppEnginFirmwareUpdateEvent);
    }

    public void SetOperationMode(OpMode opMode) => appEngine.SetOperationMode(opMode);

    public void EnableAvailableScannersDetection(bool scannerDetection) => appEngine.EnableAvailableScannersDetection(scannerDetection);

    public void SubscribeForEvents(int notificationsMask) => appEngine.SubscribeForEvents(notificationsMask);

    public void UnSubscribeForEvents(int notificationsMask) => appEngine.UnsubscribeForEvents(notificationsMask);

    public void StartScanForTopologyChanges() => appEngine.DcssdkStartScanForTopologyChanges();

    public List<Scanner> GetAvailableScanners() => appEngine.GetAvailableScannerList();

    public List<Scanner> GetActiveScanners() => appEngine.GetActiveScannerList();

    public void EnableBluetoothScannerDiscovery() => appEngine.EnableBluetoothScannerDiscovery(true);

    public void SetSTCEnabledState(bool enableState)
    {
    }

    public void DisableBluetoothScannerDiscovery() => appEngine.EnableBluetoothScannerDiscovery(false);

    private void AppEnginScannerHasAppearedEvent(Scanner availableScanner) => Appeared?.Invoke(availableScanner);

    private void AppEnginScannerHasDisappearedEvent(int scannerID) => Disappeared?.Invoke(scannerID);

    private void AppEnginCommunicationSessionEstablishedEvent(Scanner activeScanner) => Connected?.Invoke(activeScanner);

    private void AppEnginCommunicationSessionTerminatedEvent(int scannerID) => Disconnected?.Invoke(scannerID);

    private void AppEnginBarcodeDataEvent(BarcodeData barcodeData, int scannerID) => BarcodeData?.Invoke(barcodeData, scannerID);

    private void AppEnginImageEvent(byte[] imageData, int scannerID) => ImageData?.Invoke(imageData, scannerID);

    private void AppEnginVideoEvent(byte[] videoFrame, int scannerID) => VideoFrame?.Invoke(videoFrame, scannerID);

    private void AppEnginAuxScannerAppearedEvent(Scanner newTopology, Scanner scannerInfo) => AuxScannerAppeared?.Invoke(newTopology, scannerInfo);

    private void AppEnginFirmwareUpdateEvent(FirmwareUpdateEvent firmwareUpdateEvent) => FirmwareUpdate?.Invoke(firmwareUpdateEvent);

    private void AppEnginScannerDisconnectEvent() => ScannerDisconnect?.Invoke();

    public delegate void ScannerAppearedEventHandler(Scanner scannerInfo);

    public delegate void ScannerDisappearedEventHandler(int scannerID);

    public delegate void ScannerConnectedEventHandler(Scanner scannerInfo);

    public delegate void ScannerDisconnectedEventHandler(int scannerID);

    public delegate void AuxScannerAppearedEventHandler(Scanner newTopology, Scanner scannerInfo);

    public delegate void BarcodeDataEventHandler(BarcodeData barcodeData, int scannerID);

    public delegate void ImageEventHandler(byte[] imageData, int scannerID);

    public delegate void VideoEventHandler(byte[] videoFrame, int scannerID);

    public delegate void FirmwareUpdateEventHandler(FirmwareUpdateEvent firmwareUpdateEvent);

    public delegate void ScannerDisconnectEventHandler();
}