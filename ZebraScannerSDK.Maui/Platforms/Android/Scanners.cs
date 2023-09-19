using Android.Bluetooth;

namespace ZebraBarcodeScannerSDK;

public class Scanners
{
    private AppEngine appEngine;
    private static int ANDROID_EVENT_BARCODE = 1;
    private static int ANDROID_EVENT_IMAGE = 2;
    private static int ANDROID_EVENT_VIDEO = 4;
    private static int ANDROID_EVENT_BINARY_DATA = 8;
    private static int ANDROID_EVENT_SCANNER_APPEARANCE = 16;
    private static int ANDROID_EVENT_SCANNER_DISAPPEARANCE = 32;
    private static int ANDROID_EVENT_SESSION_ESTABLISHMENT = 64;
    private static int ANDROID_EVENT_SESSION_TERMINATION = 128;
    private static int IOS_EVENT_BARCODE = 1;
    private static int IOS_EVENT_IMAGE = 2;
    private static int IOS_EVENT_VIDEO = 4;
    private static int IOS_EVENT_SCANNER_APPEARANCE = 8;
    private static int IOS_EVENT_SCANNER_DISAPPEARANCE = 16;
    private static int IOS_EVENT_SESSION_ESTABLISHMENT = 32;
    private static int IOS_EVENT_SESSION_TERMINATION = 64;
    private static int IOS_EVENT_RAW_DATA = 128;

    public event ScannerAppearedEventHandler? Appeared;

    public event ScannerDisappearedEventHandler? Disappeared;

    public event ScannerConnectedEventHandler? Connected;

    public event ScannerDisconnectedEventHandler? Disconnected;

    public event AuxScannerAppearedEventHandler? AuxScannerAppeared;

    public event BarcodeDataEventHandler? BarcodeData;

    public event ImageEventHandler? ImageData;

    public event VideoEventHandler? VideoFrame;

    public event FirmwareUpdateEventHandler? FirmwareUpdateEvent;

    public event ConnectingToLastConnectedScannerEventHandler? ConnectingToLastConnectedScannerEvent;

    public event LastConnectedScannerDetectEventHandler? LastConnectedScannerDetectEvent;

    public event ScannerDisconnectEventHandler? ScannerDisconnectEvent;

    internal Scanners(AppEngine appEngine)
    {
        this.appEngine = appEngine;
        this.appEngine.ScannerHasAppearedEvent += new AppEngine.ScannerHasAppearedEventHandler(AppEnginScannerHasAppearedEvent);
        this.appEngine.ScannerHasDisappearedEvent += new AppEngine.ScannerHasDisappearedEventHandler(AppEnginScannerHasDisappearedEvent);
        this.appEngine.CommunicationSessionEstablishedEvent += new AppEngine.CommunicationSessionEstablishedEventHandler(AppEnginCommunicationSessionEstablishedEvent);
        this.appEngine.CommunicationSessionTerminatedEvent += new AppEngine.CommunicationSessionTerminatedEventHandler(AppEnginCommunicationSessionTerminatedEvent);
        this.appEngine.BarcodeDataEvent += new AppEngine.BarcodeDataEventHandler(AppEnginBarcodeDataEvent);
        this.appEngine.ImageEvent += new AppEngine.ImageEventHandler(AppEnginImageEvent);
        this.appEngine.VideoEvent += new AppEngine.VideoEventHandler(AppEnginVideoEvent);
        this.appEngine.AuxScannerAppearedEvent += new AppEngine.AuxScannerAppearedEventHandler(AppEnginAuxScannerAppearedEvent);
        this.appEngine.FirmwareUpdateEvent += new AppEngine.FirmwareUpdateEventHandler(AppEnginFirmwareUpdateEvent);
        this.appEngine.ConnectingToLastConnectedScannerEvent += new AppEngine.ConnectingToLastConnectedScannerEventHandler(AppEnginConnectingToLastConnectedScannerEvent);
        this.appEngine.LastConnectedScannerDetectEvent += new AppEngine.LastConnectedScannerDetectEventHandler(AppEnginLastConnectedScannerDetectEvent);
        this.appEngine.ScannerDisconnectEvent += new AppEngine.ScannerDisconnectEventHandler(AppEnginScannerDisconnectEvent);
    }

    public void SetOperationMode(OpMode opMode) => appEngine.SetOperationMode(opMode);

    public void EnableAvailableScannersDetection(bool scannerDetection) => appEngine.EnableAvailableScannersDetection(scannerDetection);

