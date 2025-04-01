using Symbolbtscannersdk;

namespace ZebraBarcodeScannerSDK;

public class FirmwareUpdateEvent
{
    private AppEngine appEngine;

    public FirmwareUpdateEvent(Symbolbtscannersdk.FirmwareUpdateEvent firmwareUpdate, AppEngine appEngine)
    {
        this.appEngine = appEngine;
        if (firmwareUpdate.ScannerInfo is not null)
            ScannerInfo = new Scanner(firmwareUpdate.ScannerInfo, this.appEngine);

        MaxRecords = firmwareUpdate.MaxRecords;
        SoftwareComponent = firmwareUpdate.SwComponent;
        CurrentRecord = firmwareUpdate.CurrentRecord;
        Size = firmwareUpdate.Size;

        Status = (object)firmwareUpdate.Status switch
        {
            SbtFwUpdateResult.Success => SDKResultCodes.RESULT_SUCCESS,
            SbtFwUpdateResult.Failure => SDKResultCodes.RESULT_FAILURE,
            _ => SDKResultCodes.RESULT_FAILURE,
        };
    }

    public Scanner? ScannerInfo { get; }

    public FirmwareEventType EventType { get; }

    public int MaxRecords { get; }

    public int SoftwareComponent { get; }

    public int CurrentRecord { get; }

    public int Size { get; }

    public SDKResultCodes Status { get; }
}