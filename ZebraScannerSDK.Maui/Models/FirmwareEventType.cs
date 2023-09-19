namespace ZebraBarcodeScannerSDK;

public enum FirmwareEventType
{
    SESSION_START = 11, // 0x0000000B
    DL_START = 12, // 0x0000000C
    DL_PROGRESS = 13, // 0x0000000D
    DL_END = 14, // 0x0000000E
    SESSION_END = 15, // 0x0000000F
    FIRMWARE_UPDATE_STATUS = 16, // 0x00000010
}