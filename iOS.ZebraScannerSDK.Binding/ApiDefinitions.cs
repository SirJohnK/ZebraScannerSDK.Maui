using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Symbolbtscannersdk;

// @interface SbtScannerInfo : NSObject
[BaseType(typeof(NSObject))]
interface SbtScannerInfo
{
    // -(void)dealloc;
    [Export("dealloc")]
    void Dealloc();

    // -(void)setScannerID:(int)scannerID;
    [Export("setScannerID:")]
    void SetScannerID(int scannerID);

    // -(void)setConnectionType:(int)connectionType;
    [Export("setConnectionType:")]
    void SetConnectionType(int connectionType);

    // -(void)setAutoCommunicationSessionReestablishment:(BOOL)enable;
    [Export("setAutoCommunicationSessionReestablishment:")]
    void SetAutoCommunicationSessionReestablishment(bool enable);

    // -(void)setActive:(BOOL)active;
    [Export("setActive:")]
    void SetActive(bool active);

    // -(void)setAvailable:(BOOL)available;
    [Export("setAvailable:")]
    void SetAvailable(bool available);

    // -(void)setScannerName:(NSString *)scannerName;
    [Export("setScannerName:")]
    void SetScannerName(string scannerName);

    // -(void)setScannerModel:(NSString *)scannerModel;
    [Export("setScannerModel:")]
    void SetScannerModel(string scannerModel);

    // -(void)setStcConnected:(BOOL)connected;
    [Export("setStcConnected:")]
    void SetStcConnected(bool connected);

    // -(int)getScannerID;
    [Export("getScannerID")]
    int ScannerID { get; }

    // -(int)getConnectionType;
    [Export("getConnectionType")]
    int ConnectionType { get; }

    // -(BOOL)getAutoCommunicationSessionReestablishment;
    [Export("getAutoCommunicationSessionReestablishment")]
    bool AutoCommunicationSessionReestablishment { get; }

    // -(BOOL)isActive;
    [Export("isActive")]
    bool IsActive { get; }

    // -(BOOL)isAvailable;
    [Export("isAvailable")]
    bool IsAvailable { get; }

    // -(BOOL)isStcConnected;
    [Export("isStcConnected")]
    bool IsStcConnected { get; }

    // -(NSString *)getScannerName;
    [Export("getScannerName")]
    string ScannerName { get; }

    // -(NSString *)getScannerModel;
    [Export("getScannerModel")]
    string ScannerModel { get; }

    // @property (retain, nonatomic) NSString * firmwareVersion;
    [Export("firmwareVersion", ArgumentSemantic.Retain)]
    string FirmwareVersion { get; set; }

    // @property (retain, nonatomic) NSString * mFD;
    [Export("mFD", ArgumentSemantic.Retain)]
    string MFD { get; set; }

    // @property (retain, nonatomic) NSString * serialNo;
    [Export("serialNo", ArgumentSemantic.Retain)]
    string SerialNo { get; set; }

    // @property (retain, nonatomic) NSString * scannerModelString;
    [Export("scannerModelString", ArgumentSemantic.Retain)]
    string ScannerModelString { get; set; }
}

// @interface FirmwareUpdateEvent : NSObject
[BaseType(typeof(NSObject))]
interface FirmwareUpdateEvent
{
    // @property (retain, nonatomic) SbtScannerInfo * scannerInfo;
    [Export("scannerInfo", ArgumentSemantic.Retain)]
    SbtScannerInfo ScannerInfo { get; set; }

    // @property (readonly) int maxRecords;
    [Export("maxRecords")]
    int MaxRecords { get; }

    // @property (readonly) int swComponent;
    [Export("swComponent")]
    int SwComponent { get; }

    // @property (readonly) int currentRecord;
    [Export("currentRecord")]
    int CurrentRecord { get; }

    // @property (readonly) int size;
    [Export("size")]
    int Size { get; }

    // @property (readonly) SBT_FW_UPDATE_RESULT status;
    [Export("status")]
    SbtFwUpdateResult Status { get; }

    // -(instancetype)initWithScannerInfo:(SbtScannerInfo *)_scannerInfo withRecords:(int)_maxRecords withSWComponenet:(int)_swComponent withCurrentRecord:(int)_currentRecord withStatus:(SBT_FW_UPDATE_RESULT)_status;
    [Export("initWithScannerInfo:withRecords:withSWComponenet:withCurrentRecord:withStatus:")]
    NativeHandle Constructor(SbtScannerInfo _scannerInfo, int _maxRecords, int _swComponent, int _currentRecord, SbtFwUpdateResult _status);
}

