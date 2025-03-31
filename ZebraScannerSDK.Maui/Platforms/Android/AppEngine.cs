using Android.Bluetooth;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Com.Zebra.Barcode.Sdk.Sms;
using Com.Zebra.Scannercontrol;
using Java.Interop;

namespace ZebraBarcodeScannerSDK;

public class AppEngine : Java.Lang.Object, IDcsSdkApiDelegate, IJavaObject, IDisposable, IJavaPeerable, IDcsScannerEventsOnReLaunch
{
    public const string RESULT_SUCCESS = "DCSSDK_RESULT_SUCCESS";

    private readonly SDKHandler scannerApi;
    private readonly List<Scanner> availableScannerList = new();
    private readonly List<Scanner> activeScannerList = new();

    internal event ScannerHasAppearedEventHandler? ScannerHasAppearedEvent;

    internal event ScannerHasDisappearedEventHandler? ScannerHasDisappearedEvent;

    internal event CommunicationSessionEstablishedEventHandler? CommunicationSessionEstablishedEvent;

    internal event AuxScannerAppearedEventHandler? AuxScannerAppearedEvent;

    internal event CommunicationSessionTerminatedEventHandler? CommunicationSessionTerminatedEvent;

    internal event BarcodeDataEventHandler? BarcodeDataEvent;

    internal event ImageEventHandler? ImageEvent;

    internal event VideoEventHandler? VideoEvent;

    internal event FirmwareUpdateEventHandler? FirmwareUpdateEvent;

    internal event ConnectingToLastConnectedScannerEventHandler? ConnectingToLastConnectedScannerEvent;

    internal event LastConnectedScannerDetectEventHandler? LastConnectedScannerDetectEvent;

    internal event ScannerDisconnectEventHandler? ScannerDisconnectEvent;

    public AppEngine() => scannerApi = new SDKHandler(Application.Context);

    internal string? GetSdkVersion() => scannerApi.DcssdkGetVersion();

