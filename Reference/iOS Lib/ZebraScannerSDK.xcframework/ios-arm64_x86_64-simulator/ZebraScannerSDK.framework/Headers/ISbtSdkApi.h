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
 *  Description:   ISbtSdkApi.h
 *
 *  Notes:
 *
 ******************************************************************************/

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>
#import <ZebraScannerSDK/SbtSdkDefs.h>
#import <ZebraScannerSDK/ISbtSdkApiDelegate.h>


/// A protocol where all the available Zebra Scanner SDK  APIs  are defined.
@protocol ISbtSdkApi <NSObject>

/// Registers a particular object which conforms to ISbtSdkApiDelegate protocol as a receiver of Zebra Scanner SDK notifications.
///
/// It is required to registration of a particular object which conforms to ISbtSdkApiDelegate protocol to receive notifications from the SDK.
///
/// - Parameter delegate: A  class that implements the ISbtSdkApiDelegate protocol
/// 
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtSetDelegate:(id<ISbtSdkApiDelegate>)delegate;

/// Get the Zebra Scanner SDK version
///
/// - Returns: SDK version
- (NSString*) sbtGetVersion;

/// Set the required operational mode of the scanners
///
/// Zebra Scanner SDK for iOS is designed to support interaction with scanners operating in BT MFi or BT LE mode. The SDK should be configured to enable communication with a particular type of scanner by setting the operation mode.
///
/// - Parameter operationalMode: Operational mode to be set
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtSetOperationalMode:(int)operationalMode;

/// Subscribe for specific asynchronous events/ notifications which are sent by Zebra Scanner SDK
///
/// Zebra Scanner SDK will send notifications for subscribed events only
///
/// - Parameter sdkEventsMask: Event mask which decides the subscription status
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtSubsribeForEvents:(int)sdkEventsMask;


/// Disable  events/ notifications of subscribed types.
///
/// - Parameter sdkEventsMask: Event mask which decides the unsubscription status
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtUnsubsribeForEvents:(int)sdkEventsMask;

/// Fetch a list of available scanners from the Zebra Scanner SDK
///
/// Application is responsible for allocation and deallocation of the array. An "available" scanner is a scanner that is connected to the iOS device via Bluetooth, but has not yet established a communication session with the SDK
///
/// - Parameter availableScannersList: A pointer variable of NSMutableArray type
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtGetAvailableScannersList:(NSMutableArray**)availableScannersList;

/// Fetch a list of active scanners from the Zebra Scanner SDK
///
/// Application is responsible for allocation and deallocation of the array.  An "active" scanner is a scanner that is both connected to the iOS device via Bluetooth, and has established a communication session with the SDK.
///
/// - Parameter activeScannersList: A pointer variable of NSMutableArray type
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtGetActiveScannersList:(NSMutableArray**)activeScannersList;


/// Establish communication session with a particular available scanner
///
/// - Parameter scannerID: Unique identifier of a particular scanner assigned by the SDK
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtEstablishCommunicationSession:(int)scannerID;

///  Terminate communication session with a particular active scanner.
///
/// - Parameter scannerID: Unique identifier of a particular scanner assigned by the SDK
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtTerminateCommunicationSession:(int)scannerID;

/// Method to actively detect appearance and disappearance of scanners
///
///The SDK supports automatic detection of appearance and disappearance of available scanners. When the "Available scanners detection" option is enabled the SDK updates its internal list of available scanners and deliver a corresponding asynchronous notification, once it detects connection or disconnection of a particular scanner to the iOS device via Bluetooth. If the option is disabled, the SDK updates its internal list of available scanners, only when requested by the application via the sbtGetAvailableScannersList.
///
/// - Parameter enable: Boolean value whether the option should be enabled or disabled
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtEnableAvailableScannersDetection:(BOOL)enable;


- (SBT_RESULT) sbtEnableBluetoothScannerDiscovery:(BOOL)enable;

/// Method to enable or disable automatic communication session reestablishment for a specific scanner
///
/// The SDK supports "Automatic Session Reestablishment" option is used to automatically reconnect to a scanner that unexpectedly disappeared. If the "Automatic Session Reestablishment" option is enabled for a specific scanner, the SDK automatically attempts to reestablish a connection with the scanner when it becomes available again. The option has no effect if the application has intentionally terminated a communication session with the scanner via the sbtTerminateCommunicationSession function.
///
/// - Parameters:
///   - enable: Boolean value whether the option should be enabled or disabled
///   - scannerID: Unique identifier of a particular scanner assigned by the SDK
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtEnableAutomaticSessionReestablishment:(BOOL)enable forScanner:(int)scannerID;

