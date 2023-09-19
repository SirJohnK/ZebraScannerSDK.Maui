using static Microsoft.Maui.ApplicationModel.Permissions;

namespace ZebraScannerSDK.Maui.Sample;

internal class BluetoothConnectPermission : BasePlatformPermission
{
    public override (string androidPermission, bool isRuntime)[] RequiredPermissions => new List<(string androidPermission, bool isRuntime)>
      {
          (Android.Manifest.Permission.BluetoothScan, true),
          (Android.Manifest.Permission.BluetoothConnect, true),
          (Android.Manifest.Permission.BluetoothAdvertise, true),
          (Android.Manifest.Permission.AccessFineLocation, true),
          (Android.Manifest.Permission.PostNotifications, true),
          (Android.Manifest.Permission.BluetoothPrivileged, true)
      }.ToArray();
}