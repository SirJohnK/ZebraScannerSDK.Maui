using ObjCRuntime;
using Symbolbtscannersdk;

namespace ZebraBarcodeScannerSDK;

public class AppEngine : ISbtSdkApiDelegate
{
    public const string STATUS_SUCCESS = "Success";
    private IISbtSdkApi? scannerApi;
    private List<Scanner> scannerList = new();
    private List<Scanner> activeScannerList = new();

    internal event ScannerHasAppearedEventHandler? ScannerHasAppearedEvent;

    internal event ScannerHasDisappearedEventHandler? ScannerHasDisappearedEvent;

    internal event CommunicationSessionEstablishedEventHandler? CommunicationSessionEstablishedEvent;

    internal event CommunicationSessionTerminatedEventHandler? CommunicationSessionTerminatedEvent;

    internal event BarcodeDataEventHandler? BarcodeDataEvent;

    internal event FirmwareUpdateEventHandler? FirmwareUpdateEvent;

    public AppEngine() => scannerApi = SbtSdkFactory.CreateSbtSdkApiInstance;

    internal void CreateSdkAppInstance() => scannerApi ??= SbtSdkFactory.CreateSbtSdkApiInstance;

    internal string? GetSdkVersion() => scannerApi?.GetSbtGetVersion();

    internal void SetDelegate()
    {
        SbtResult? sbtResult = scannerApi?.SbtSetDelegate(this);
        if (!(sbtResult?.ToString().Equals(STATUS_SUCCESS) ?? false))
            throw new Exception(sbtResult?.ToString());
    }

    internal void SubscribeForEvents(int sdkEventsMask)
    {
        SbtResult? sbtResult = scannerApi?.SbtSubsribeForEvents(sdkEventsMask);
        if (!(sbtResult?.ToString().Equals(STATUS_SUCCESS) ?? false))
            throw new Exception(sbtResult?.ToString());
    }

    internal void UnsubscribeForEvents(int sdkEventsMask)
    {
        SbtResult? sbtResult = scannerApi?.SbtUnsubsribeForEvents(sdkEventsMask);
        if (!(sbtResult?.ToString().Equals(STATUS_SUCCESS) ?? false))
            throw new Exception(sbtResult?.ToString());
    }

    internal void SetOperationMode(OpMode opMode)
    {
        SbtResult? sbtResult = opMode == OpMode.OPMODE_BTLE || opMode == OpMode.OPMODE_MFI || opMode == OpMode.OPMODE_MFI_BTLE ? scannerApi?.SbtSetOperationalMode((int)opMode) : throw new Exception("OpMode is not supported for iOS Scanner SDK.");
        if (!(sbtResult?.ToString().Equals(STATUS_SUCCESS) ?? false))
            throw new Exception(sbtResult?.ToString());
    }

    internal void EnableAvailableScannersDetection(bool deviceDetection)
    {
        SbtResult? sbtResult = scannerApi?.SbtEnableAvailableScannersDetection(deviceDetection);
        if (!(sbtResult?.ToString().Equals(STATUS_SUCCESS) ?? false))
            throw new Exception(sbtResult?.ToString());
    }

    internal void EnableAutomaticSessionReestablishment(bool reconnection, int scannerID)
    {
        SbtResult? sbtResult = scannerApi?.SbtEnableAutomaticSessionReestablishment(reconnection, scannerID);
        if (!(sbtResult?.ToString().Equals(STATUS_SUCCESS) ?? false))
            throw new Exception(sbtResult?.ToString());
    }

    internal List<Scanner> GetAvailableScannerList()
    {
        var availableScanners = new NSMutableArray().Handle.Handle;
        SbtResult? sbtResult = scannerApi?.SbtGetAvailableScannersList(ref availableScanners);
        if (!(sbtResult?.ToString().Equals(STATUS_SUCCESS) ?? false))
            throw new Exception(sbtResult?.ToString());

        var nsObject = Runtime.GetNSObject<NSMutableArray>(availableScanners);
        if (nsObject is not null)
        {
            scannerList.Clear();
            scannerList.AddRange(NSArray.FromArray<SbtScannerInfo>(nsObject).Select(info => new Scanner(info, this)));
        }

        return scannerList;
    }

    internal List<Scanner> GetActiveScannerList()
    {
        var activeScanners = new NSMutableArray().Handle.Handle;
        var sbtResult = scannerApi?.SbtGetActiveScannersList(ref activeScanners);
        if (!(sbtResult?.ToString().Equals(STATUS_SUCCESS) ?? false))
            throw new Exception(sbtResult?.ToString());

        var nsObject = Runtime.GetNSObject<NSMutableArray>(activeScanners);
        if (nsObject is not null)
        {
            activeScannerList.Clear();
            activeScannerList.AddRange(NSArray.FromArray<SbtScannerInfo>(nsObject).Select(info => new Scanner(info, this)));
        }

        return activeScannerList;
    }

