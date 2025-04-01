/******************************************************************************
 *
 *       Copyright Zebra Technologies Corporation. 2014 - 2015
 *
 *       The copyright notice above does not evidence any
 *       actual or intended publication of such source code.
 *       The code contains Zebra Technologies
 *       Confidential Proprietary Information.
 *
 *
 *  Description:   RMDAttributes.h
 *
 *  Notes:
 *
 ******************************************************************************/

#ifndef __RMD_ATTRIBUTES_H__
#define __RMD_ATTRIBUTES_H__

/* nrv364: in accordance with Motorola Scanner SDK Attribute Data Dictionary 72E-149786-01 */
/* attribute identifiers */

/// Model number of the scanner which matches the label of the device. For example: “LS4208-SR20001ZZ”
static int const RMD_ATTR_MODEL_NUMBER                                          =      533;

/// Unique serial number assigned at time of manufacture. For example: “M1J26F45V”

static int const RMD_ATTR_SERIAL_NUMBER                                         =      534;

/// Unique serial number assigned at time of manufacture. For example: “M1J26F45V”
static int const RMD_ATTR_ID_SERIAL_NUMBER                                      =      0x0216; //534

/// The parameter defines the friendly name displayed by Bluetooth remote devices.
static int const RMD_ATTR_ID_NAME                                               =      0x025F; //607

/// The parameter defines the volume level set of the Beeper.
static int const RMD_ATTR_BEEPER_VOLUME                                         =      140;

/// The parameter defines the frequency level set of the Beeper.
static int const RMD_ATTR_BEEPER_FREQUENCY                                      =      145;

/// Triggers the beeper/LED via command.
static int const RMD_ATTR_ACTION_BEEPER_LED                                     =      6000;

/// This parameter enables the decoding of UPC-A.
static int const RMD_ATTR_SYM_UPC_A                                             =      1;

/// This parameter enables the decoding of UPC-E.
static int const RMD_ATTR_SYM_UPC_E                                             =      2;

/// This parameter enables the decoding of UPC-E1.
static int const RMD_ATTR_SYM_UPC_E_1                                           =      12;

/// This parameter enables the decoding of EAN-8/JAN 8.
static int const RMD_ATTR_SYM_EAN_8_JAN_8                                       =      4;

/// This parameter enables the decoding of EAN-13/JAN 13.
static int const RMD_ATTR_SYM_EAN_13_JAN_13                                     =      3;

/// This parameter enables the decoding of Bookland EAN.
static int const RMD_ATTR_SYM_BOOKLAND_EAN                                      =      83;

/// This parameter enables the decoding of Code 128.
static int const RMD_ATTR_SYM_CODE_128                                          =      8;

/// This parameter enables the decoding of UCC/EAN-128.
static int const RMD_ATTR_SYM_UCC_EAN_128                                       =      14;

/// This parameter enables the decoding of Code 39.
static int const RMD_ATTR_SYM_CODE_39                                           =      0;

/// This parameter enables the decoding of Code 93.
static int const RMD_ATTR_SYM_CODE_93                                           =      9;

/// This parameter enables the decoding of Code 11.
static int const RMD_ATTR_SYM_CODE_11                                           =      10;

/// This parameter enables the decoding of Interleaved 2 of 5.
static int const RMD_ATTR_SYM_INTERLEAVED_2_OF_5                                =      6;

/// This parameter enables the decoding of Discrete 2 of 5.
static int const RMD_ATTR_SYM_DISCRETE_2_OF_5                                   =      5;

/// This parameter enables the decoding of Chinese 2 of 5.
static int const RMD_ATTR_SYM_CHINESE_2_OF_5                                    =      408;

/// This parameter enables the decoding of Codabar.
static int const RMD_ATTR_SYM_CODABAR                                           =      7;

/// This parameter enables the decoding of MSI.
static int const RMD_ATTR_SYM_MSI                                               =      11;

/// This parameter enables the decoding of DataMatrix.
static int const RMD_ATTR_SYM_DATAMATRIXQR                                      =      292;

/// This parameter enables the decoding of PDF.
static int const RMD_ATTR_SYM_PDF                                               =      15;