// @protocol ISbtSdkApiDelegate <NSObject>
[Protocol, Model]
[BaseType(typeof(NSObject))]
interface ISbtSdkApiDelegate
{
    // @required -(void)sbtEventScannerAppeared:(SbtScannerInfo *)availableScanner;
    //[Abstract]
    [Export("sbtEventScannerAppeared:")]
    void SbtEventScannerAppeared(SbtScannerInfo availableScanner);

    // @required -(void)sbtEventScannerDisappeared:(int)scannerID;
    //[Abstract]
    [Export("sbtEventScannerDisappeared:")]
    void SbtEventScannerDisappeared(int scannerID);

    // @required -(void)sbtEventCommunicationSessionEstablished:(SbtScannerInfo *)activeScanner;
    //[Abstract]
    [Export("sbtEventCommunicationSessionEstablished:")]
    void SbtEventCommunicationSessionEstablished(SbtScannerInfo activeScanner);

    // @required -(void)sbtEventCommunicationSessionTerminated:(int)scannerID;
    //[Abstract]
    [Export("sbtEventCommunicationSessionTerminated:")]
    void SbtEventCommunicationSessionTerminated(int scannerID);

    // @required -(void)sbtEventBarcode:(NSString *)barcodeData barcodeType:(int)barcodeType fromScanner:(int)scannerID;
    //[Abstract]
    [Export("sbtEventBarcode:barcodeType:fromScanner:")]
    void SbtEventBarcode(string barcodeData, int barcodeType, int scannerID);

    // @required -(void)sbtEventBarcodeData:(NSData *)barcodeData barcodeType:(int)barcodeType fromScanner:(int)scannerID;
    //[Abstract]
    [Export("sbtEventBarcodeData:barcodeType:fromScanner:")]
    void SbtEventBarcodeData(NSData barcodeData, int barcodeType, int scannerID);

    // @required -(void)sbtEventFirmwareUpdate:(FirmwareUpdateEvent *)fwUpdateEventObj;
    //[Abstract]
    [Export("sbtEventFirmwareUpdate:")]
    void SbtEventFirmwareUpdate(FirmwareUpdateEvent fwUpdateEventObj);

    // @required -(void)sbtEventImage:(NSData *)imageData fromScanner:(int)scannerID;
    //[Abstract]
    [Export("sbtEventImage:fromScanner:")]
    void SbtEventImage(NSData imageData, int scannerID);

    // @required -(void)sbtEventVideo:(NSData *)videoFrame fromScanner:(int)scannerID;
    //[Abstract]
    [Export("sbtEventVideo:fromScanner:")]
    void SbtEventVideo(NSData videoFrame, int scannerID);
}

// @protocol ISbtSdkApi <NSObject>
/*
  Check whether adding [Model] to this declaration is appropriate.
  [Model] is used to generate a C# class that implements this protocol,
  and might be useful for protocols that consumers are supposed to implement,
  since consumers can subclass the generated class instead of implementing
  the generated interface. If consumers are not supposed to implement this
  protocol, then [Model] is redundant and will generate code that will never
  be used.
*/
[Protocol, Model]
[BaseType(typeof(NSObject))]
interface ISbtSdkApi
{
    // @required -(SBT_RESULT)sbtSetDelegate:(id<ISbtSdkApiDelegate>)delegate;
    //[Abstract]
    [Export("sbtSetDelegate:")]
    SbtResult SbtSetDelegate(ISbtSdkApiDelegate @delegate);

    // @required -(NSString *)sbtGetVersion;
    //[Abstract]
    [Export("sbtGetVersion")]
    string SbtGetVersion { get; }

    // @required -(SBT_RESULT)sbtSetOperationalMode:(int)operationalMode;
    //[Abstract]
    [Export("sbtSetOperationalMode:")]
    SbtResult SbtSetOperationalMode(int operationalMode);

    // @required -(SBT_RESULT)sbtSubsribeForEvents:(int)sdkEventsMask;
    //[Abstract]
    [Export("sbtSubsribeForEvents:")]
    SbtResult SbtSubsribeForEvents(int sdkEventsMask);

    // @required -(SBT_RESULT)sbtUnsubsribeForEvents:(int)sdkEventsMask;
    //[Abstract]
    [Export("sbtUnsubsribeForEvents:")]
    SbtResult SbtUnsubsribeForEvents(int sdkEventsMask);

    // @required -(SBT_RESULT)sbtGetAvailableScannersList:(IntPtr **)availableScannersList;
    //[Abstract]
    [Export("sbtGetAvailableScannersList:")]
    SbtResult SbtGetAvailableScannersList(out System.IntPtr availableScannersList);

    // @required -(SBT_RESULT)sbtGetActiveScannersList:(IntPtr **)activeScannersList;
    //[Abstract]
    [Export("sbtGetActiveScannersList:")]
    SbtResult SbtGetActiveScannersList(out System.IntPtr activeScannersList);

