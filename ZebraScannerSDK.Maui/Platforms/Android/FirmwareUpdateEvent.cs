using Com.Zebra.Scannercontrol;

namespace ZebraBarcodeScannerSDK;

public class FirmwareUpdateEvent
{
    private AppEngine appEngine;
    private Scanner? scannerInfo;
    private FirmwareEventType eventType;
    private int maxRecords;
    private int softwareComponent;
    private int currentRecord;
    private int size;
    private SDKResultCodes status;

    public FirmwareUpdateEvent(Com.Zebra.Scannercontrol.FirmwareUpdateEvent firmwareUpdate, AppEngine appEngine)
    {
        this.appEngine = appEngine;
        if (firmwareUpdate.ScannerInfo is not null)
            scannerInfo = new Scanner(firmwareUpdate.ScannerInfo, this.appEngine);

        if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfSessStart)
            eventType = FirmwareEventType.SESSION_START;
        else if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfDlStart)
            eventType = FirmwareEventType.DL_START;
        else if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfDlProgress)
            eventType = FirmwareEventType.DL_PROGRESS;
        else if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfDlEnd)
            eventType = FirmwareEventType.DL_END;
        else if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfSessEnd)
            eventType = FirmwareEventType.SESSION_END;
        else if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfStatus)
            eventType = FirmwareEventType.FIRMWARE_UPDATE_STATUS;
        maxRecords = firmwareUpdate.MaxRecords;
        softwareComponent = firmwareUpdate.SwComponent;
        currentRecord = firmwareUpdate.CurrentRecord;
        if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultSuccess)
            status = SDKResultCodes.RESULT_SUCCESS;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultFailure)
            status = SDKResultCodes.RESULT_FAILURE;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultScannerNotAvailable)
            status = SDKResultCodes.SCANNER_NOT_AVAILABLE;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultScannerNotActive)
            status = SDKResultCodes.RESULT_SCANNER_NOT_ACTIVE;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultInvalidParams)
            status = SDKResultCodes.RESULT_INVALID_PARAMS;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultResponseTimeout)
            status = SDKResultCodes.RESULT_RESPONSE_TIMEOUT;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultOpcodeNotSupported)
            status = SDKResultCodes.RESULT_OPCODE_NOT_SUPPORTED;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultScannerAlreadyActive)
            status = SDKResultCodes.RESULT_SCANNER_ALREADY_ACTIVE;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultFirmwareUpdateInProgress)
            status = SDKResultCodes.RESULT_FIRMWARE_UPDATE_IN_PROGRESS;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultFirmwareUpdateAborted)
        {
            status = SDKResultCodes.RESULT_FIRMWARE_UPDATE_ABORTED;
        }
        else
        {
            if (firmwareUpdate.Status != DCSSDKDefs.DCSSDK_RESULT.DcssdkResultScaleNotPresent) return;
            status = SDKResultCodes.RESULT_SCALE_NOT_PRESENT;
        }
    }

    public Scanner? ScannerInfo => scannerInfo;

    public FirmwareEventType EventType => eventType;

    public int MaxRecords => maxRecords;

    public int SoftwareComponent => softwareComponent;

    public int CurrentRecord => currentRecord;

    public int Size => size;

    public SDKResultCodes Status => status;
}