namespace ZebraBarcodeScannerSDK;

internal class Constants
{
    // available values for 'status' //
    public const int STATUS_SUCCESS = 0;
    public const int STATUS_FALSE = 1;
    public const int STATUS_LOCKED = 10;
    public const int ERROR_CDC_SCANNERS_NOT_FOUND = 150;
    public const int ERROR_UNABLE_TO_OPEN_CDC_COM_PORT = 151;

    // Scanner types
    public const short SCANNER_TYPES_ALL = 1;
    public const short SCANNER_TYPES_SNAPI = 2;
    public const short SCANNER_TYPES_SSI = 3;
    public const short SCANNER_TYPES_RSM = 4;
    public const short SCANNER_TYPES_IMAGING = 5;
    public const short SCANNER_TYPES_IBMHID = 6;
    public const short SCANNER_TYPES_NIXMODB = 7;
    public const short SCANNER_TYPES_HIDKB = 8;
    public const short SCANNER_TYPES_IBMTT = 9;
    public const short SCALE_TYPES_IBM = 10;
    public const short SCALE_TYPES_SSI_BT = 11;
    public const short CAMERA_TYPES_UVC = 14;

    // Total number of scanner types
    public const short TOTAL_SCANNER_TYPES = CAMERA_TYPES_UVC;
}