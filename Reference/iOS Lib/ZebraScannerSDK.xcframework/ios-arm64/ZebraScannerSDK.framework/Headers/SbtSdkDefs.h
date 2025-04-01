/******************************************************************************
 *
 *       Copyright Zebra Technologies Corporation. 2013 - 2015
 *
 *       The copyright notice above does not evidence any
 *       actual or intended publication of such source code.
 *       The code contains Zebra Technologies
 *       Confidential Proprietary Information.
 *
 *
 *  Description:   SbtSdkDefs.h
 *
 *  Notes:
 *
 ******************************************************************************/

#ifndef _SBT_SDK_DEFS_
#define _SBT_SDK_DEFS_

/**
 * These constants are defined to represent result codes that can be returned by SDK API functions.
 */
typedef enum {
    /**
     *  A particular API function has completed successfully.
     */
    SBT_RESULT_SUCCESS                   = 0x00,
    /**
     *  A particular API function has completed unsuccessfully.
     */
    SBT_RESULT_FAILURE                   = 0x01,
    /**
     *  A particular API function has completed unsuccessfully because a specified scanner was not available.
     */
    SBT_RESULT_SCANNER_NOT_AVAILABLE     = 0x02,
    /**
     *  A particular API function has completed unsuccessfully because a specified scanner was not active.
     */
    SBT_RESULT_SCANNER_NOT_ACTIVE        = 0x03,
    /**
     *  A particular API function has completed unsuccessfully due to invalid input and/or output parameters.
     */
    SBT_RESULT_INVALID_PARAMS            = 0x04,
    /**
     *  A particular API function has completed unsuccessfully due to expiration of a response timeout during communication with a particular scanner.
     */
    SBT_RESULT_RESPONSE_TIMEOUT          = 0x05,
    /**
     *  A particular API function has completed unsuccessfully due to unsupported opcode.
     */
    SBT_RESULT_OPCODE_NOT_SUPPORTED      = 0x06,
    /**
     * A particular API function has failed due to unsupported scanner.
     */
    SBT_RESULT_SCANNER_NO_SUPPORT        = 0x07,
    /**
     * A particular API function has failed due to not setting up the bluetooth address.
     */
    SBT_RESULT_BTADDRESS_NOT_SET         = 0x08,
    /**
     * A particular API function has failed due to not connecting through STC pairing code.
     */
    SBT_RESULT_SCANNER_NOT_CONNECT_STC   = 0x09
} SBT_RESULT;

/**
 * These constants are defined to represent operating modes of  Zebra Scanner SDK for iOS.
 */
enum {
    /**
     * The SDK is able to communicate MFi supported scanners.
     */
    SBT_OPMODE_MFI                       = 0x01,
    /**
     * The SDK is able to communicate BT LE scanners.
     */
    SBT_OPMODE_BTLE                      = 0x02,
    /**
     * The SDK is able to communicate both BT LE & MFi supported scanners.
     */
    SBT_OPMODE_ALL                       = 0x03
};

/**
 * These constants are defined to represent communication modes of scanners.
 */
enum {
    /**
     * The SDK is unable to determine communication mode of a particular scanner.
     */
    SBT_CONNTYPE_INVALID                 = 0x00,
    /**
     * A particular scanner is in MFi mode.
     */
    SBT_CONNTYPE_MFI                     = 0x01,
    /**
     * A particular scanner is in BT LE mode.
     */
    SBT_CONNTYPE_BTLE                    = 0x02
};

/**
 * These constants are defined to represent notifications provided by Zebra Scanner SDK for iOS.
 */
