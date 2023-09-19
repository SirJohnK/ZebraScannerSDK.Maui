using Foundation;
using UIKit;

namespace ZebraScannerSDK.Maui.Sample
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

        //public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        //{
        //    var status = Permissions.CheckStatusAsync<Permissions.StorageRead>().Result;
        //    status = Permissions.CheckStatusAsync<Permissions.LocationAlways>().Result;

        //    return base.FinishedLaunching(application, launchOptions);
        //}
    }
}