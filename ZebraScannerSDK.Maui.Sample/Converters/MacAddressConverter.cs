using System.Globalization;
using System.Text.RegularExpressions;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Format Mac Address Converter.
/// </summary>
public class MacAddressConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string macAddress && Regex.Match(macAddress, GlobalConstants.MacAddressPattern).Success)
        {
            return Regex.Replace(macAddress, GlobalConstants.MacAddressPattern, @"$1:$2:$3:$4:$5:$6");
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string macAddress && Regex.Match(macAddress, GlobalConstants.MacAddressPattern).Success)
        {
            return Regex.Replace(macAddress, GlobalConstants.MacAddressPattern, @"$1$2$3$4$5$6");
        }

        return value;
    }
}