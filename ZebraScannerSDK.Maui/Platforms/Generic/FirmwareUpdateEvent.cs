namespace ZebraBarcodeScannerSDK;

public class FirmwareUpdateEvent
{
    private readonly AppEngine appEngine;

    public FirmwareUpdateEvent(AppEngine appEngine)
    {
        this.appEngine = appEngine;
    }

    public Scanner? ScannerInfo { get; }

    public FirmwareEventType EventType { get; }

    public int MaxRecords { get; }

    public int SoftwareComponent { get; }

    public int CurrentRecord { get; }

    public int Size { get; }

    public SDKResultCodes Status { get; }
}