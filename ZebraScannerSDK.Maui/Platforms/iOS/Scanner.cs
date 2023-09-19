using Symbolbtscannersdk;

namespace ZebraBarcodeScannerSDK;

public partial class Scanner
{
    public Scanner(SbtScannerInfo scannerInfo, AppEngine appEngine)
    {
        this.appEngine = appEngine;
        id = scannerInfo.ScannerID;
        switch (scannerInfo.ConnectionType)
        {
            case 1:
                connectionType = ConnectionType.CONNECTION_TYPE_MFI;
                break;

            case 2:
                connectionType = ConnectionType.CONNECTION_TYPE_BTLE;
                break;

            case 3:
                connectionType = ConnectionType.CONNECTION_TYPE_MFI_BTLE;
                break;

            default:
                connectionType = ConnectionType.CONNECTION_TYPE_MFI_BTLE;
                break;
        }

        autoCommunicationSessionReestablishment = scannerInfo.AutoCommunicationSessionReestablishment;
        active = scannerInfo.IsActive;
        available = scannerInfo.IsAvailable;
        name = scannerInfo.ScannerName;
        model = scannerInfo.ScannerModel.ToString();
        firmwareVersion = scannerInfo.FirmwareVersion;
        manufactureDate = scannerInfo.MFD;
        serialNumber = scannerInfo.SerialNo;
        scannerModelName = scannerInfo.ScannerModelString;
    }
}