/// This parameter enables ISBT 128 (non-concatenated). If concatenation is necessary, the host must perform the process.
static int const RMD_ATTR_SYM_ISBT_128                                          =      84;

/// When enabled, this parameter decodes UPCA bar codes starting with digit ‘5’, EAN-13 bar codes starting with digit ‘99’, and UPCA/
/// EAN-128 Coupon Codes. UPCA, EAN-13 and EAN-128 must be enabled to scan all types of Coupon Codes.
static int const RMD_ATTR_UCC_COUPEN_EXTENDED_CODE                              =      85;

/// Top level release name of products which contain multiple “packaged” software versions. For example: “CAAAACS00-001-N01D9”
static int const RMD_ATTR_FRMWR_VERSION                                         =      20012;

/// Date of Device Manufacture. For example: “15APR05”
static int const RMD_ATTR_MFD                                                   =      535;

/// This parameter enables reboot and unpair option.
static int const RMD_ATTR_REBOOT_AND_UNPAIR                                     =      6045;

/// This parameter enables the decoding of USPost1.
static int const RMD_ATTR_SYM_US_Postnet                                        =      89;

/// This parameter enables the decoding of USPost2.
static int const RMD_ATTR_SYM_US_Planet                                         =      90;

/// This parameter enables the decoding of UKPost.
static int const RMD_ATTR_SYM_UK_POST                                           =      91;

/// This parameter enables the parity option of USPostal barcodes.
static int const RMD_ATTR_SYM_US_POSTAL_CHECK_DIGIT                             =      95;

/// This parameter enables the parity option of UKPostal barcodes.
static int const RMD_ATTR_SYM_UK_POSTAL_CHECK_DIGIT                             =      96;

/// This parameter enables the parity option of Japan Post barcodes.
static int const RMD_ATTR_SYM_JAPAN_POST                                        =      290;

/// This parameter enables the parity option of Australia Post barcodes.
static int const RMD_ATTR_SYM_AUS_POST                                          =      291;

/// This parameter enables the decoding of Netherlands KIX Code.
static int const RMD_ATTR_SYM_Netherlands_KIX                                   =      326;

/// This parameter enables the decoding of UPU FICS Postal.
static int const RMD_ATTR_SYM_UPU_FICS                                          =      611;

/// This parameter enables the decoding of USPS 4CB/One Code/Intelligent Mail
static int const RMD_ATTR_SYM_USPS_4CB_ONECODE_INTELLIGENT_MAIL                 =      592;

/// This parameter enables the decoding of GS1 DataBar-14 and its stacked variant.
static int const RMD_ATTR_SYM_GS1_DATABAR_14                                    =      338;

/// This parameter enables the decoding of GS1 DataBar Limited.
static int const RMD_ATTR_SYM_GS1_DATABAR_LIMITED                               =      339;

/// This parameter enables the decoding of GS1 DataBar Expanded and its stacked variant.
static int const RMD_ATTR_SYM_GS1_DATABAR_EXPANDED                              =      340;

/// This parameter enables the decoding of MicroPDF.
static int const RMD_ATTR_SYM_MICRO_PDF                                         =      227;

/// This parameter enables the decoding of Maxicode.
static int const RMD_ATTR_SYM_MAXI_CODE                                         =      294;

/// This enables ISSN EAN decoding.
static int const RMD_ATTR_ISSN_EAN                                              =      617;

/// This parameter enables the decoding of Matrix 2 of 5.
static int const RMD_ATTR_MATRIX_2_OF_5                                         =      618;

/// This parameter enables the decoding of Korean 2 of 5.
static int const RMD_ATTR_KOREAN_3_OF_5                                         =      581;

/// This parameter enables the decoding of QR.
static int const RMD_ATTR_QR_CODE                                               =      293;

/// This parameter enables the decoding of MicroQR.
static int const RMD_ATTR_MICRO_QR                                              =      573;

/// This parameter enables the decoding of Aztec.
static int const RMD_ATTR_AZTEC                                                 =      574;

