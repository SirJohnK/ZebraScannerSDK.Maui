using System.Text;
using System.Xml.Linq;

namespace ZebraBarcodeScannerSDK;

public partial class Scanner
{
    private readonly AppEngine appEngine;
    private readonly int maximumNonPrintableCharactorValue = 32;

    private readonly string xmlArgs = "<inArgs>";
    private readonly string xmlArgsEnd = "</inArgs>";
    private readonly string xmlScannerId = "<scannerID>";
    private readonly string xmlScannerIdEnd = "</scannerID>";
    private readonly string xmlCommandArgs = "<cmdArgs>";
    private readonly string xmlCommandArgsEnd = "</cmdArgs>";
    private readonly string xmlArgString = "<arg-string>";
    private readonly string xmlArgStringEnd = "</arg-string>";
    private readonly string xmlArgsXml = "<arg-xml>";
    private readonly string xmlArgsXmlEnd = "</arg-xml>";
    private readonly string xmlAttributeList = "<attrib_list>";
    private readonly string xmlAttributeListEnd = "</attrib_list>";
    private readonly string xmlAttribute = "<attribute>";
    private readonly string xmlAttributeEnd = "</attribute>";
    private readonly string xmlId = "<id>";
    private readonly string xmlIdEnd = "</id>";
    private readonly string xmlDataType = "<datatype>";
    private readonly string xmlDataTypeEnd = "</datatype>";
    private readonly string xmlValue = "<value>";
    private readonly string xmlValueEnd = "</value>";
    private readonly string xmlAssertInformatiomnAttributes = "20012,535,534,533,616,30012";
    private readonly string constantSerialNumber = "serialnumber";
    private readonly string constantModelNumber = "modelnumber";
    private readonly string constantAttribute = "attribute";
    private readonly string constantId = "id";
    private readonly string constantValue = "value";
    private readonly string constantFirmwareId = "20012";
    private readonly string constantDateOfManufacturedId = "535";
    private readonly string constantConfigurationId = "616";
    private readonly string constantBatteryPercentageId = "30012";
    private readonly string constantPickListMode = "402";
    private readonly string constantAllSimbologies = "6022";
    private readonly string dataTypeinary = "B";
    private readonly string dataTypeAction = "X";
    private readonly string STATUS_PICK_LIST_ENABLE = "2";
    private readonly string STATUS_PICK_LIST_DISABLE = "0";
    private readonly string DISABLE_ALL_SYMBOLOGIES_TEMPORARY = "0";
    private readonly string STATUS_SCALE_NOT_ENABLED = "0";
    private readonly string STATUS_SCALE_NOT_READY = "1";
    private readonly string STATUS_STABLE_WEIGHT_OVER_LIMIT = "2";
    private readonly string STATUS_STABLE_WEIGHT_UNDER_ZERO = "3";
    private readonly string STATUS_NON_STABLE_WEIGHT = "4";
    private readonly string STATUS_STABLE_ZERO_WEIGHT = "5";
    private readonly string STATUS_STABLE_NON_ZERO_WEIGHT = "6";

    public int Id { get; }

    public ConnectionType ConnectionType { get; }

    public bool AutoCommunicationSessionReestablishment { get; }

    public bool Active { get; }

    public bool Available { get; }

    public string? Name { get; }

    public string? Model { get; }

    public string? FirmwareVersion { get; }

    public string? MFD { get; }

    public string? SerialNo { get; }

    public string? ScannerModelString { get; }

    public void Connect() => appEngine.ConnectScanner(Id);

    public void Disconnect() => appEngine.DisconnectScanner(Id);

    public void EnableScannerAutoConnection(bool reconnection) => appEngine.EnableAutomaticSessionReestablishment(reconnection, Id);

    public AssetInformation ScannerAssetInformation() => GetAssetInformation(ExecuteCommand(OpCode.RSM_ATTRIBUTE_GET, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlCommandArgs + xmlArgsXml + xmlAttributeList + xmlAssertInformatiomnAttributes + xmlAttributeListEnd + xmlArgsXmlEnd + xmlCommandArgsEnd + xmlArgsEnd));

