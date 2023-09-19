namespace ZebraBarcodeScannerSDK;

public enum BluetoothProtocol
{
    LEGACY_B = 1,
    CRD_BT = 12, // 0x0000000C
    SSI_BT_SSI_SLAVE = 13, // 0x0000000D
    SPP_BT_MASTER = 14, // 0x0000000E
    SPP_BT_SLAVE = 15, // 0x0000000F
    HID_BT = 17, // 0x00000011
    SSI_BT_MFI = 19, // 0x00000013
    HID_BT_LE = 20, // 0x00000014
    CRD_BT_LE = 21, // 0x00000015
    SSI_BT_CRADLE_HOST = 22, // 0x00000016
    SSI_BT_LE = 23, // 0x00000017
}