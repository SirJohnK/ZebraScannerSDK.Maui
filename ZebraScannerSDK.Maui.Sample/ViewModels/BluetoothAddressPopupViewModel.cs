using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Text.RegularExpressions;

namespace ZebraScannerSDK.Maui.Sample;

/// <summary>
/// Popup dialog that allows users to enter and validate a Bluetooth address.
/// </summary>
/// <remarks>
/// This view model provides commands for confirming or cancelling the dialog and exposes validation
/// logic for Bluetooth addresses. The dialog can be pre-populated with an input value via the Input property. The
/// OkButton command is only enabled when the entered Bluetooth address is valid according to the expected MAC address
/// format.
/// </remarks>
/// <param name="popupService">A service used to manage popup dialogs within the application.</param>
[QueryProperty(nameof(Input), "Input")]
public partial class BluetoothAddressPopupViewModel(IPopupService popupService) : ObservableObject
{
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAddressValid))]
    private string? bluetoothAddress;

    public string? Input
    {
        set => BluetoothAddress = value;
    }

    /// <summary>
    /// Dialog OK button command to close dialog. (Overrideable)
    /// </summary>
    /// <remarks>Will close dialog with <see langword="true"/> value respons.</remarks>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    [RelayCommand(CanExecute = nameof(IsAddressValid))]
    public virtual Task OkButton() => popupService.ClosePopupAsync(Shell.Current, BluetoothAddress);

    /// <summary>
    /// Dialog CANCEL button command to close dialog. (Overrideable)
    /// </summary>
    /// <remarks>Will close dialog with <see langword="false"/> value respons.</remarks>
    /// <returns>Awaitable <see cref="Task"/>.</returns>
    [RelayCommand]
    public virtual Task CancelButton() => popupService.ClosePopupAsync(Shell.Current);

    public bool IsAddressValid
    {
        get => !string.IsNullOrEmpty(BluetoothAddress) && Regex.Match(BluetoothAddress, GlobalConstants.MacAddressPattern).Success;
    }
}