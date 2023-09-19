namespace Symbolbtscannersdk;

public enum SbtResult : uint
{
    Success = 0,
    Failure = 1,
    ScannerNotAvailable = 2,
    ScannerNotActive = 3,
    InvalidParams = 4,
    ResponseTimeout = 5,
    OpcodeNotSupported = 6,
    ScannerNoSupport = 7,
    BtaddressNotSet = 8,
    ScannerNotConnectStc = 9
}

//[Verify (InferredFromMemberPrefix)]
public enum SbtOpmode : uint
{
    Mfi = 1,
    Btle = 2,
    All = 3
}

//[Verify (InferredFromMemberPrefix)]
public enum SbtConntype : uint
{
    Invalid = 0,
    Mfi = 1,
    Btle = 2
}

//[Verify (InferredFromMemberPrefix)]
public enum SbtEvent : uint
{
    Barcode = (1),
    Image = (1 << 1),
    Video = (1 << 2),
    ScannerAppearance = (1 << 3),
    ScannerDisappearance = (1 << 4),
    SessionEstablishment = (1 << 5),
    SessionTermination = (1 << 6),
    RawData = (1 << 7)
}

//[Verify (InferredFromMemberPrefix)]
public enum SbtDevmanufacturer : uint
{
    Invalid = 0,
    Zebra = 1,
    Csr = 2
}

//[Verify (InferredFromMemberPrefix)]
public enum SbtLedcode : uint
{
    Red = 0,
    Green = 1,
    Yellow = 2,
    Amber = 3,
    Blue = 4
}

//[Verify (InferredFromMemberPrefix)]
public enum SbtBeepcode : uint
{
    ShortHigh1 = 0,
    ShortHigh2 = 1,
    ShortHigh3 = 2,
    ShortHigh4 = 3,
    ShortHigh5 = 4,
    ShortLow1 = 5,
    ShortLow2 = 6,
    ShortLow3 = 7,
    ShortLow4 = 8,
    ShortLow5 = 9,
    LongHigh1 = 10,
    LongHigh2 = 11,
    LongHigh3 = 12,
    LongHigh4 = 13,
    LongHigh5 = 14,
    LongLow1 = 15,
    LongLow2 = 16,
    LongLow3 = 17,
    LongLow4 = 18,
    LongLow5 = 19,
    FastWarble = 20,
    SlowWarble = 21,
    Mix1HighLow = 22,
    Mix2LowHigh = 23,
    Mix3HighLowHigh = 24,
    Mix4LowHighLow = 25
}

//[Verify (InferredFromMemberPrefix)]
public enum Sbt
{
    ErrorOpcode = -1, // 0xFFFFFFFF
    DeviceAbortMacropdf = 2000, // 0x000007D0
    DeviceAbortUpdateFirmware = 2001, // 0x000007D1
    DeviceAimOff = 2002, // 0x000007D2
    DeviceAimOn = 2003, // 0x000007D3
    DeviceEnterLowPowerMode = 2004, // 0x000007D4
    DeviceFlushMacropdf = 2005, // 0x000007D5
    DeviceGetParameters = 2007, // 0x000007D7
    DeviceGetScannerCapabilities = 2008, // 0x000007D8
    DeviceLedOff = 2009, // 0x000007D9
    DeviceLedOn = 2010, // 0x000007DA
    DevicePullTrigger = 2011, // 0x000007DB
    DeviceReleaseTrigger = 2012, // 0x000007DC
    DeviceScanDisable = 2013, // 0x000007DD
    DeviceScanEnable = 2014, // 0x000007DE
    DeviceSetParameterDefaults = 2015, // 0x000007DF
    DeviceSetParameters = 2016, // 0x000007E0
    DeviceSetParameterPersistance = 2017, // 0x000007E1
    DeviceBeepControl = 2018, // 0x000007E2
    RebootScanner = 2019, // 0x000007E3
    DeviceVibrationFeedback = 2020, // 0x000007E4
    DeviceCaptureImage = 3000, // 0x00000BB8
    DeviceAbortImageXfer = 3001, // 0x00000BB9
    DeviceCaptureBarcode = 3500, // 0x00000DAC
    DeviceCaptureVideo = 4000, // 0x00000FA0
    RsmAttrGetall = 5000, // 0x00001388
    RsmAttrGet = 5001, // 0x00001389
    RsmAttrGetnext = 5002, // 0x0000138A
    RsmAttrGetOffset = 5003, // 0x0000138B
    RsmAttrSet = 5004, // 0x0000138C
    RsmAttrStore = 5005, // 0x0000138D
    GetDeviceTopology = 5006, // 0x0000138E
    RefreshTopology = 5007, // 0x0000138F
    GetDeviceTopologyEx = 5008, // 0x00001390
    RsmGetPcktsize = 5011, // 0x00001393
    StartNewFirmware = 5014, // 0x00001396
    UpdateAttribMetaFile = 5015, // 0x00001397
    UpdateFirmware = 5016, // 0x00001398
    UpdateFirmwareFromPlugin = 5017, // 0x00001399
    SetAction = 6000, // 0x00001770
}

public enum SbtFlashCommand : uint
{
    Ping = 1,
    SessionStart = 2,
    SessionEnd = 3,
    DlStart = 4,
    DlBlock = 5,
    DlEnd = 6,
    ChangeBaud = 7,
    EnterFat = 8,
    SessionInfo = 9,
    JumpAddress = 10,
    SetGuid = 11,
    ReadSc = 12
}

public enum SbtDataLayerStatus : uint
{
    DOk = 0,
    DScUnknown = 1,
    DScRelResident = 2,
    DBaudUnsupported = 3,
    DScNotAllowed = 5,
    DWarningMax = 15,
    LlOk = 0,
    DFailMin = 16,
    LlTimeoutErr = 17,
    LlStxErr = 18,
    LlLenErr = 19,
    LlChecksumErr = 20,
    LlInternalErr = 21,
    DBlkSizeErr = 32,
    DAddrErr = 33,
    DCmdSeqErr = 34,
    DDownloadErr = 35,
    DScUnsupportedErr = 36,
    DScCrcErr = 37,
    DWriteAddrErr = 38,
    DInternalErr = 39,
    DOpIllegalErr = 40,
    DCmdFormatErr = 41,
    DFlashEraseErr = 64,
    DFlashWriteErr = 96,
    SDeviceNotFlashable = 128
}

public enum SbtFwUpdateResult : uint
{
    Success = 0,
    Failure = 1
}

public enum StcComProtocol : uint
{
    StcSsiMfi = 0,
    StcSsiBle = 1,
    SbtSsiHid = 2,
    NoComProtocol = 3
}

public enum SetdefaultStatus : uint
{
    Yes = 0,
    No = 1
}

public enum BarcodeType : uint
{
    Legacy = 0,
    Stc = 1
}

public enum SbtDevmodel : uint
{
    Invalid = 0,
    SsiRfd8500 = 1,
    SsiCs4070 = 2,
    SsiLi3678 = 3,
    SsiDs3678 = 4,
    SsiDs8178 = 5,
    SsiDs2278 = 6,
    SsiGeneric = 7,
    RfidRfd8500 = 8,
    SsiCs6080 = 9,
    SsiRs5100 = 10,
}