/// This parameter enables the decoding of DotCode.
static int const RMD_ATTR_SYM_DOT_CODE                                          =      1906;

/// This parameter enables the decoding of Han Xin.
static int const RMD_ATTR_HANXIN                                                =      1167;

/// This parameter enables the decoding of composite bar codes of type CC-C
static int const RMD_ATTR_COMPOSITE_CC_C                                        =      341;

/// This parameter enables the decoding of composite bar codes of type CC-A/B.
static int const RMD_ATTR_COMPOSITE_CC_A_B                                      =      342;

/// This parameter enables the decoding of composite bar codes of type TLC-39
static int const RMD_ATTR_COMPOSITE_TLC_39                                      =      371;

/* attribute values */

/// Attribute value "Low" of ``ZebraScannerSDK/RMD_ATTR_BEEPER_VOLUME`` attribute.
static int const RMD_ATTR_VALUE_BEEPER_VOLUME_LOW                               =      2;

/// Attribute value "Medium" of ``ZebraScannerSDK/RMD_ATTR_BEEPER_VOLUME`` attribute.
static int const RMD_ATTR_VALUE_BEEPER_VOLUME_MEDIUM                            =      1;

/// Attribute value "High" of ``ZebraScannerSDK/RMD_ATTR_BEEPER_VOLUME`` attribute.
static int const RMD_ATTR_VALUE_BEEPER_VOLUME_HIGH                              =      0;

/// Attribute value "Low" of ``ZebraScannerSDK/RMD_ATTR_BEEPER_FREQUENCY`` attribute.
static int const RMD_ATTR_VALUE_BEEPER_FREQ_LOW                                 =      2;

/// Attribute value "Medium" of ``ZebraScannerSDK/RMD_ATTR_BEEPER_FREQUENCY`` attribute.
static int const RMD_ATTR_VALUE_BEEPER_FREQ_MEDIUM                              =      1;

/// Attribute value "High" of ``ZebraScannerSDK/RMD_ATTR_BEEPER_FREQUENCY`` attribute.
static int const RMD_ATTR_VALUE_BEEPER_FREQ_HIGH                                =      0;

/// Action attribute value for "1 high short beep".
static int const RMD_ATTR_VALUE_ACTION_HIGH_SHORT_BEEP_1                        =      0;

/// Action attribute value for "2 high short beeps".
static int const RMD_ATTR_VALUE_ACTION_HIGH_SHORT_BEEP_2                        =      1;

/// Action attribute value for "3 high short beeps".
static int const RMD_ATTR_VALUE_ACTION_HIGH_SHORT_BEEP_3                        =      2;

/// Action attribute value for "4 high short beeps".
static int const RMD_ATTR_VALUE_ACTION_HIGH_SHORT_BEEP_4                        =      3;

/// Action attribute value for "5 high short beeps".
static int const RMD_ATTR_VALUE_ACTION_HIGH_SHORT_BEEP_5                        =      4;

/// Action attribute value for "1 low short beep".
static int const RMD_ATTR_VALUE_ACTION_LOW_SHORT_BEEP_1                         =      5;

/// Action attribute value for "2 low short beeps".
static int const RMD_ATTR_VALUE_ACTION_LOW_SHORT_BEEP_2                         =      6;

/// Action attribute value for "3 low short beeps".
static int const RMD_ATTR_VALUE_ACTION_LOW_SHORT_BEEP_3                         =      7;

/// Action attribute value for "4 low short beeps".
static int const RMD_ATTR_VALUE_ACTION_LOW_SHORT_BEEP_4                         =      8;

/// Action attribute value for "5 low short beeps".
static int const RMD_ATTR_VALUE_ACTION_LOW_SHORT_BEEP_5                         =      9;

/// Action attribute value for "1 high long beep".
static int const RMD_ATTR_VALUE_ACTION_HIGH_LONG_BEEP_1                         =      10;

/// Action attribute value for "2 high long beeps".
static int const RMD_ATTR_VALUE_ACTION_HIGH_LONG_BEEP_2                         =      11;

/// Action attribute value for "3 high long beeps".
static int const RMD_ATTR_VALUE_ACTION_HIGH_LONG_BEEP_3                         =      12;

