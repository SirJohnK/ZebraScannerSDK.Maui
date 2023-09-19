using System.Text;

namespace ZebraBarcodeScannerSDK;

public class BarcodeData
{
    private string? type;
    private byte[] rawData;
    private string barcode;

    public BarcodeData(byte[] barcodeStream, int barcodeType)
    {
        this.rawData = barcodeStream;
        this.barcode = Encoding.Default.GetString(barcodeStream);
        this.GetBarcodeType(barcodeType);
    }

    private void GetBarcodeType(int barcodeType)
    {
        switch (barcodeType)
        {
            case 0:
                this.type = "NOT APPLICABLE";
                break;

            case 1:
                this.type = "CODE 39";
                break;

            case 2:
                this.type = "CODABAR";
                break;

            case 3:
                this.type = "CODE 128";
                break;

            case 4:
                this.type = "D2OF5";
                break;

            case 5:
                this.type = "IATA";
                break;

            case 6:
                this.type = "I2OF5";
                break;

            case 8:
                this.type = "UPCA";
                break;

            case 9:
                this.type = "UPCE0";
                break;

            case 10:
                this.type = "EAN8";
                break;

            case 11:
                this.type = "EAN13";
                break;

            case 12:
                this.type = "CODE11";
                break;

            case 13:
                this.type = "CODE49";
                break;

            case 14:
                this.type = "MSI";
                break;

            case 15:
                this.type = "EAN128";
                break;

            case 16:
                this.type = "UPCE1";
                break;

            case 17:
                this.type = "PDF417";
                break;

            case 18:
                this.type = "CODE16K";
                break;

            case 19:
                this.type = "C39FULL";
                break;

            case 20:
                this.type = "UPCD";
                break;

            case 21:
                this.type = "TRIOPTIC";
                break;

            case 22:
                this.type = "BOOKLAND";
                break;

            case 23:
                this.type = "COUPON";
                break;

            case 24:
                this.type = "NW7";
                break;

            case 25:
                this.type = "ISBT128";
                break;

            case 26:
                this.type = "MACRO PDF";
                break;

            case 27:
                this.type = "DATAMATRIX";
                break;

            case 28:
                this.type = "QR CODE";
                break;

            case 29:
                this.type = "MICRO PDF CCA";
                break;

            case 30:
                this.type = "POSTNET US";
                break;

            case 31:
                this.type = "PLANET CODE";
                break;

            case 32:
                this.type = "CODE 32";
                break;

            case 33:
                this.type = "ISBT128 CON";
                break;

            case 34:
                this.type = "JAPAN POSTAL";
                break;

            case 35:
                this.type = "AUS POSTAL";
                break;

            case 36:
                this.type = "DUTCH POSTAL";
                break;

            case 37:
                this.type = "MAXICODE";
                break;

            case 38:
                this.type = "CANADIN POSTAL";
                break;

            case 39:
                this.type = "UK POSTAL";
                break;

            case 40:
                this.type = "MICRO PDF";
                break;

            case 44:
                this.type = "MICRO QR CODE";
                break;

            case 45:
                this.type = "AZTEC";
                break;

            case 46:
                this.type = "AZTEC RUNE CODE";
                break;

            case 48:
                this.type = "RSS14";
                break;

            case 49:
                this.type = "RSS LIMITED";
                break;

            case 50:
                this.type = "RSS EXPANDED";
                break;

            case 54:
                this.type = "ISSN";
                break;

            case 55:
                this.type = "SCANLET";
                break;

            case 57:
                this.type = "MATRIX 2 OF 5";
                break;

            case 72:
                this.type = "UPCA 2";
                break;

            case 73:
                this.type = "UPCE0 2";
                break;

            case 74:
                this.type = "EAN8 2";
                break;

            case 75:
                this.type = "EAN13 2";
                break;

            case 80:
                this.type = "UPCE1 2";
                break;

            case 81:
                this.type = "CCA EAN128";
                break;

            case 82:
                this.type = "CCA EAN13";
                break;

            case 83:
                this.type = "CCA EAN8";
                break;

            case 84:
                this.type = "CCA RSS EXPANDED";
                break;

            case 85:
                this.type = "CCA RSS LIMITED";
                break;

            case 86:
                this.type = "CCA RSS14";
                break;

            case 87:
                this.type = "CCA UPCA";
                break;

            case 88:
                this.type = "CCA UPCE";
                break;

            case 89:
                this.type = "CCC EAN128";
                break;

            case 90:
                this.type = "TLC39";
                break;

            case 97:
                this.type = "CCB EAN128";
                break;

            case 98:
                this.type = "CCB EAN13";
                break;

            case 99:
                this.type = "CCB EAN8";
                break;

            case 100:
                this.type = "CCB RSS EXPANDED";
                break;

            case 101:
                this.type = "CCB RSS LIMITED";
                break;

            case 102:
                this.type = "CCB RSS14";
                break;

            case 103:
                this.type = "CCB UPCA";
                break;

            case 104:
                this.type = "CCB UPCE";
                break;

            case 105:
                this.type = "SIGNATURE CAPTURE";
                break;

            case 113:
                this.type = "MATRIX2OF5";
                break;

            case 114:
                this.type = "CHINESE2OF5";
                break;

            case 115:
                this.type = "KOREAN 3 OF 5";
                break;

            case 136:
                this.type = "UPCA 5";
                break;

            case 137:
                this.type = "UPCE0 5";
                break;

            case 138:
                this.type = "EAN8 5";
                break;

            case 139:
                this.type = "EAN13 5";
                break;

            case 144:
                this.type = "UPCE1 5";
                break;

            case 154:
                this.type = "MACRO MICRO PDF";
                break;

            case 180:
                this.type = "NEW COUPON CODE";
                break;

            case 183:
                this.type = "HAN XIN";
                break;

            default:
                this.type = "UNKNOWN SYMBOLOGY";
                break;
        }
    }

    public string? Type => type;

    public byte[] RawData => rawData;

    public string Barcode => barcode;
}