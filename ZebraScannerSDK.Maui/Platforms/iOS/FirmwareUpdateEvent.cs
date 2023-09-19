using Symbolbtscannersdk;

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

    public FirmwareUpdateEvent(Symbolbtscannersdk.FirmwareUpdateEvent firmwareUpdate, AppEngine appEngine)
    {
        this.appEngine = appEngine;
        if (firmwareUpdate.ScannerInfo is not null)
            scannerInfo = new Scanner(firmwareUpdate.ScannerInfo, this.appEngine);

        maxRecords = firmwareUpdate.MaxRecords;
        softwareComponent = firmwareUpdate.SwComponent;
        currentRecord = firmwareUpdate.CurrentRecord;
        size = firmwareUpdate.Size;
        switch (firmwareUpdate.Status)
        {
            case SbtFwUpdateResult.Success:
                status = SDKResultCodes.RESULT_SUCCESS;
                break;

            case SbtFwUpdateResult.Failure:
                status = SDKResultCodes.RESULT_FAILURE;
                break;

            default:
                status = SDKResultCodes.RESULT_FAILURE;
                break;
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