enum {
    /**
     * "Barcode event" notification (reception of a particular barcode of particular type from a particular active scanner).
     */
    SBT_EVENT_BARCODE                    = (0x01),
    /**
     * "Image event" notification (triggered when an imaging scanner captures images in image mode).
     */
    SBT_EVENT_IMAGE                      = (0x01 << 1),
    /**
     * "Video event" notification (triggered when an imaging scanner captures video in video mode).
     */
    SBT_EVENT_VIDEO                      = (0x01 << 2),
    /**
     * "Device Arrival" notification (appearance of an available scanner).
     */
    SBT_EVENT_SCANNER_APPEARANCE         = (0x01 << 3),
    /**
     * "Device Disappeared" notification (disappearance of an available scanner).
     */
    SBT_EVENT_SCANNER_DISAPPEARANCE      = (0x01 << 4),
    /**
     * "Session Established" notification (appearance of a particular active scanner).
     */
    SBT_EVENT_SESSION_ESTABLISHMENT      = (0x01 << 5),
    /**
     * "Session Terminated" notification (disappearance of an active scanner).
     */
    SBT_EVENT_SESSION_TERMINATION        = (0x01 << 6),
    /**
     * "Raw data event" notification (triggered when an imaging scanner captures IDC data in IDC mode).
     */
    SBT_EVENT_RAW_DATA                   = (0x01 << 7),
    
    /**
     * "Hard trigger Pull and Release notification
     */
    SBT_EVENT_TRIGGER                    = (0x01 << 8)
};

/**
 * LED opcodes to be used in ``ZebraScannerSDK/ISbtSdkApi/sbtLedControl:aLedCode:forScanner:`` for CS4070 scanner.
 */
enum {
    /**
     * Opcode for Red colored LED
     */
    SBT_LEDCODE_RED                      = 0x00,
    /**
     * Opcode for Green colored LED
     */
    SBT_LEDCODE_GREEN                    = 0x01,
    /**
     * Opcode for Yellow colored LED
     */
    SBT_LEDCODE_YELLOW                   = 0x02,
    /**
     * Opcode for Amber colored LED
     */
    SBT_LEDCODE_AMBER                    = 0x03,
    /**
     * Opcode for Blue colored LED
     */
    SBT_LEDCODE_BLUE                     = 0x04
};

/**
 * Beeper opcodes to be used in ``ZebraScannerSDK/ISbtSdkApi/sbtBeepControl:forScanner:`` for CS4070 Scanner.
 */

enum {
    /**
     * Opcode for one high short beep
     */
    SBT_BEEPCODE_SHORT_HIGH_1            = 0x00,
    /**
     * Opcode for two high short beeps
     */
    SBT_BEEPCODE_SHORT_HIGH_2            = 0x01,
    /**
     * Opcode for three high short beeps
     */
    SBT_BEEPCODE_SHORT_HIGH_3            = 0x02,
    /**
     * Opcode for four high short beeps
     */
    SBT_BEEPCODE_SHORT_HIGH_4            = 0x03,
    /**
     * Opcode for five high short beeps
     */
    SBT_BEEPCODE_SHORT_HIGH_5            = 0x04,
    /**
     * Opcode for one low short beep
     */
    SBT_BEEPCODE_SHORT_LOW_1             = 0x05,
    /**
     * Opcode for two low short beeps
     */
    SBT_BEEPCODE_SHORT_LOW_2             = 0x06,
    /**
     * Opcode for three low short beeps
     */
    SBT_BEEPCODE_SHORT_LOW_3             = 0x07,
    /**
     * Opcode for four low short beeps
     */
    SBT_BEEPCODE_SHORT_LOW_4             = 0x08,
    /**
     * Opcode for one low short beeps
     */
    SBT_BEEPCODE_SHORT_LOW_5             = 0x09,
    /**
     * Opcode for one high long beep
     */
    SBT_BEEPCODE_LONG_HIGH_1             = 0x0A,
    /**
     * Opcode for two high long beeps
     */
    SBT_BEEPCODE_LONG_HIGH_2             = 0x0B,
    /**
     * Opcode for three high long beeps
     */
    SBT_BEEPCODE_LONG_HIGH_3             = 0x0C,
    /**
     * Opcode for four high long beeps
     */
    SBT_BEEPCODE_LONG_HIGH_4             = 0x0D,
    /**
     * Opcode for five high long beeps
     */
    SBT_BEEPCODE_LONG_HIGH_5             = 0x0E,
    /**
     * Opcode for one low long beep
     */
    SBT_BEEPCODE_LONG_LOW_1              = 0x0F,
    /**
     * Opcode for two low long beeps
     */
    SBT_BEEPCODE_LONG_LOW_2              = 0x10,
    /**
     * Opcode for three low long beeps
     */
    SBT_BEEPCODE_LONG_LOW_3              = 0x11,
    /**
     * Opcode for four low long beeps
     */
    SBT_BEEPCODE_LONG_LOW_4              = 0x12,
    /**
     * Opcode for five low long beeps
     */
    SBT_BEEPCODE_LONG_LOW_5              = 0x13,
    /**
     * Opcode for fast warble beep
     */
    SBT_BEEPCODE_FAST_WARBLE             = 0x14,
    /**
     * Opcode for slow warble beep
     */
    SBT_BEEPCODE_SLOW_WARBLE             = 0x15,
    /**
     * Opcode for high-low beep
     */
    SBT_BEEPCODE_MIX1_HIGH_LOW           = 0x16,
    /**
     * Opcode for low-high beep
     */
    SBT_BEEPCODE_MIX2_LOW_HIGH           = 0x17,
    /**
     * Opcode for high-low-high beep
     */
    SBT_BEEPCODE_MIX3_HIGH_LOW_HIGH      = 0x18,
    /**
     * Opcode for low-high-low beep
     */
    SBT_BEEPCODE_MIX4_LOW_HIGH_LOW       = 0x19
};