    internal void ConnectScanner(int scannerID)
    {
        SbtResult? sbtResult = scannerApi?.SbtEstablishCommunicationSession(scannerID);
        if (!(sbtResult?.ToString().Equals(STATUS_SUCCESS) ?? false))
            throw new Exception(sbtResult?.ToString());
    }

    internal void DisconnectScanner(int scannerID)
    {
        SbtResult? sbtResult = scannerApi?.SbtTerminateCommunicationSession(scannerID);
        if (!(sbtResult?.ToString().Equals(STATUS_SUCCESS) ?? false))
            throw new Exception(sbtResult?.ToString());
    }

    internal string ExecuteCommand(OpCode opCode, string inXml, int scannerID)
    {
        var outXML = new NSMutableString().Handle.Handle;
        SbtResult sbtResult = scannerApi?.SbtExecuteCommand((int)opCode, inXml, ref outXML, scannerID) ?? SbtResult.Failure;
        if (!sbtResult.ToString().Equals(STATUS_SUCCESS))
            throw new Exception(sbtResult.ToString());

        var nsObject = Runtime.GetNSObject<NSMutableString>(outXML) ?? NSString.Empty;
        return opCode == OpCode.FIRMWARE_UPDATE_DAT || opCode == OpCode.SBT_UPDATE_FIRMWARE_FROM_PLUGIN ? sbtResult.ToString() : (string)nsObject;
    }

    internal void EnableBluetoothScannerDiscovery(bool enable)
    {
        SbtResult sbtResult = scannerApi?.SbtEnableBluetoothScannerDiscovery(enable) ?? SbtResult.Failure;
        if (!sbtResult.ToString().Equals(STATUS_SUCCESS))
            throw new Exception(sbtResult.ToString());
    }

    public override void SbtEventScannerAppeared(SbtScannerInfo availableScanner)
    {
        if (availableScanner is null || scannerList.Exists(scanner => scanner.Id == availableScanner.ScannerID)) return;

        var scanner = new Scanner(availableScanner, this);
        scannerList.Add(scanner);
        ScannerHasAppearedEvent?.Invoke(scanner);
    }

    public override void SbtEventScannerDisappeared(int scannerID)
    {
        var scanner = scannerList.FirstOrDefault(scanner => scanner.Id == scannerID);
        if (scanner is null) return;

        scannerList.Remove(scanner);
        ScannerHasDisappearedEvent?.Invoke(scannerID);
    }

    public override void SbtEventCommunicationSessionEstablished(SbtScannerInfo activeScanner)
    {
        if (activeScanner is not null)
        {
            var scanner = scannerList.FirstOrDefault(scanner => scanner.Id == activeScanner.ScannerID);
            if (scanner is null) return;

            scannerList.Remove(scanner);
            scannerList.Add(new Scanner(activeScanner, this));
            CommunicationSessionEstablishedEvent?.Invoke(scanner);
        }
    }

    public override void SbtEventCommunicationSessionTerminated(int scannerID) => CommunicationSessionTerminatedEvent?.Invoke(scannerID);

    public override void SbtEventBarcode(string barcodeData, int barcodeType, int scannerID)
    {
    }

    public override void SbtEventBarcodeData(NSData barcodeData, int barcodeType, int scannerID)
    {
        var data = barcodeData?.ToArray();
        if (data is null) return;

        BarcodeDataEvent?.Invoke(new BarcodeData(data, barcodeType), scannerID);
    }

    public override void SbtEventFirmwareUpdate(Symbolbtscannersdk.FirmwareUpdateEvent fwUpdateEventObj)
    {
        if (fwUpdateEventObj is null) return;

        FirmwareUpdateEvent?.Invoke(new FirmwareUpdateEvent(fwUpdateEventObj, this));
    }

    public override void SbtEventImage(NSData imageData, int scannerID)
    {
    }

    public override void SbtEventVideo(NSData videoFrame, int scannerID)
    {
    }

    internal void Close() => throw new Exception("Feature not supported");

