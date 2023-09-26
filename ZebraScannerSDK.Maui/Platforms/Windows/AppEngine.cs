using CoreScanner;

namespace ZebraBarcodeScannerSDK;

public class AppEngine : IDisposable
{
    private int appHandle = 0;
    private int status = Constants.STATUS_FALSE;

    private CCoreScannerClass? scannerApi;

    public AppEngine() => scannerApi = new CCoreScannerClass();

    internal void SetDelegate()
    {
        scannerApi?.Open(appHandle, )
    }

    public void Dispose()
    {
        //Init
        status = Constants.STATUS_FALSE;

        try
        {
            //Close and Clean up
            scannerApi?.Close(appHandle, out status);
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}