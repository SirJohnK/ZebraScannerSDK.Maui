using Com.Zebra.Scannercontrol;

namespace ZebraBarcodeScannerSDK;

public class FirmwareUpdateEvent
{
    private readonly AppEngine appEngine;

    public FirmwareUpdateEvent(Com.Zebra.Scannercontrol.FirmwareUpdateEvent firmwareUpdate, AppEngine appEngine)
    {
        this.appEngine = appEngine;
        if (firmwareUpdate.ScannerInfo is not null)
            ScannerInfo = new Scanner(firmwareUpdate.ScannerInfo, this.appEngine);

        if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfSessStart)
            EventType = FirmwareEventType.SESSION_START;
        else if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfDlStart)
            EventType = FirmwareEventType.DL_START;
        else if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfDlProgress)
            EventType = FirmwareEventType.DL_PROGRESS;
        else if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfDlEnd)
            EventType = FirmwareEventType.DL_END;
        else if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfSessEnd)
            EventType = FirmwareEventType.SESSION_END;
        else if (firmwareUpdate.EventType == DCSSDKDefs.DCSSDK_FU_EVENT_TYPE.ScannerUfStatus)
            EventType = FirmwareEventType.FIRMWARE_UPDATE_STATUS;

        MaxRecords = firmwareUpdate.MaxRecords;
        SoftwareComponent = firmwareUpdate.SwComponent;
        CurrentRecord = firmwareUpdate.CurrentRecord;

        if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultSuccess)
            Status = SDKResultCodes.RESULT_SUCCESS;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultFailure)
            Status = SDKResultCodes.RESULT_FAILURE;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultScannerNotAvailable)
            Status = SDKResultCodes.SCANNER_NOT_AVAILABLE;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultScannerNotActive)
            Status = SDKResultCodes.RESULT_SCANNER_NOT_ACTIVE;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultInvalidParams)
            Status = SDKResultCodes.RESULT_INVALID_PARAMS;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultResponseTimeout)
            Status = SDKResultCodes.RESULT_RESPONSE_TIMEOUT;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultOpcodeNotSupported)
            Status = SDKResultCodes.RESULT_OPCODE_NOT_SUPPORTED;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultScannerAlreadyActive)
            Status = SDKResultCodes.RESULT_SCANNER_ALREADY_ACTIVE;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultFirmwareUpdateInProgress)
            Status = SDKResultCodes.RESULT_FIRMWARE_UPDATE_IN_PROGRESS;
        else if (firmwareUpdate.Status == DCSSDKDefs.DCSSDK_RESULT.DcssdkResultFirmwareUpdateAborted)
        {
            Status = SDKResultCodes.RESULT_FIRMWARE_UPDATE_ABORTED;
        }
        else
        {
            if (firmwareUpdate.Status != DCSSDKDefs.DCSSDK_RESULT.DcssdkResultScaleNotPresent) return;
            Status = SDKResultCodes.RESULT_SCALE_NOT_PRESENT;
        }
    }

    public Scanner? ScannerInfo { get; }

    public FirmwareEventType EventType { get; }

    public int MaxRecords { get; }

    public int SoftwareComponent { get; }

    public int CurrentRecord { get; }

    public int Size { get; }

    public SDKResultCodes Status { get; }
}