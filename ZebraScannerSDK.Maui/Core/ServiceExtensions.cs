using Microsoft.Extensions.DependencyInjection;

namespace ZebraBarcodeScannerSDK;

public static class ServiceExtensions
{
    public static IServiceCollection AddZebraScannerSDK(this IServiceCollection services)
    {
        return services.AddSingleton(ScannerSDK.Instance);
    }
}