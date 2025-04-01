using Com.Zebra.Scannercontrol;

namespace ZebraBarcodeScannerSDK;

public partial class Scanner
{
    public Scanner(DCSScannerInfo dCSScannerInfo, AppEngine appEngine)
    {
        this.appEngine = appEngine;
        Id = dCSScannerInfo.ScannerID;

        DCSSDKDefs.DCSSDK_CONN_TYPES? connectionType = dCSScannerInfo.ConnectionType;
        if (connectionType == DCSSDKDefs.DCSSDK_CONN_TYPES.DcssdkConntypeBtLe)
            ConnectionType = ConnectionType.CONNECTION_TYPE_BTLE;
        else if (connectionType == DCSSDKDefs.DCSSDK_CONN_TYPES.DcssdkConntypeBtNormal)
            ConnectionType = ConnectionType.CONNECTION_TYPE_SSI;
        else if (connectionType == DCSSDKDefs.DCSSDK_CONN_TYPES.DcssdkConntypeUsbSnapi)
            ConnectionType = ConnectionType.CONNECTION_TYPE_SNAPI;
        else if (connectionType == DCSSDKDefs.DCSSDK_CONN_TYPES.DcssdkConntypeUsbCdc)
            ConnectionType = ConnectionType.CONNECTION_TYPE_USB_CDC;

        AutoCommunicationSessionReestablishment = dCSScannerInfo.IsAutoCommunicationSessionReestablishment;
        Active = dCSScannerInfo.IsActive;
        Name = dCSScannerInfo.ScannerName;
        Model = dCSScannerInfo.ScannerModel;
        SerialNo = dCSScannerInfo.ScannerHWSerialNumber;
    }
}