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
 *  Description:   ISbtSdkApiDelegate.h
 *
 *  Notes:
 *
 ******************************************************************************/

#import <Foundation/Foundation.h>
#import <ZebraScannerSDK/SbtScannerInfo.h>
#import <ZebraScannerSDK/FirmwareUpdateEvent.h>

/// A protocol to be implemented by the application to receive all the supported callbacks from the Zebra Scanner SDK to inform the application about scanner related events.
///
/// The SDK supports a set of asynchronous notifications to inform the application about scanner related events. This includes connectivity related events (i.e., appearance or diappearance of a scanner), and scanner action events (i.e., received barcode data). If an object is registered via ``ZebraScannerSDK/ISbtSdkApi/sbtSetDelegate:`` as a notification receiver, the SDK calls the corresponding method of the registered object when a particular event occurs if the application is subscribed for events of this type using ``ZebraScannerSDK/ISbtSdkApi/sbtSubsribeForEvents:`` .
@protocol ISbtSdkApiDelegate <NSObject>

/// Device arrival notification informs about appearance of a particular available scanner.
/// - Parameter availableScanner: Object representing an appeared available scanner
- (void)sbtEventScannerAppeared:(SbtScannerInfo*)availableScanner;

/// Device disappeared notification informs about disappearance of a particular available scanner.
/// - Parameter scannerID: Unique identifier of a disappeared available scanner assigned by SDK.
- (void)sbtEventScannerDisappeared:(int)scannerID;

/// Session established notification informs about appearance of a particular active scanner.
/// - Parameter activeScanner: Object representing an appeared active scanner.
- (void)sbtEventCommunicationSessionEstablished:(SbtScannerInfo*)activeScanner;

/// Session terminated notification informs about disappearance of a particular active scanner
/// - Parameter scannerID: Unique identifier of a disappeared active scanner assigned by SDK
- (void)sbtEventCommunicationSessionTerminated:(int)scannerID;

/// ***DEPRECATED***
///
/// Use ``ZebraScannerSDK/ISbtSdkApiDelegate/sbtEventBarcodeData:barcodeType:fromScanner:``
- (void)sbtEventBarcode:(NSString*)barcodeData barcodeType:(int)barcodeType fromScanner:(int)scannerID;

/// Barcode event notification informs about reception of a particular barcode of a particular type from a particular active scanner.
/// - Parameters:
///   - barcodeData: Object representing ASCII data of a scanned barcode
///   - barcodeType: Barcode type (Symbology type) of the scanned barcode
///   - scannerID: Unique identifier that assigned by SDK of a particular active scanner that barcode is scanned
- (void)sbtEventBarcodeData:(NSData*)barcodeData barcodeType:(int)barcodeType fromScanner:(int)scannerID;

/// Firmware Update Event notification informs about status in firmware update process
///
/// No need to specifically subscribe to this event. It is just need to implement this delegate method.
/// - Parameter fwUpdateEventObj: Object representing firmware updating status
- (void)sbtEventFirmwareUpdate:(FirmwareUpdateEvent*)fwUpdateEventObj;



@optional
/// System event notification for trigger pull and trigger release.
///
/// System events can be enabled by setting the  ``ZebraScannerSDK/RSM_ATTR_SYSTEM_EVENT`` RSM attribure to True
/// - Parameters:
///   - triggerState: State of the trigger ``ZebraScannerSDK/SBT_TRIGGER_STATE``
///   - scannerID: Unique identifier of the scanner that trigger comes from.
- (void)sbtEventTrigger:(SBT_TRIGGER_STATE) triggerState fromScanner:(int)scannerID;

@end