/// Action attribute value for "4 high long beeps".
static int const RMD_ATTR_VALUE_ACTION_HIGH_LONG_BEEP_4                         =      13;

/// Action attribute value for "5 high long beeps".
static int const RMD_ATTR_VALUE_ACTION_HIGH_LONG_BEEP_5                         =      14;

/// Action attribute value for "1 low long beep".
static int const RMD_ATTR_VALUE_ACTION_LOW_LONG_BEEP_1                          =      15;

/// Action attribute value for "2 low long beeps".
static int const RMD_ATTR_VALUE_ACTION_LOW_LONG_BEEP_2                          =      16;

/// Action attribute value for "3 low long beeps".
static int const RMD_ATTR_VALUE_ACTION_LOW_LONG_BEEP_3                          =      17;

/// Action attribute value for "4 low long beeps".
static int const RMD_ATTR_VALUE_ACTION_LOW_LONG_BEEP_4                          =      18;

/// Action attribute value for "5 low long beeps".
static int const RMD_ATTR_VALUE_ACTION_LOW_LONG_BEEP_5                          =      19;

/// Action attribute value for "Fast warble beep".
static int const RMD_ATTR_VALUE_ACTION_FAST_WARBLE_BEEP                         =      20;

/// Action attribute value for "Slow warble beep".
static int const RMD_ATTR_VALUE_ACTION_SLOW_WARBLE_BEEP                         =      21;

/// Action attribute value for "High-Low beep".
static int const RMD_ATTR_VALUE_ACTION_HIGH_LOW_BEEP                            =      22;

/// Action attribute value for "Low-High beep".
static int const RMD_ATTR_VALUE_ACTION_LOW_HIGH_BEEP                            =      23;

/// Action attribute value for "High-Low-High beep".
static int const RMD_ATTR_VALUE_ACTION_HIGH_LOW_HIGH_BEEP                       =      24;

/// Action attribute value for "Low-High-Low beep".
static int const RMD_ATTR_VALUE_ACTION_LOW_HIGH_LOW_BEEP                        =      25;

/// Action attribute value for "High-High-Low-Low beep".
static int const RMD_ATTR_VALUE_ACTION_HIGH_HIGH_LOW_LOW_BEEP                   =      26;

/// Action attribute value to turn off green LED.
static int const RMD_ATTR_VALUE_ACTION_LED_GREEN_OFF                            =      42;

/// Action attribute value to turn on green LED.
static int const RMD_ATTR_VALUE_ACTION_LED_GREEN_ON                             =      43;

/// Action attribute value to turn on yellow LED.
static int const RMD_ATTR_VALUE_ACTION_LED_YELLOW_ON                            =      45;

/// Action attribute value to turn off yellow LED.
static int const RMD_ATTR_VALUE_ACTION_LED_YELLOW_OFF                           =      46;

/// Action attribute value to turn on red LED.
static int const RMD_ATTR_VALUE_ACTION_LED_RED_ON                               =      47;

/// Action attribute value to turn off red LED.
static int const RMD_ATTR_VALUE_ACTION_LED_RED_OFF                              =      48;

/// Action attribute value to turn on fast blink.
static int const RMD_ATTR_VALUE_ACTION_FAST_BLINK                               =      85;

/// Action attribute value to turn off fast blink.
static int const RMD_ATTR_VALUE_ACTION_FAST_BLINK_OFF                           =      90;

/// Action attribute value to turn on reboot and unpair option.
static int const RMD_ATTR_VALUE_ACTION_REBOOT_AND_UNPAIR                        =      1;

/// Action attribute value to turn on virtual tether simulation.
static int const RMD_ATTR_VALUE_ACTION_VIRTUAL_TETHER_SIMULATION_ENABLE         =      232;

/// Action attribute value to turn off virtual tether simulation.
static int const RMD_ATTR_VALUE_ACTION_VIRTUAL_TETHER_SIMULATION_DISABLE        =      233;

