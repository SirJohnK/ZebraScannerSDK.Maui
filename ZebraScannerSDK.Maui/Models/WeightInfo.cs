namespace ZebraBarcodeScannerSDK;

public class WeightInfo
{
    private readonly string weight;
    private readonly string weightUnit;
    private readonly string status;

    public WeightInfo(string weightInfo, string unitInfo, string statusInfo)
    {
        weight = weightInfo;
        weightUnit = unitInfo;
        status = statusInfo;
    }

    public string Weight => weight;

    public string WeightUnit => weightUnit;

    public string Status => status;
}