/// Invalid scanner id
static int const SBT_SCANNER_ID_INVALID                                     =      0;

/// Op-codes to be used in ``ZebraScannerSDK/ISbtSdkApi/sbtExecuteCommand:aInXML:aOutXML:forScanner:`` method available in ``ZebraScannerSDK/ISbtSdkApi`` protocol
enum
{
    /* Supported in accordance to SRS */
    /* implemented */
    /**
     * Opcode to pull the soft trigger
     */
    SBT_DEVICE_PULL_TRIGGER                     = 0x7DB,	//2011
    /**
     * Opcode to release the soft trigger
     */
	SBT_DEVICE_RELEASE_TRIGGER                  = 0x7DC,	//2012
    /**
     * Opcode to disable barcode scanning
     */
	SBT_DEVICE_SCAN_DISABLE                     = 0x7DD,	//2013
    /**
     * Opcode to enable barcode scanning
     */
	SBT_DEVICE_SCAN_ENABLE                      = 0x7DE,	//2014
    /**
     * Opcode to turn on the image mode of the scanner
     */
    SBT_DEVICE_CAPTURE_IMAGE                    = 0xBB8,	//3000
    /**
     * Opcode to turn on the barcode mode of the scanner
     */
    SBT_DEVICE_CAPTURE_BARCODE                  = 0xDAC,	//3500
    /**
     * Opcode to turn on the video mode of the scanner
     */
	SBT_DEVICE_CAPTURE_VIDEO                    = 0xFA0,	//4000
    /**
     * Opcode to get all attributes available in the scanner
     */
	SBT_RSM_ATTR_GETALL                         = 0x1388,	//5000
    /**
     * Opcode to get value of single or multiple attributes available in the scanner
     */
	SBT_RSM_ATTR_GET                            = 0x1389,	//5001
    /**
     * Opcode to get the offset value of an attribute
     */
    SBT_RSM_ATTR_GET_OFFSET                     = 0x138B,	//5003
    /**
     * Opcode to set value of a single or multiple attributes available in the scanner
     */
    SBT_RSM_ATTR_SET                            = 0x138C,	//5004
    /**
     * Opcode to store value of a single or multiple attributes available in the scanner
     */
	SBT_RSM_ATTR_STORE                          = 0x138D,	//5005
    /**
     * Opcode to set an action like beep or LED in scanner
     */
	SBT_SET_ACTION                              = 0x1770,	//6000

    
    /* Supported in accordance to SRS */
    /* not implemented */
    