/// The parameter defines re-connect option for when the digital scanner loses its connection with a remote device:
static int const RMD_ATTR_VALUE_AUTO_RECONNECT                                  =      604;

/// This parameter defines parameter barcode decoding option.
static int const RMD_ATTR_VALUE_PARAMETER_BARCODE                               =      236;

/// This parameter is used to change the friendly name of a scanner.
static int const RSM_ATTR_FRIENDLY_NAME                                         =      607;

/// This parameter is used to enable/ disable system event notifications like trigger pull and trigger release.
static int const RSM_ATTR_SYSTEM_EVENT                                         =      257;

/// This parameter defines the battery manufacture date.
static int const RMD_ATTR_BATTERY_MANUFACTURE_DATE                = 30018;

/// This parameter defines the battery serial number.
static int const RMD_ATTR_BATTERY_SERIAL_NUMBER                   = 30030;

/// This parameter defines the battery model.
static int const RMD_ATTR_BATTERY_MODEL_NUMBER                    = 30017;

/// This parameter defines the battery firmware version..
static int const RMD_ATTR_BATTERY_FIRMWARE_VERSION                = 30019;

/// This parameter defines the design capacity of the battery.
static int const RMD_ATTR_BATTERY_DESIGN_CHARGE_CAPACITY          = 30029;

/// This parameter defines the battery health status.
static int const RMD_ATTR_BATTERY_HEALTH                          = 30013;

/// This parameter defines the consumed charged cycles of the battery.
static int const RMD_ATTR_BATTERY_CHARGE_CYCLES                   = 30021;

/// This parameter defines the battery full charge capacity.
static int const RMD_ATTR_BATTERY_FULL_CHARGE_CAPACITY            = 30020;

/// This parameter defines the state of charge of the battery.
static int const RMD_ATTR_BATTERY_CHARGE_PERCENTAGE               = 30012;

/// This parameter defines the battery current.
static int const RMD_ATTR_BATTERY_CURRENT                         = 30011;

/// This parameter defines parameter barcode decoding option.
static int const RMD_ATTR_BATTERY_CHARGE_STATUS                   = 30026;

/// This parameter defines the remaining time to complete charging .
static int const RMD_ATTR_BATTERY_TIME_TO_FULL_CHARGE_COUNT       = 30023;

/// This parameter defines battery voltage.
static int const RMD_ATTR_BATTERY_VOLTAGE                         = 30010;

/// This parameter defines the battery remaining charge capacity.
static int const RMD_ATTR_BATTERY_REMAINING_CHARGE_CAPACITY       = 30027;

/// This parameter defines the battery present temperature.
static int const RMD_ATTR_BATTERY_CURRENT_TEMPERATURE             = 30016;

/// This parameter defines the battery highest temperature
static int const RMD_ATTR_BATTERY_HIGHEST_TEMPERATURE             = 30024;

/// This parameter defines the battery lowest temperature
static int const RMD_ATTR_BATTERY_LOWEST_TEMPERATURE              = 30025;

/// This parameter defines the enable/disable status of the pick list mode.
static int const RMD_ATTR_PICKLIST_MODE                          = 402;

/// This parameter is used to set the duration of the vibration.
static int const RMD_ATTR_VIBRATION_DURATION                     = 626;

/// This parameter defines the status of the scanner configuration.
static int const RMD_ATTR_CONFIG_FILE_NAME                       = 616;

/// This parameter defines the enable/disable status of the virtual tether alarm.
static int const RMD_ATTR_VIRTUAL_TETHER_ALARM_STATUS            = 2053;
       
/// Attribute value for enabling ``ZebraScannerSDK/RMD_ATTR_VIRTUAL_TETHER_ALARM_STATUS`` attribute.
static int const RMD_ATTR_VALUE_VIRTUAL_TETHER_ALARM_ENABLE      = 1;

/// Attribute value for disabling ``ZebraScannerSDK/RMD_ATTR_VIRTUAL_TETHER_ALARM_STATUS`` attribute.
static int const RMD_ATTR_VALUE_VIRTUAL_TETHER_ALARM_DISABLE     = 0;

#endif /* __RMD_ATTRIBUTES_H__ */
