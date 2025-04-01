namespace ZebraBarcodeScannerSDK;

public partial class Scanner
{
	public Scanner(AppEngine appEngine)
	{
        //Init
        this.appEngine = appEngine;

        Id = default;
        ConnectionType = ConnectionType.CONNECTION_TYPE_BTLE;
        AutoCommunicationSessionReestablishment = default;
        Active = default;
        Available = default;
        Name = default;
        Model = default;
        FirmwareVersion = default;
        MFD = default;
        SerialNo = default;
        ScannerModelString = default;
    }
}
