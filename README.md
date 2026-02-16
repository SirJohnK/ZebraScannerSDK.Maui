# ZebraScannerSDK.Maui <a href="https://www.buymeacoffee.com/sirjohnk" target="_blank"><img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" align="right" style="height: 37px !important;width: 170px !important;" ></a>

Enhanced .NET MAUI version of the Zebra Scanner SDK for Xamarin/MAUI.

## NuGet

|Name|Info|
| ------------------- | :------------------: |
|ZebraScannerSDK.Maui|[![NuGet](https://img.shields.io/nuget/v/ZebraScannerSDK.Maui)](https://www.nuget.org/packages/ZebraScannerSDK.Maui/#versions-body-tab)|

## Background
A couple of years ago, when upgrading a [Xamarin](https://dotnet.microsoft.com/en-us/apps/xamarin) application to [.NET MAUI](https://dotnet.microsoft.com/en-us/apps/maui), the Zebra [Scanner Xamarin Wrapper (Android and iOS)](https://www.zebra.com/us/en/support-downloads/software/scanner-software/xamarin-wrapper-for-scanner-sdk-for-android-and-ios.html) was not yet migrated to [.NET MAUI](https://dotnet.microsoft.com/en-us/apps/maui), so this library was created. Zebra have since then released the [.NET MAUI Wrapper for Scanner SDK (iOS and Android)](https://www.zebra.com/us/en/support-downloads/software/scanner-software/net-maui-wrapper.html) package. That package is not distributed as a common NuGet package and is not updated in the same pace as the native Android/iOS libraries, therefore this library is still maintained as a convenient complement.

## What's included?
Compared to the original solution we have some enhanced and added features:
- One easy to use NuGet package.
- Easy setup with builder pattern extension.
- New `IScannerSDK` interface registered for constructor injection with DI.

## Setup
Use the `AddZebraScannerSDK` service collection extension method for library configuration.
```csharp
var builder = MauiApp.CreateBuilder();
builder
    .UseMauiApp<App>()
    .ConfigureFonts(fonts =>
    {
        fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
        fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
    })

    //Add Services
    builder.Services.AddZebraScannerSDK();
```
For further documentation, see the [Official Zebra Resources](#official-zebra-resources) or the [Sample application](#sample).

## Sample
Look at the [Sample project](https://github.com/SirJohnK/ZebraScannerSDK.Maui/tree/main/ZebraScannerSDK.Maui.Sample) for a example of how to use this library in an .NET MAUI application.

[![Main View](https://github.com/SirJohnK/ZebraScannerSDK.Maui/blob/main/Docs/MainView.png)](https://github.com/SirJohnK/ZebraScannerSDK.Maui/tree/main/ZebraScannerSDK.Maui.Sample)
[![Pairing View](https://github.com/SirJohnK/ZebraScannerSDK.Maui/blob/main/Docs/PairingView.png)](https://github.com/SirJohnK/ZebraScannerSDK.Maui/tree/main/ZebraScannerSDK.Maui.Sample)

## Official Zebra Resources
- [.NET MAUI Wrapper for Scanner SDK (iOS and Android)](https://www.zebra.com/us/en/support-downloads/software/scanner-software/net-maui-wrapper.html)
- [Scanner SDK for Android](https://www.zebra.com/us/en/support-downloads/software/scanner-software/scanner-sdk-for-android.html)
- [Scanner SDK for iOS](https://www.zebra.com/us/en/support-downloads/software/scanner-software/scanner-sdk-for-iOS.html)
- [Scanner Xamarin Wrapper (Android and iOS)](https://www.zebra.com/us/en/support-downloads/software/scanner-software/xamarin-wrapper-for-scanner-sdk-for-android-and-ios.html) (Deprecated and no longer maintained)