    internal byte[]? GetBluetoothPairingBarcode(
      PairingBarcodeType pairingBarcodeType,
      BluetoothProtocol bluetoothProtocol,
      ScannerConfiguration scannerConfiguration,
      string bluetoothMAC,
      CGRect? imageFrame = null)
    {
        BarcodeType barcodeType;
        switch (pairingBarcodeType)
        {
            case PairingBarcodeType.BARCODE_TYPE_LEGACY:
                barcodeType = BarcodeType.Legacy;
                break;

            case PairingBarcodeType.BARCODE_TYPE_STC:
                barcodeType = BarcodeType.Stc;
                break;

            default:
                throw new Exception("PairingBarcodeType not supported");
        }

        StcComProtocol comProtocol;
        switch (bluetoothProtocol)
        {
            case BluetoothProtocol.HID_BT:
                comProtocol = StcComProtocol.SbtSsiHid;
                break;

            case BluetoothProtocol.SSI_BT_MFI:
                comProtocol = StcComProtocol.StcSsiMfi;
                break;

            case BluetoothProtocol.SSI_BT_LE:
                comProtocol = StcComProtocol.StcSsiBle;
                break;

            default:
                throw new Exception("BluetoothProtocol not supported");
        }

        SetdefaultStatus setDefaultsStatus;
        switch (scannerConfiguration)
        {
            case ScannerConfiguration.KEEP_CURRENT:
                setDefaultsStatus = SetdefaultStatus.No;
                break;

            case ScannerConfiguration.SET_FACTORY_DEFAULTS:
                setDefaultsStatus = SetdefaultStatus.Yes;
                break;

            case ScannerConfiguration.RESTORE_FACTORY_DEFAULTS:
                setDefaultsStatus = SetdefaultStatus.Yes;
                break;

            default:
                throw new Exception("ScannerConfiguration not supported");
        }

        return scannerApi?.SbtGetPairingBarcode(barcodeType, comProtocol, setDefaultsStatus, bluetoothMAC, imageFrame ?? new CGRect(0f, 0f, 340f, 250f)).AsPNG()?.ToArray();
    }

    internal byte[]? GetBluetoothPairingBarcode(
      PairingBarcodeType pairingBarcodeType,
      BluetoothProtocol bluetoothProtocol,
      ScannerConfiguration scannerConfiguration,
      CGRect? imageFrame = null)
    {
        BarcodeType barcodeType;
        switch (pairingBarcodeType)
        {
            case PairingBarcodeType.BARCODE_TYPE_LEGACY:
                barcodeType = BarcodeType.Legacy;
                break;

            case PairingBarcodeType.BARCODE_TYPE_STC:
                barcodeType = BarcodeType.Stc;
                break;

            default:
                throw new Exception("PairingBarcodeType not supported");
        }

        StcComProtocol comProtocol;
        switch (bluetoothProtocol)
        {
            case BluetoothProtocol.HID_BT:
                comProtocol = StcComProtocol.SbtSsiHid;
                break;

            case BluetoothProtocol.SSI_BT_MFI:
                comProtocol = StcComProtocol.StcSsiMfi;
                break;

            case BluetoothProtocol.SSI_BT_LE:
                comProtocol = StcComProtocol.StcSsiBle;
                break;

            default:
                throw new Exception("BluetoothProtocol not supported");
        }

        SetdefaultStatus setDefaultsStatus;
        switch (scannerConfiguration)
        {
            case ScannerConfiguration.KEEP_CURRENT:
                setDefaultsStatus = SetdefaultStatus.No;
                break;

            case ScannerConfiguration.SET_FACTORY_DEFAULTS:
                setDefaultsStatus = SetdefaultStatus.Yes;
                break;

            case ScannerConfiguration.RESTORE_FACTORY_DEFAULTS:
                setDefaultsStatus = SetdefaultStatus.Yes;
                break;

            default:
                throw new Exception("ScannerConfiguration not supported");
        }

        return scannerApi?.SbtGetPairingBarcode(barcodeType, comProtocol, setDefaultsStatus, imageFrame ?? new CGRect(0f, 0f, 340f, 250f)).AsPNG()?.ToArray();
    }

    internal byte[] GetUSBSNAPIWithImagingBarcode() => throw new Exception("Not supported");

    internal void DcssdkStartScanForTopologyChanges() => throw new Exception("Feature not supported");

    internal delegate void ScannerHasAppearedEventHandler(Scanner scannerInfo);

    internal delegate void ScannerHasDisappearedEventHandler(int scannerID);

    internal delegate void CommunicationSessionEstablishedEventHandler(Scanner scannerInfo);

    internal delegate void CommunicationSessionTerminatedEventHandler(int scannerID);

    internal delegate void BarcodeDataEventHandler(BarcodeData barcodeData, int scannerID);

    internal delegate void FirmwareUpdateEventHandler(FirmwareUpdateEvent firmwareUpdateEvent);
}