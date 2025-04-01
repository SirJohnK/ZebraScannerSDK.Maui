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
 *  Description:   SbtSdkFactory.h
 *
 *  Notes:
 *
 ******************************************************************************/

#import <Foundation/Foundation.h>
#import <ZebraScannerSDK/ISbtSdkApi.h>

/// A class that facilitates to obtain a single shared instance of the Zebra Scanner SDK API.
@interface SbtSdkFactory : NSObject

/// Get an instance to the Zebra Scanner SDK API.
///
/// - Returns: A single shared instance of an API object that implements the ISbtSdkApi.
+(id<ISbtSdkApi>)createSbtSdkApiInstance;

@end
