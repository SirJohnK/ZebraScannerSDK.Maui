namespace ZebraBarcodeScannerSDK;

public class AssetInformation
{
    private readonly string configurationName;
    private readonly string serialNumber;
    private readonly string modelNumber;
    private readonly string firmwareVersion;
    private readonly string manufacturedDate;
    private readonly int batteryPercentage;
    private readonly DateTime retrivedTime = DateTime.Now;

    internal AssetInformation(
      string configuration,
      string scannerSerialNumber,
      string model,
      string firmware,
      string scannerManufacturedDate,
      int batteryPercentage)
    {
        configurationName = configuration;
        serialNumber = scannerSerialNumber;
        modelNumber = model;
        firmwareVersion = firmware;
        manufacturedDate = scannerManufacturedDate;
        this.batteryPercentage = batteryPercentage;
    }

    public string ConfigurationName => configurationName;

    public string SerialNumber => serialNumber;

    public string ModelNumber => modelNumber;

    public string FirmwareVersion => firmwareVersion;

    public string ManufacturedDate => manufacturedDate;

    public int BatteryPercentage => batteryPercentage;

    public DateTime RetrivedTime => retrivedTime;
}