    public void SubscribeForEvents(int notificationsMask)
    {
        int sdkEventsMask = 0;
        if ((notificationsMask & IOS_EVENT_BARCODE) == IOS_EVENT_BARCODE)
            sdkEventsMask |= ANDROID_EVENT_BARCODE;
        if ((notificationsMask & IOS_EVENT_IMAGE) == IOS_EVENT_IMAGE)
            sdkEventsMask |= ANDROID_EVENT_IMAGE;
        if ((notificationsMask & IOS_EVENT_VIDEO) == IOS_EVENT_VIDEO)
            sdkEventsMask |= ANDROID_EVENT_VIDEO;
        if ((notificationsMask & IOS_EVENT_SCANNER_APPEARANCE) == IOS_EVENT_SCANNER_APPEARANCE)
            sdkEventsMask |= ANDROID_EVENT_SCANNER_APPEARANCE;
        if ((notificationsMask & IOS_EVENT_SCANNER_DISAPPEARANCE) == IOS_EVENT_SCANNER_DISAPPEARANCE)
            sdkEventsMask |= ANDROID_EVENT_SCANNER_DISAPPEARANCE;
        if ((notificationsMask & IOS_EVENT_SESSION_ESTABLISHMENT) == IOS_EVENT_SESSION_ESTABLISHMENT)
            sdkEventsMask |= ANDROID_EVENT_SESSION_ESTABLISHMENT;
        if ((notificationsMask & IOS_EVENT_SESSION_TERMINATION) == IOS_EVENT_SESSION_TERMINATION)
            sdkEventsMask |= ANDROID_EVENT_SESSION_TERMINATION;
        if ((notificationsMask & IOS_EVENT_RAW_DATA) == IOS_EVENT_RAW_DATA)
            sdkEventsMask |= ANDROID_EVENT_BINARY_DATA;
        appEngine.SubscribeForEvents(sdkEventsMask);
    }

    public void UnSubscribeForEvents(int notificationsMask) => appEngine.UnsubscribeForEvents(notificationsMask);

    public void StartScanForTopologyChanges() => appEngine.DcssdkStartScanForTopologyChanges();

    public List<Scanner> GetAvailableScanners() => appEngine.GetAvailableScannerList();

    public List<Scanner> GetActiveScanners() => appEngine.GetAvailableScannerList();

    public void EnableBluetoothScannerDiscovery() => appEngine.DcssdkEnableBluetoothScannerDiscovery(true);

    public void SetSTCEnabledState(bool enableState) => appEngine.SetSTCEnabledState(enableState);

    public void DisableBluetoothScannerDiscovery() => appEngine.DcssdkEnableBluetoothScannerDiscovery(false);

    private void AppEnginScannerHasAppearedEvent(Scanner availableScanner) => Appeared?.Invoke(availableScanner);

    private void AppEnginScannerHasDisappearedEvent(int scannerID) => Disappeared?.Invoke(scannerID);

    private void AppEnginCommunicationSessionEstablishedEvent(Scanner activeScanner) => Connected?.Invoke(activeScanner);

    private void AppEnginCommunicationSessionTerminatedEvent(int scannerID) => Disconnected?.Invoke(scannerID);

    private void AppEnginBarcodeDataEvent(BarcodeData barcodeData, int scannerID) => BarcodeData?.Invoke(barcodeData, scannerID);

    private void AppEnginImageEvent(byte[] imageData, int scannerID) => ImageData?.Invoke(imageData, scannerID);

    private void AppEnginVideoEvent(byte[] videoFrame, int scannerID) => VideoFrame?.Invoke(videoFrame, scannerID);

    private void AppEnginAuxScannerAppearedEvent(Scanner newTopology, Scanner scannerInfo) => AuxScannerAppeared?.Invoke(newTopology, scannerInfo);

    private void AppEnginFirmwareUpdateEvent(FirmwareUpdateEvent firmwareUpdateEvent) => FirmwareUpdateEvent?.Invoke(firmwareUpdateEvent);

    private void AppEnginConnectingToLastConnectedScannerEvent(BluetoothDevice bluetoothDevice) => ConnectingToLastConnectedScannerEvent?.Invoke(bluetoothDevice);

    private bool AppEnginLastConnectedScannerDetectEvent(BluetoothDevice bluetoothDevice) => LastConnectedScannerDetectEvent?.Invoke(bluetoothDevice) ?? false;

    private void AppEnginScannerDisconnectEvent() => ScannerDisconnectEvent?.Invoke();

    public delegate void ScannerAppearedEventHandler(Scanner scannerInfo);

    public delegate void ScannerDisappearedEventHandler(int scannerID);

    public delegate void ScannerConnectedEventHandler(Scanner scannerInfo);

    public delegate void ScannerDisconnectedEventHandler(int scannerID);

    public delegate void AuxScannerAppearedEventHandler(Scanner newTopology, Scanner scannerInfo);

    public delegate void BarcodeDataEventHandler(BarcodeData barcodeData, int scannerID);

    public delegate void ImageEventHandler(byte[] imageData, int scannerID);

    public delegate void VideoEventHandler(byte[] videoFrame, int scannerID);

    public delegate void FirmwareUpdateEventHandler(FirmwareUpdateEvent firmwareUpdateEvent);

    public delegate void ConnectingToLastConnectedScannerEventHandler(BluetoothDevice bluetoothDevice);

    public delegate bool LastConnectedScannerDetectEventHandler(BluetoothDevice bluetoothDevice);

    public delegate void ScannerDisconnectEventHandler();
}