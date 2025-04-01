using Symbolbtscannersdk;

namespace ZebraBarcodeScannerSDK;

public partial class Scanner
{
    public Scanner(SbtScannerInfo scannerInfo, AppEngine appEngine)
    {
        this.appEngine = appEngine;
        Id = scannerInfo.ScannerID;
        ConnectionType = (object)scannerInfo.ConnectionType switch
        {
            1 => ConnectionType.CONNECTION_TYPE_MFI,
            2 => ConnectionType.CONNECTION_TYPE_BTLE,
            3 => ConnectionType.CONNECTION_TYPE_MFI_BTLE,
            _ => ConnectionType.CONNECTION_TYPE_MFI_BTLE,
        };
        AutoCommunicationSessionReestablishment = scannerInfo.AutoCommunicationSessionReestablishment;
        Active = scannerInfo.IsActive;
        Available = scannerInfo.IsAvailable;
        Name = scannerInfo.ScannerName;
        Model = scannerInfo.ScannerModel.ToString();
        FirmwareVersion = scannerInfo.FirmwareVersion;
        MFD = scannerInfo.MFD;
        SerialNo = scannerInfo.SerialNo;
        ScannerModelString = scannerInfo.ScannerModelString;
    }
}