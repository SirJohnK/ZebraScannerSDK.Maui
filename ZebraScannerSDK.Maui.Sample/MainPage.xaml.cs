using System.Diagnostics;
using ZebraBarcodeScannerSDK;

namespace ZebraScannerSDK.Maui.Sample
{
    public partial class MainPage : ContentPage
    {
        private int count = 0;
        private readonly IScannerSDK scannerSDK;

        public MainPage(IScannerSDK scannerSDK)
        {
            //Init
            this.scannerSDK = scannerSDK;

            InitializeComponent();

            HelloWorldLabel.Text = scannerSDK.Version;

            InitScannerManager();
        }

        private async void InitScannerManager()
        {
            //await Permissions.RequestAsync<BluetoothConnectPermission>();
            scannerSDK.ScannerManager.EnableAvailableScannersDetection(true);
            scannerSDK.ScannerManager.EnableBluetoothScannerDiscovery();

            scannerSDK.ScannerManager.SubscribeForEvents((int)(Notifications.EVENT_SCANNER_APPEARANCE | Notifications.EVENT_SCANNER_DISAPPEARANCE | Notifications.EVENT_SESSION_ESTABLISHMENT | Notifications.EVENT_SESSION_TERMINATION | Notifications.EVENT_BARCODE));

            scannerSDK.ScannerManager.Appeared += ScannerManager_AppearedEvent;
            scannerSDK.ScannerManager.BarcodeData += ScannerManager_BarcodeDataEvent;
            scannerSDK.ScannerManager.Connected += ScannerManager_ConnectedEvent;
            scannerSDK.ScannerManager.Disappeared += ScannerManager_DisappearedEvent;
            scannerSDK.ScannerManager.Disconnected += ScannerManager_DisconnectedEvent;

            scannerSDK.ScannerManager.SetSTCEnabledState(true);

            //scannerSDK.ScannerManager.SetOperationMode(OpMode.OPMODE_SSI); //Android
            scannerSDK.ScannerManager.SetOperationMode(OpMode.OPMODE_MFI_BTLE); //iOS

            //scannerSDK.ScannerManager.EnableBluetoothScannerDiscovery();
            //scannerSDK.ScannerManager.StartScanForTopologyChanges();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //var bc = scannerSDK.GetBluetoothPairingBarcode(PairingBarcodeType.BARCODE_TYPE_STC, BluetoothProtocol.SSI_BT_CRADLE_HOST, ScannerConfiguration.SET_FACTORY_DEFAULTS, "A4:75:B9:D1:82:6D"); //Android
            //var bc = scannerSDK.GetBluetoothPairingBarcode(PairingBarcodeType.BARCODE_TYPE_STC, BluetoothProtocol.SSI_BT_MFI, ScannerConfiguration.SET_FACTORY_DEFAULTS, "FC:B6:D8:77:5A:95");
            var bc = scannerSDK.GetBluetoothPairingBarcode(PairingBarcodeType.BARCODE_TYPE_STC, BluetoothProtocol.SSI_BT_LE, ScannerConfiguration.KEEP_CURRENT); //iOS
            BotImage.Source = ImageSource.FromStream(() => new MemoryStream(bc));
            OnPropertyChanged(nameof(BotImage));

            var scanners = scannerSDK.ScannerManager.GetAvailableScanners();
            if (scanners.Count > 0)
            {
                scanners[0].Connect();
            }
        }

        private void ScannerManager_DisconnectedEvent(int scannerID)
        {
            Debug.WriteLine($"Scanner Disconnected! (Scanner: {scannerID})");
        }

        private void ScannerManager_DisappearedEvent(int scannerID)
        {
            Debug.WriteLine($"Scanner Disappeared! (Scanner: {scannerID})");
        }

        private void ScannerManager_ConnectedEvent(Scanner scannerInfo)
        {
            Debug.WriteLine($"Scanner Connected! (Scanner: {scannerInfo.Name} ({scannerInfo.Id}))");

            var info = scannerInfo.ScannerAssetInformation();
        }

        private void ScannerManager_AppearedEvent(Scanner scannerInfo)
        {
            Debug.WriteLine($"Scanner Appeared! (Scanner: {scannerInfo.Name} ({scannerInfo.Id}))");
        }

        private void ScannerManager_BarcodeDataEvent(ZebraBarcodeScannerSDK.BarcodeData barcodeData, int scannerID)
        {
            Debug.WriteLine($"Barcode Scanned: {barcodeData.Barcode} (Scanner: {scannerID}");
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);

            var active = scannerSDK.ScannerManager.GetActiveScanners();
            var available = scannerSDK.ScannerManager.GetAvailableScanners();
            var latest = available.FirstOrDefault(scanner => scanner.SerialNo == "04:EE:03:CE:62:E0");
            var info = latest?.ScannerAssetInformation();
        }
    }
}