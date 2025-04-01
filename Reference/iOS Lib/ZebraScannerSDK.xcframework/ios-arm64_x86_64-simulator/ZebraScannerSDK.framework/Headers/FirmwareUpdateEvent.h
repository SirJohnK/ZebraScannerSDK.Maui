//
//  FirmwareUpdateEvent.h
//  ScannerSDKApp
//
//  Created by pqj647 on 6/7/16.
//  Copyright © 2016 Alexei Igumnov. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <ZebraScannerSDK/SbtScannerInfo.h>
#import <ZebraScannerSDK/SbtSdkDefs.h>

/// Class which wraps the firmware updating details and send these details via ``ZebraScannerSDK/ISbtSdkApiDelegate/sbtEventFirmwareUpdate:`` event
@interface FirmwareUpdateEvent : NSObject


/// Scanner information that triggered the event
@property (nonatomic, retain) SbtScannerInfo *scannerInfo;

/// Maximum number of records contained in the firmware update file
@property(readonly) int maxRecords;

/// Record number that is being downloaded
@property(readonly) int currentRecord;

/// Software component number that download has started
@property(readonly) int swComponent;

/// Status of the firmware update event
@property(readonly) SBT_FW_UPDATE_RESULT status;



- (instancetype)initWithScannerInfo:(SbtScannerInfo*)_scannerInfo withRecords:(int)_maxRecords withSWComponenet:(int)_swComponent withCurrentRecord:(int)_currentRecord withStatus:(SBT_FW_UPDATE_RESULT)_status;

@end