    /**
     * Opcode to start the updated firmware of the scanner
     */
    SBT_START_NEW_FIRMWARE                      = 0x1396,	//5014
    /**
     * Opcode to update firmware of the scanner
     */
	SBT_UPDATE_FIRMWARE                         = 0x1398,	//5016
    /**
     * Opcode to be used in ``ZebraScannerSDK/ISbtSdkApi/sbtExecuteCommand:aInXML:aOutXML:forScanner:`` while using a scanner plugin for firmware update
     */
    SBT_UPDATE_FIRMWARE_FROM_PLUGIN             = 0x1399,	//5017
    /**
     * Opcode to abort an ongoing firmware update
     */
    SBT_DEVICE_ABORT_UPDATE_FIRMWARE            = 0x7D1,	//2001
    /**
     * Opcode to turn off the aim guide of the scanner
     */
	SBT_DEVICE_AIM_OFF                          = 0x7D2,	//2002
    /**
     * Opcode to turn on the aim guide of the scanner
     */
	SBT_DEVICE_AIM_ON                           = 0x7D3,	//2003
    /* New features required for 1.2 release */
    /**
     * Opcode to vibrate the pager motor of the scanner
     */
    SBT_DEVICE_VIBRATION_FEEDBACK               = 0X7E4,    //2020
    
};

/// These constants are defined to represent result codes that can be returned by firmware update API function.
///
/// These are the results returned as the ``ZebraScannerSDK/FirmwareUpdateEvent/status``  in ``ZebraScannerSDK/FirmwareUpdateEvent`` when triggering ``ZebraScannerSDK/ISbtSdkApiDelegate/sbtEventFirmwareUpdate:`` API.
typedef enum {
    /**
     * Firmware Update API has completed successfully.
     */
    SBT_FW_UPDATE_RESULT_SUCCESS                   = 0x00,
    /**
     * Firmware Update API has failed.
     */
    SBT_FW_UPDATE_RESULT_FAILURE                   = 0x01,
} SBT_FW_UPDATE_RESULT;

/**
 * Bluetooth Protocol to be used in pairing barcode. Legacy B format will not change the scanners com protocol.
 */
typedef enum {
    /**
     * Simple Serial Interface (SSI) BT with MFi (iOS Support)
     */
    STC_SSI_MFI                   = 0x00,
    /**
     * Simple Serial Interface (SSI) BT Low energy
     */
    STC_SSI_BLE                   = 0x01,
    /**
     * ***DEPRECATED***
     */
    SBT_SSI_HID                   = 0x02,
    
    NO_COM_PROTOCOL               = 0x03
} STC_COM_PROTOCOL;

/**
 * Specifies scanner configuration types to be used for pairing barcode generation.
 */
typedef enum {
    /**
     * Set factory default configurations for the pairing barcode
     */
    SETDEFAULT_YES                   = 0x00,
    /**
     * Keep scanner's current configurations for the pairing barcode
     */
    SETDEFAULT_NO                    = 0x01,
} SETDEFAULT_STATUS;

/**
 * Pairing barcode type to be used in  ``ZebraScannerSDK/ISbtSdkApi/sbtGetPairingBarcode:withComProtocol:withSetDefaultStatus:withBTAddress:withImageFrame:`` to generate the pairing barcode.
 */
typedef enum {
    /**
     * Scan-To-Connect Barcode Type
     */
    BARCODE_TYPE_STC = 0x01,
    /**
     * ***DEPRECATED***
     */
    BARCODE_TYPE_LEGACY = 0x00 ,


} BARCODE_TYPE;




typedef enum {
    
    SBT_TRIGGER_PULLED             = 0x00,
    SBT_TRIGGER_RELEASED           = 0x01
    
} SBT_TRIGGER_STATE;


#endif /* _SBT_SDK_DEFS_ */
