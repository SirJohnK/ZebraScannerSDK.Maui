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
 *  Description:   SbtScannerInfo.h
 *
 *  Notes:
 *
 ******************************************************************************/

#import <Foundation/Foundation.h>

/// This class contains information about the scanner like id, name, model, auto communication session reestablishment etc.
@interface SbtScannerInfo : NSObject
{
    /// Unique identifier of a particular scanner assigned by SDK.
    int m_ScannerID;
    /// Mode of a particular scanner.
    int m_ConnectionType;
    /// State of "Automatic communication session reestablishment" option.
    BOOL m_AutoCommunicationSessionReestablishment;
    /// State of a particular scanner (i.e. active scanner is a scanner with which a communication session has been already established).
    BOOL m_Active;
    /// State of a particular scanner (i.e available scanner is a scanner which is available to establish a new communication session).
    BOOL m_Available;
    /// Indicates whether the connection is made through STC
    BOOL m_IsStcConnected;
    /// Name of a particular scanner.
    NSString *m_ScannerName;
    /// Model of a particular scanner.
    NSString *m_ScannerModel;
}

/// Method to set an id for the scanner.
/// - Parameter scannerID: ID to be set
- (void)setScannerID:(int)scannerID;

/// Method to set the connection type.
/// - Parameter connectionType: connection type to be set
- (void)setConnectionType:(int)connectionType;

/// Method to enable/disable the connection reestablishment feature for the scanner.
/// - Parameter enable: true / false
- (void)setAutoCommunicationSessionReestablishment:(BOOL)enable;

/// Method to set the active status(communicating or not) for the scanner.
/// - Parameter active: true/false
- (void)setActive:(BOOL)active;

/// Method to set the available status for the scanner.
/// - Parameter available: true/false
- (void)setAvailable:(BOOL)available;

/// Method to set the name of the scanner.
/// - Parameter scannerName: Name to be set
- (void)setScannerName:(NSString*)scannerName;

/// Method to set the model of the scanner.
/// - Parameter scannerModel: Model to be set
- (void)setScannerModel:(NSString*)scannerModel;

/// Method to set whether the connection is made through STC.
/// - Parameter connected: true/false
- (void)setStcConnected:(BOOL)connected;

/// Method to access the scannerID.
- (int)getScannerID;

/// Method to access the connectionType. (If multiple connection types are supported)
- (int)getConnectionType;

/// Method to know whether the scanner is configured to support connection reestablishment.
- (BOOL)getAutoCommunicationSessionReestablishment;

/// Method which returns true/false depending on whether we are communicating with the scanner or not.
- (BOOL)isActive;

/// Method which returns true/false depending on whether scanner is available for session establishment.
- (BOOL)isAvailable;

/// Method which returns true/false depending on whether scanner is connected via STC.
- (BOOL)isStcConnected;

/// Method which returns the name of the scanner.
- (NSString*)getScannerName;

/// Method to access the model of the scanner.
- (NSString*)getScannerModel;

/// Firmware version of a particular scanner.
@property(nonatomic, retain) NSString *firmwareVersion;

/// Manufature date of a particular scanner.
@property(nonatomic, retain) NSString *mFD;

/// Hardware serial number (Bluetooth Serial Number or HID serial number) of a particular scanner.
@property(nonatomic, retain) NSString *serialNo;

/// Scanner model of a particular scanner.
@property(nonatomic, retain) NSString *scannerModelString;

@end