///  Perform various scanner operations using provided operation codes
///
/// - Parameters:
///   - opCode: Operation code for the command
///   - inXML: Relevant argument list for the opCode, structured into an XML string.
///   - outXML: Results of method execution, structured into an XML string.
///   - scannerID: Unique identifier of a particular scanner assigned by the SDK
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtExecuteCommand:(int)opCode aInXML:(NSString*)inXML aOutXML:(NSMutableString**)outXML forScanner:(int)scannerID;

///  Perform LED on/ off for the CS4070 scanner
///
/// - Parameters:
///   - enable: Boolean value to switch on or off the particular LED
///   - ledCode:   LED code.
///   - scannerID: Unique identifier of a particular scanner assigned by the SDK
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtLedControl:(BOOL)enable aLedCode:(int)ledCode forScanner:(int)scannerID;

///  Perform beep operations for the CS4070 scanner
///
/// - Parameters:
///   - beepCode:   Beep code.
///   - scannerID: Unique identifier of a particular scanner assigned by the SDK
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtBeepControl:(int)beepCode forScanner:(int)scannerID;

/// Method to set the Bluetooth address of the device.
///
///  This address will be used when SDK cannot determine Bluetooth MAC address automatically.
///
/// - Parameter btAdd: Bluetooth MAC address to be used
- (void) sbtSetBTAddress:(NSString*)btAdd;

/// Provides STC(Scan to connect) pairing barcode for Bluetooth scanners
///
/// After scanning the STC pairing barcode, the scanner will be paired and get connected. Then the sbtEventCommunicationSessionEstablished event gets triggered
///
/// - Parameters:
///   - barcodeType: Barcode type to be used
///   - comProtocol: Communication protocol to be used
///   - setDefaultsStatus: Set default status to be used
///   - btAddress: Bluetooth MAC address to be used
///   - imageFrame: A CGRect type object with required image frame
///
/// - Returns: STC paring barcode as an UIImage object
- (UIImage*) sbtGetPairingBarcode:(BARCODE_TYPE)barcodeType withComProtocol:(STC_COM_PROTOCOL)comProtocol withSetDefaultStatus:(SETDEFAULT_STATUS)setDefaultsStatus withBTAddress:(NSString*)btAddress withImageFrame:(CGRect)imageFrame;

/// Provides STC(Scan to connect) pairing barcode for Bluetooth scanners
///
/// After scanning the STC pairing barcode, the scanner will be paired and get connected. Then the sbtEventCommunicationSessionEstablished event gets triggered
///
/// - Parameters:
///   - barcodeType: Barcode type to be used
///   - comProtocol: Communication protocol to be used
///   - setDefaultsStatus: Set default status to be used
///   - imageFrame: A CGRect type object with required image frame
///
/// - Returns: STC paring barcode as an UIImage object
- (UIImage*) sbtGetPairingBarcode:(BARCODE_TYPE)barcodeType withComProtocol:(STC_COM_PROTOCOL)comProtocol withSetDefaultStatus:(SETDEFAULT_STATUS)setDefaultsStatus withImageFrame:(CGRect)imageFrame;

/// Automatically attempts to establish a connection with the last connected scanner during app relaunch
///
/// The SDK supports "Auto connect to the last connected Scanner" option to automatically connect to the last connected scanner during app relaunch in Bluetooth Low Energy (BLE) mode. This option is disabled by default and if the "Auto connect to the last connected Scanner" option is enabled, the SDK automatically attempts to establish a connection with the last connected scanner during app relaunch.
///
/// - Parameter enable: Boolean value whether the option should be enabled or disabled
///
/// - Returns: Success or failure of the operation
- (SBT_RESULT) sbtAutoConnectToLastConnectedScannerOnAppRelaunch:(BOOL)enable;


/// Debug logs on Debug console. The defaut is disabled.
/// - Parameter enable: Boolean value whether the option should be enabled or disabled
- (SBT_RESULT) sbtEnableDebugOuput:(BOOL)enable;

/// To set a meaningful name for the digital scanner that appears in the application during device discovery. The default name is the digital scanner name followed by its serial number.
/// - Parameters:
///   - friendlyName: New Bluetooth Friendly Name that needs to appear in the application during device discovery.  Bluetooth Friendly Name should contain less than 23 characters.
///   - scannerID: Unique identifier of a particular scanner assigned by the SDK
- (SBT_RESULT) sbtSetFriendlyName:(NSString *)friendlyName forScanner:(int)scannerID;


@end