    public WeightInfo ReadWeight() => GetWeightInformation(ExecuteCommand(OpCode.SCALE_READ_WEIGHT, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlArgsEnd));

    public string ZeroScale()
    {
        ExecuteCommand(OpCode.SCALE_ZERO_SCALE, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlArgsEnd);
        return "Zero Scale successful";
    }

    public string ResetScale()
    {
        ExecuteCommand(OpCode.SCALE_RESET_SCALE, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlArgsEnd);
        return "Reset Scale successful";
    }

    public string ScaleEnable()
    {
        ExecuteCommand(OpCode.SCALE_ENABLE_SCALE, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlArgsEnd);
        return "Scale enable successful";
    }

    public string ScaleDisable()
    {
        ExecuteCommand(OpCode.SCALE_DISABLE_SCALE, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlArgsEnd);
        return "Scale disable successful";
    }

    public void EnableScanner() => ExecuteCommand(OpCode.DEVICE_SCAN_ENABLE, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlArgsEnd);

    public void DisableScanner() => ExecuteCommand(OpCode.DEVICE_SCAN_DISABLE, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlArgsEnd);

    public void EnablePickListMode() => ExecuteCommand(OpCode.RSM_ATTRIBUTE_SET, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlCommandArgs + xmlArgsXml + xmlAttributeList + xmlAttribute + xmlId + constantPickListMode + xmlIdEnd + xmlDataType + dataTypeinary + xmlDataTypeEnd + xmlValue + STATUS_PICK_LIST_ENABLE + xmlValueEnd + xmlAttributeEnd + xmlAttributeListEnd + xmlArgsXmlEnd + xmlCommandArgsEnd + xmlArgsEnd);

    public void DisablePickListMode() => ExecuteCommand(OpCode.RSM_ATTRIBUTE_SET, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlCommandArgs + xmlArgsXml + xmlAttributeList + xmlAttribute + xmlId + constantPickListMode + xmlIdEnd + xmlDataType + dataTypeinary + xmlDataTypeEnd + xmlValue + STATUS_PICK_LIST_DISABLE + xmlValueEnd + xmlAttributeEnd + xmlAttributeListEnd + xmlArgsXmlEnd + xmlCommandArgsEnd + xmlArgsEnd);

    public string ScannerFirmwareUpdate(string firmwareLocation) => ExecuteCommand(OpCode.SBT_UPDATE_FIRMWARE_FROM_PLUGIN, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlCommandArgs + xmlArgString + firmwareLocation + xmlArgStringEnd + xmlCommandArgsEnd + xmlArgsEnd);

    public string ScannerFirmwareUpdateFromDat(string firmwareLocation) => ExecuteCommand(OpCode.FIRMWARE_UPDATE_DAT, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlCommandArgs + xmlArgString + firmwareLocation + xmlArgStringEnd + xmlCommandArgsEnd + xmlArgsEnd);

    public string StartNewFirmware() => ExecuteCommand(OpCode.FIRMWARE_START_NEW_FIRMWARE, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlArgsEnd);

    public string AbortFirmwareUpdate() => ExecuteCommand(OpCode.FIRMWARE_ABORT_UPDATE_FIRMWARE, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlArgsEnd);

    public string ExecuteCommand(OpCode opCode, string inXml) => appEngine.ExecuteCommand(opCode, inXml, Id);

    public void EnableAutoReconnection(bool reconnection) => appEngine.EnableAutomaticSessionReestablishment(reconnection, Id);

    public void DisableAllSymbologies() => ExecuteCommand(OpCode.RSM_ATTRIBUTE_SET, xmlArgs + xmlScannerId + Id.ToString() + xmlScannerIdEnd + xmlCommandArgs + xmlArgsXml + xmlAttributeList + xmlAttribute + xmlId + constantAllSimbologies + xmlIdEnd + xmlDataType + dataTypeAction + xmlDataTypeEnd + xmlValue + DISABLE_ALL_SYMBOLOGIES_TEMPORARY + xmlValueEnd + xmlAttributeEnd + xmlAttributeListEnd + xmlArgsXmlEnd + xmlCommandArgsEnd + xmlArgsEnd);

