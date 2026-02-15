using CommunityToolkit.Maui.Views;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Popup view that allows users to enter or select a Bluetooth address.
/// </summary>
/// <remarks>
/// View where a Bluetooth address must be provided or confirmed by the user,
/// such as pairing with a device. The result of the popup is the selected or entered Bluetooth address.
/// </remarks>
public partial class BluetoothAddressPopupView : Popup<string>
{
    public BluetoothAddressPopupView(BluetoothAddressPopupViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}