    internal void SetDelegate()
    {
        DCSSDKDefs.DCSSDK_RESULT? dcssdkResult = scannerApi.DcssdkSetDelegate(this);
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal void SubscribeForEvents(int sdkEventsMask)
    {
        DCSSDKDefs.DCSSDK_RESULT? dcssdkResult = scannerApi.DcssdkSubsribeForEvents(sdkEventsMask);
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal void UnsubscribeForEvents(int sdkEventsMask)
    {
        DCSSDKDefs.DCSSDK_RESULT? dcssdkResult = scannerApi.DcssdkUnsubsribeForEvents(sdkEventsMask);
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal void SetOperationMode(OpMode opMode)
    {
        DCSSDKDefs.DCSSDK_MODE? operationalMode;
        switch (opMode)
        {
            case OpMode.OPMODE_BTLE:
                operationalMode = DCSSDKDefs.DCSSDK_MODE.DcssdkOpmodeBtLe;
                break;

            case OpMode.OPMODE_SSI:
                operationalMode = DCSSDKDefs.DCSSDK_MODE.DcssdkOpmodeBtNormal;
                break;

            case OpMode.OPMODE_SNAPI:
                operationalMode = DCSSDKDefs.DCSSDK_MODE.DcssdkOpmodeSnapi;
                break;

            case OpMode.OPMODE_USB_CDC:
                operationalMode = DCSSDKDefs.DCSSDK_MODE.DcssdkOpmodeUsbCdc;
                break;

            default:
                operationalMode = DCSSDKDefs.DCSSDK_MODE.DcssdkOpmodeDisabled;
                break;
        }
        if (opMode != OpMode.OPMODE_BTLE && opMode != OpMode.OPMODE_SNAPI && opMode != OpMode.OPMODE_SSI && opMode != OpMode.OPMODE_USB_CDC)
            throw new Exception("Operation Mode is not supported for Android Scanner SDK.");

        DCSSDKDefs.DCSSDK_RESULT? dcssdkResult = scannerApi.DcssdkSetOperationalMode(operationalMode);
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal void EnableAvailableScannersDetection(bool deviceDetection)
    {
        DCSSDKDefs.DCSSDK_RESULT? dcssdkResult = scannerApi.DcssdkEnableAvailableScannersDetection(deviceDetection);
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal void EnableAutomaticSessionReestablishment(bool reconnection, int scannerID)
    {
        DCSSDKDefs.DCSSDK_RESULT? dcssdkResult = scannerApi.DcssdkEnableAutomaticSessionReestablishment(reconnection, scannerID);
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal void DcssdkEnableBluetoothScannerDiscovery(bool bluetoothScannerDetection)
    {
        DCSSDKDefs.DCSSDK_RESULT? dcssdkResult = scannerApi.DcssdkEnableBluetoothScannersDiscovery(bluetoothScannerDetection);
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal List<Scanner> GetAvailableScannerList()
    {
        availableScannerList.Clear();
        var availableScanners = scannerApi.DcssdkGetAvailableScannersList();
        if ((availableScanners?.Count ?? 0) > 0)
        {
            availableScannerList.AddRange(availableScanners!.Select(scannerInfo => new Scanner(scannerInfo, this)));
        }
        return availableScannerList;
    }

    internal List<Scanner> GetActiveScannerList()
    {
        activeScannerList.Clear();
        var activeScanners = scannerApi.DcssdkGetActiveScannersList();
        if ((activeScanners?.Count ?? 0) > 0)
        {
            activeScannerList.AddRange(activeScanners!.Select(scannerInfo => new Scanner(scannerInfo, this)));
        }
        return activeScannerList;
    }

    internal void ConnectScanner(int scannerID)
    {
        var dcssdkResult = scannerApi.DcssdkEstablishCommunicationSession(scannerID);
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal void DisconnectScanner(int scannerID)
    {
        var dcssdkResult = scannerApi.DcssdkTerminateCommunicationSession(scannerID);
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal string ExecuteCommand(OpCode opCode, string inXml, int scannerID)
    {
        DCSSDKDefs.DCSSDK_COMMAND_OPCODE? cmdOpCode;
        switch (opCode)
        {
            case OpCode.FIRMWARE_ABORT_UPDATE_FIRMWARE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkAbortUpdateFirmware;
                break;

            case OpCode.DEVICE_AIM_OFF:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDeviceAimOff;
                break;

            case OpCode.DEVICE_AIM_ON:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDeviceAimOn;
                break;

            case OpCode.DEVICE_PULL_TRIGGER:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDevicePullTrigger;
                break;

            case OpCode.DEVICE_RELEASE_TRIGGER:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDeviceReleaseTrigger;
                break;

            case OpCode.DEVICE_SCAN_DISABLE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDeviceScanDisable;
                break;

            case OpCode.DEVICE_SCAN_ENABLE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDeviceScanEnable;
                break;

            case OpCode.DEVICE_VIBRATION_FEEDBACK:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDeviceVibrationFeedback;
                break;

            case OpCode.DEVICE_IMAGE_MODE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDeviceImageMode;
                break;

            case OpCode.DEVICE_BARCODE_MODE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDeviceBarcodeMode;
                break;

            case OpCode.DEVICE_VIDEO_MODE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDeviceVideoMode;
                break;

            case OpCode.RSM_ATTRIBUTE_GET_ALL:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkRsmAttrGetall;
                break;

            case OpCode.RSM_ATTRIBUTE_GET:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkRsmAttrGet;
                break;

            case OpCode.RSM_ATTRIBUTE_SET:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkRsmAttrSet;
                break;

            case OpCode.RSM_ATTRIBUTE_STORE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkRsmAttrStore;
                break;

            case OpCode.FIRMWARE_START_NEW_FIRMWARE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkStartNewFirmware;
                break;

            case OpCode.FIRMWARE_UPDATE_DAT:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkUpdateFirmware;
                break;

            case OpCode.SBT_UPDATE_FIRMWARE_FROM_PLUGIN:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkUpdateFirmware;
                break;

            case OpCode.RSM_SET_ACTION:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkSetAction;
                break;

            case OpCode.SCALE_READ_WEIGHT:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkReadWeight;
                break;

            case OpCode.SCALE_ZERO_SCALE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkZeroScale;
                break;

            case OpCode.SCALE_RESET_SCALE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkResetScale;
                break;

            case OpCode.SCALE_ENABLE_SCALE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkEnableScale;
                break;

            case OpCode.SCALE_DISABLE_SCALE:
                cmdOpCode = DCSSDKDefs.DCSSDK_COMMAND_OPCODE.DcssdkDisableScale;
                break;

            default:
                throw new Exception("Opcode not supported");
        }
        return scannerApi.DcssdkExecuteCommandOpCodeInXMLForScanner(cmdOpCode, inXml) ?? throw new Exception("Execute command not successful");
    }

    public void DcssdkEventScannerAppeared(DCSScannerInfo? availableScanner)
    {
        if (availableScanner is null || availableScannerList.Exists(scanner => scanner.Id == availableScanner.ScannerID)) return;

        var scanner = new Scanner(availableScanner, this);
        availableScannerList.Add(scanner);
        ScannerHasAppearedEvent?.Invoke(scanner);
    }

    public void DcssdkEventScannerDisappeared(int scannerID)
    {
        var scanner = availableScannerList.FirstOrDefault(scanner => scanner.Id == scannerID);
        if (scanner is null) return;

        availableScannerList.Remove(scanner);
        ScannerHasDisappearedEvent?.Invoke(scannerID);
    }

    public void DcssdkEventAuxScannerAppeared(DCSScannerInfo? newTopology, DCSScannerInfo? auxScanner)
    {
        if (newTopology is null || auxScanner is null) return;

        AuxScannerAppearedEvent?.Invoke(new Scanner(newTopology, this), new Scanner(auxScanner, this));
    }

    public void DcssdkEventCommunicationSessionEstablished(DCSScannerInfo? activeScanner)
    {
        if (activeScanner is not null)
        {
            var scanner = availableScannerList.FirstOrDefault(scanner => scanner.Id == activeScanner.ScannerID);
            if (scanner is null) return;

            availableScannerList.Remove(scanner);
            availableScannerList.Add(new Scanner(activeScanner, this));
            CommunicationSessionEstablishedEvent?.Invoke(scanner);
        }
    }

    public void DcssdkEventCommunicationSessionTerminated(int scannerID) => CommunicationSessionTerminatedEvent?.Invoke(scannerID);

    public void DcssdkEventBarcode(byte[]? barcodeData, int barcodeType, int scannerID)
    {
        if (barcodeData is null) return;

        BarcodeDataEvent?.Invoke(new BarcodeData(barcodeData, barcodeType), scannerID);
    }

    public void DcssdkEventBinaryData(byte[]? binaryData, int scannerID)
    {
    }

    public void DcssdkEventFirmwareUpdate(Com.Zebra.Scannercontrol.FirmwareUpdateEvent? fwUpdateEventObj)
    {
        if (fwUpdateEventObj is null) return;

        FirmwareUpdateEvent?.Invoke(new FirmwareUpdateEvent(fwUpdateEventObj, this));
    }

    public void DcssdkEventImage(byte[]? imageData, int scannerID)
    {
        if (imageData is null) return;

        ImageEvent?.Invoke(imageData, scannerID);
    }

    public void DcssdkEventVideo(byte[]? videoFrame, int scannerID)
    {
        if (videoFrame is null) return;

        VideoEvent?.Invoke(videoFrame, scannerID);
    }

    internal void Close()
    {
        var dcssdkResult = scannerApi.DcssdkClose();
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal Bitmap? GetBitmapFromView(View? view)
    {
        if (view is null || Bitmap.Config.Argb8888 is null) return null;

        var bitmap = Bitmap.CreateBitmap(1000, 100, Bitmap.Config.Argb8888);
        if (bitmap is not null)
        {
            var canvas = new Canvas(bitmap);
            view.Layout(view.Left, view.Top, view.Right, view.Bottom);
            view.Draw(canvas);
        }

        return bitmap;
    }

    internal byte[]? GetByteArrayFromBitmap(Bitmap? bitmapImage)
    {
        if (bitmapImage is null) return null;

        var memoryStream = new MemoryStream();
        bitmapImage.Compress(Bitmap.CompressFormat.Png!, 0, memoryStream);
        return memoryStream.ToArray();
    }

    internal byte[]? GetBluetoothPairingBarcode(
      PairingBarcodeType pairingBarcodeType,
      BluetoothProtocol bluetoothProtocol,
      ScannerConfiguration scannerConfiguration,
      string bluetoothMAC)
    {
        if (pairingBarcodeType != PairingBarcodeType.BARCODE_TYPE_STC)
            throw new Exception("PairingBarcodeType not supported");

        DCSSDKDefs.DCSSDK_BT_PROTOCOL? btProtocol;
        switch (bluetoothProtocol)
        {
            case BluetoothProtocol.LEGACY_B:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.LegacyB;
                break;

            case BluetoothProtocol.CRD_BT:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.CrdBt;
                break;

            case BluetoothProtocol.SSI_BT_SSI_SLAVE:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SsiBtSsiSlave;
                break;

            case BluetoothProtocol.SPP_BT_MASTER:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SppBtMaster;
                break;

            case BluetoothProtocol.SPP_BT_SLAVE:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SppBtSlave;
                break;

            case BluetoothProtocol.HID_BT:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.HidBt;
                break;

            case BluetoothProtocol.SSI_BT_MFI:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SsiBtMfi;
                break;

            case BluetoothProtocol.HID_BT_LE:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.HidBtLe;
                break;

            case BluetoothProtocol.CRD_BT_LE:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.CrdBtLe;
                break;

            case BluetoothProtocol.SSI_BT_CRADLE_HOST:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SsiBtCradleHost;
                break;

            case BluetoothProtocol.SSI_BT_LE:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SsiBtLe;
                break;

            default:
                throw new Exception("BluetoothProtocol not supported");
        }

        DCSSDKDefs.DCSSDK_BT_SCANNER_CONFIG? defaultOption;
        switch (scannerConfiguration)
        {
            case ScannerConfiguration.KEEP_CURRENT:
                defaultOption = DCSSDKDefs.DCSSDK_BT_SCANNER_CONFIG.KeepCurrent;
                break;

            case ScannerConfiguration.SET_FACTORY_DEFAULTS:
                defaultOption = DCSSDKDefs.DCSSDK_BT_SCANNER_CONFIG.SetFactoryDefaults;
                break;

            case ScannerConfiguration.RESTORE_FACTORY_DEFAULTS:
                defaultOption = DCSSDKDefs.DCSSDK_BT_SCANNER_CONFIG.RestoreFactoryDefaults;
                break;

            default:
                throw new System.Exception("ScannerConfiguration not supported");
        }

        var view = scannerApi.DcssdkGetPairingBarcode(btProtocol, defaultOption, bluetoothMAC) as View;
        return GetByteArrayFromBitmap(GetBitmapFromView(view));
    }

    internal void SetSTCEnabledState(bool STCEnabledState)
    {
        var dcssdkResult = scannerApi.DcssdkSetSTCEnabledState(STCEnabledState);
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    internal byte[]? GetBluetoothPairingBarcode(
      PairingBarcodeType pairingBarcodeType,
      BluetoothProtocol bluetoothProtocol,
      ScannerConfiguration scannerConfiguration)
    {
        if (pairingBarcodeType != PairingBarcodeType.BARCODE_TYPE_STC)
            throw new System.Exception("PairingBarcodeType not supported");

        DCSSDKDefs.DCSSDK_BT_PROTOCOL? btProtocol;
        switch (bluetoothProtocol)
        {
            case BluetoothProtocol.LEGACY_B:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.LegacyB;
                break;

            case BluetoothProtocol.CRD_BT:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.CrdBt;
                break;

            case BluetoothProtocol.SSI_BT_SSI_SLAVE:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SsiBtSsiSlave;
                break;

            case BluetoothProtocol.SPP_BT_MASTER:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SppBtMaster;
                break;

            case BluetoothProtocol.SPP_BT_SLAVE:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SppBtSlave;
                break;

            case BluetoothProtocol.HID_BT:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.HidBt;
                break;

            case BluetoothProtocol.SSI_BT_MFI:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SsiBtMfi;
                break;

            case BluetoothProtocol.HID_BT_LE:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.HidBtLe;
                break;

            case BluetoothProtocol.CRD_BT_LE:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.CrdBtLe;
                break;

            case BluetoothProtocol.SSI_BT_CRADLE_HOST:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SsiBtCradleHost;
                break;

            case BluetoothProtocol.SSI_BT_LE:
                btProtocol = DCSSDKDefs.DCSSDK_BT_PROTOCOL.SsiBtLe;
                break;

            default:
                throw new Exception("BluetoothProtocol not supported");
        }

        DCSSDKDefs.DCSSDK_BT_SCANNER_CONFIG? defaultOption;
        switch (scannerConfiguration)
        {
            case ScannerConfiguration.KEEP_CURRENT:
                defaultOption = DCSSDKDefs.DCSSDK_BT_SCANNER_CONFIG.KeepCurrent;
                break;

            case ScannerConfiguration.SET_FACTORY_DEFAULTS:
                defaultOption = DCSSDKDefs.DCSSDK_BT_SCANNER_CONFIG.SetFactoryDefaults;
                break;

            case ScannerConfiguration.RESTORE_FACTORY_DEFAULTS:
                defaultOption = DCSSDKDefs.DCSSDK_BT_SCANNER_CONFIG.RestoreFactoryDefaults;
                break;

            default:
                throw new System.Exception("ScannerConfiguration not supported");
        }

        var view = scannerApi.DcssdkGetPairingBarcode(btProtocol, defaultOption) as View;
        return GetByteArrayFromBitmap(GetBitmapFromView(view));
    }

    internal byte[]? GetUSBSNAPIWithImagingBarcode()
    {
        var view = scannerApi.DcssdkGetUSBSNAPIWithImagingBarcode() as View;
        return GetByteArrayFromBitmap(GetBitmapFromView(view));
    }

    internal void DcssdkStartScanForTopologyChanges()
    {
        var dcssdkResult = scannerApi.DcssdkStartScanForTopologyChanges();
        if (!(dcssdkResult?.ToString().Equals(RESULT_SUCCESS) ?? false))
            throw new Exception(dcssdkResult?.ToString());
    }

    public void DcssdkEventConfigurationUpdate(ConfigurationUpdateEvent? configurationUpdateEvent) => throw new NotImplementedException();

    public void OnConnectingToLastConnectedScanner(BluetoothDevice? bluetoothDevice)
    {
        if (bluetoothDevice is null) return;

        ConnectingToLastConnectedScannerEvent?.Invoke(bluetoothDevice);
    }

    public bool OnLastConnectedScannerDetect(BluetoothDevice? bluetoothDevice)
    {
        if (bluetoothDevice is null) return false;

        return LastConnectedScannerDetectEvent?.Invoke(bluetoothDevice) ?? false;
    }

    public void OnScannerDisconnect() => ScannerDisconnectEvent?.Invoke();

    internal delegate void ScannerHasAppearedEventHandler(Scanner scannerInfo);

    internal delegate void ScannerHasDisappearedEventHandler(int scannerID);

    internal delegate void CommunicationSessionEstablishedEventHandler(Scanner scannerInfo);

    internal delegate void AuxScannerAppearedEventHandler(Scanner newTopology, Scanner scannerInfo);

    internal delegate void CommunicationSessionTerminatedEventHandler(int scannerID);

    internal delegate void BarcodeDataEventHandler(BarcodeData barcodeData, int scannerID);

    internal delegate void ImageEventHandler(byte[] imageData, int scannerID);

    internal delegate void VideoEventHandler(byte[] videoFrame, int scannerID);

    internal delegate void FirmwareUpdateEventHandler(FirmwareUpdateEvent firmwareUpdateEvent);

    internal delegate void ConnectingToLastConnectedScannerEventHandler(BluetoothDevice bluetoothDevice);

    internal delegate bool LastConnectedScannerDetectEventHandler(BluetoothDevice bluetoothDevice);

    internal delegate void ScannerDisconnectEventHandler();
}