    private AssetInformation GetAssetInformation(string inXml)
    {
        string configuration = "";
        string scannerSerialNumber = "";
        string model = "";
        string firmware = "";
        string scannerManufacturedDate = "";
        int batteryPercentage = -1;

        try
        {
            inXml = ReplaceNonPrintableCharacters(inXml);
            foreach (XElement descendant in Scanner.GenerateDocumentObj(inXml).Descendants((XName)constantSerialNumber))
                scannerSerialNumber = descendant.Value;
            foreach (XElement descendant in Scanner.GenerateDocumentObj(inXml).Descendants((XName)constantModelNumber))
                model = descendant.Value;
            foreach (XElement descendant in Scanner.GenerateDocumentObj(inXml).Descendants((XName)constantAttribute))
            {
                if (descendant.Descendants((XName)constantId).First<XElement>().Value == constantFirmwareId)
                    firmware = descendant.Descendants((XName)constantValue).First<XElement>().Value;
                else if (descendant.Descendants((XName)constantId).First<XElement>().Value == constantDateOfManufacturedId)
                    scannerManufacturedDate = descendant.Descendants((XName)constantValue).First<XElement>().Value;
                else if (descendant.Descendants((XName)constantId).First<XElement>().Value == constantConfigurationId)
                    configuration = descendant.Descendants((XName)constantValue).First<XElement>().Value;
                else if (descendant.Descendants((XName)constantId).First<XElement>().Value == constantBatteryPercentageId)
                {
                    var value = descendant.Descendants((XName)constantValue).First<XElement>().Value;
                    var parseStatus = int.TryParse(value, out batteryPercentage);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception " + ex.Message);
        }
        return new AssetInformation(configuration, scannerSerialNumber, model, firmware, scannerManufacturedDate, batteryPercentage);
    }

    private WeightInfo GetWeightInformation(string inXml)
    {
        string weightInfo = "0";
        string unitInfo = "";
        string statusInfo = "";
        try
        {
            inXml = ReplaceNonPrintableCharacters(inXml);
            foreach (XElement descendant in Scanner.GenerateDocumentObj(inXml).Descendants((XName)"response"))
            {
                weightInfo = descendant.Descendants((XName)"weight").First<XElement>().Value;
                unitInfo = descendant.Descendants((XName)"weight_mode").First<XElement>().Value;
                string str = descendant.Descendants((XName)"status").First<XElement>().Value;
                if (str.Equals(STATUS_SCALE_NOT_ENABLED))
                    statusInfo = "Scale Not Enabled";
                else if (str.Equals(STATUS_SCALE_NOT_READY))
                    statusInfo = "Scale Not Ready";
                else if (str.Equals(STATUS_STABLE_WEIGHT_OVER_LIMIT))
                    statusInfo = "Stable Weight Over Weight";
                else if (str.Equals(STATUS_STABLE_WEIGHT_UNDER_ZERO))
                    statusInfo = "Stable Weight Under Zero";
                else if (str.Equals(STATUS_NON_STABLE_WEIGHT))
                    statusInfo = "Non Stable Weight";
                else if (str.Equals(STATUS_STABLE_ZERO_WEIGHT))
                    statusInfo = "Stable Zero Weight";
                else if (str.Equals(STATUS_STABLE_NON_ZERO_WEIGHT))
                    statusInfo = "Stable Non Zero Weight";
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Exception " + ex.Message);
        }
        return new WeightInfo(weightInfo, unitInfo, statusInfo);
    }

    private string ReplaceNonPrintableCharacters(string inputString)
    {
        var stringBuilder = new StringBuilder();
        for (int index = 0; index < inputString.Length; ++index)
        {
            char ch = inputString[index];
            if ((int)(byte)ch >= maximumNonPrintableCharactorValue)
                stringBuilder.Append(ch);
        }
        return stringBuilder.ToString();
    }

    private static XDocument GenerateDocumentObj(string xmlString) => XDocument.Parse(xmlString);
}