using Com.Zebra.Scannercontrol;

namespace ZebraBarcodeScannerSDK;

public partial class Scanner
{
    public Scanner(DCSScannerInfo dCSScannerInfo, AppEngine appEngine)
    {
        this.appEngine = appEngine;
        id = dCSScannerInfo.ScannerID;

        DCSSDKDefs.DCSSDK_CONN_TYPES? connectionType = dCSScannerInfo.ConnectionType;
        if (connectionType == DCSSDKDefs.DCSSDK_CONN_TYPES.DcssdkConntypeBtLe)
            this.connectionType = ConnectionType.CONNECTION_TYPE_BTLE;
        else if (connectionType == DCSSDKDefs.DCSSDK_CONN_TYPES.DcssdkConntypeBtNormal)
            this.connectionType = ConnectionType.CONNECTION_TYPE_SSI;
        else if (connectionType == DCSSDKDefs.DCSSDK_CONN_TYPES.DcssdkConntypeUsbSnapi)
            this.connectionType = ConnectionType.CONNECTION_TYPE_SNAPI;
        else if (connectionType == DCSSDKDefs.DCSSDK_CONN_TYPES.DcssdkConntypeUsbCdc)
            this.connectionType = ConnectionType.CONNECTION_TYPE_USB_CDC;

        autoCommunicationSessionReestablishment = dCSScannerInfo.IsAutoCommunicationSessionReestablishment;
        active = dCSScannerInfo.IsActive;
        name = dCSScannerInfo.ScannerName;
        model = dCSScannerInfo.ScannerModel;
        serialNumber = dCSScannerInfo.ScannerHWSerialNumber;
    }
}