    // @required -(SBT_RESULT)sbtEstablishCommunicationSession:(int)scannerID;
    //[Abstract]
    [Export("sbtEstablishCommunicationSession:")]
    SbtResult SbtEstablishCommunicationSession(int scannerID);

    // @required -(SBT_RESULT)sbtTerminateCommunicationSession:(int)scannerID;
    //[Abstract]
    [Export("sbtTerminateCommunicationSession:")]
    SbtResult SbtTerminateCommunicationSession(int scannerID);

    // @required -(SBT_RESULT)sbtEnableAvailableScannersDetection:(BOOL)enable;
    //[Abstract]
    [Export("sbtEnableAvailableScannersDetection:")]
    SbtResult SbtEnableAvailableScannersDetection(bool enable);

    // @required -(SBT_RESULT)sbtEnableBluetoothScannerDiscovery:(BOOL)enable;
    //[Abstract]
    [Export("sbtEnableBluetoothScannerDiscovery:")]
    SbtResult SbtEnableBluetoothScannerDiscovery(bool enable);

    // @required -(SBT_RESULT)sbtEnableAutomaticSessionReestablishment:(BOOL)enable forScanner:(int)scannerID;
    //[Abstract]
    [Export("sbtEnableAutomaticSessionReestablishment:forScanner:")]
    SbtResult SbtEnableAutomaticSessionReestablishment(bool enable, int scannerID);

    // @required -(SBT_RESULT)sbtExecuteCommand:(int)opCode aInXML:(NSString *)inXML aOutXML:(System.IntPtr **)outXML forScanner:(int)scannerID;
    //[Abstract]
    [Export("sbtExecuteCommand:aInXML:aOutXML:forScanner:")]
    SbtResult SbtExecuteCommand(int opCode, string inXML, out System.IntPtr outXML, int scannerID);

    // @required -(SBT_RESULT)sbtLedControl:(BOOL)enable aLedCode:(int)ledCode forScanner:(int)scannerID;
    //[Abstract]
    [Export("sbtLedControl:aLedCode:forScanner:")]
    SbtResult SbtLedControl(bool enable, int ledCode, int scannerID);

    // @required -(SBT_RESULT)sbtBeepControl:(int)beepCode forScanner:(int)scannerID;
    //[Abstract]
    [Export("sbtBeepControl:forScanner:")]
    SbtResult SbtBeepControl(int beepCode, int scannerID);

    // @required -(void)sbtSetBTAddress:(NSString *)btAdd;
    //[Abstract]
    [Export("sbtSetBTAddress:")]
    void SbtSetBTAddress(string btAdd);

    // @required -(UIImage *)sbtGetPairingBarcode:(BARCODE_TYPE)barcodeType withComProtocol:(STC_COM_PROTOCOL)comProtocol withSetDefaultStatus:(SETDEFAULT_STATUS)setDefaultsStatus withBTAddress:(NSString *)btAddress withImageFrame:(CGRect)imageFrame;
    //[Abstract]
    [Export("sbtGetPairingBarcode:withComProtocol:withSetDefaultStatus:withBTAddress:withImageFrame:")]
    UIImage SbtGetPairingBarcode(BarcodeType barcodeType, StcComProtocol comProtocol, SetdefaultStatus setDefaultsStatus, string btAddress, CGRect imageFrame);

    // @required -(UIImage *)sbtGetPairingBarcode:(BARCODE_TYPE)barcodeType withComProtocol:(STC_COM_PROTOCOL)comProtocol withSetDefaultStatus:(SETDEFAULT_STATUS)setDefaultsStatus withImageFrame:(CGRect)imageFrame;
    //[Abstract]
    [Export("sbtGetPairingBarcode:withComProtocol:withSetDefaultStatus:withImageFrame:")]
    UIImage SbtGetPairingBarcode(BarcodeType barcodeType, StcComProtocol comProtocol, SetdefaultStatus setDefaultsStatus, CGRect imageFrame);

    // @required -(SBT_RESULT)sbtAutoConnectToLastConnectedScannerOnAppRelaunch:(BOOL)enable;
    //[Abstract]
    [Export("sbtAutoConnectToLastConnectedScannerOnAppRelaunch:")]
    SbtResult SbtAutoConnectToLastConnectedScannerOnAppRelaunch(bool enable);
}

// @interface SbtSdkFactory : NSObject
[BaseType(typeof(NSObject))]
interface SbtSdkFactory
{
    // +(id<ISbtSdkApi>)createSbtSdkApiInstance;
    [Static]
    [Export("createSbtSdkApiInstance")]
    ISbtSdkApi CreateSbtSdkApiInstance { get; }
}