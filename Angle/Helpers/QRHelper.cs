using LUNA.Models.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Drawing.Imaging;
using System.IO;

using ZXing;
using ZXing.Windows.Compatibility;
using ZXing.QrCode;

namespace Angle.Helpers
{
    /// <summary>
    /// QR Helper is an Helper class for the Utilisation of readeable QR Methodes/Function
    /// Like Create Visual QR and Validates
    /// </summary>
    public static class QRHelper
    {
        /// <summary>
        /// Takes parameters information and generates an QR Code with BarCodeWrites as bitmatrix
        /// than convert it to an HTML <img></img> Tag
        /// and includes in the src of the img tag an PNG coded as string base64
        /// </summary>
        /// <param name="url">url for qr code</param>
        /// <param name="alt">alternate message in case of error or empty url string</param>
        /// <param name="height">height of QR in pixel</param>
        /// <param name="width">width of QR in pixel(Height and width should be the same)</param>
        /// <param name="margin">html css margin for qr</param>
        /// <returns></returns>
        public static HtmlString GenerateQRCode(string url, string alt = "GeneratedQRCode", int height = 150, int width = 150, int margin = 0)
        {

            BarcodeWriter qrWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Width = width,
                    Height = height,
                }
            };

            using (var q = qrWriter.Write(url))
            {
                using (var ms = new MemoryStream())
                {
                    q.Save(ms, ImageFormat.Png);
                    var img = new TagBuilder("img");
                    img.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
                    img.Attributes.Add("alt", alt);
                    img.Attributes.Add("id", "qrcodegenerated");
                    using (var writer = new StringWriter())
                    {
                        img.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                        var htmlOutput = writer.ToString();
                        return new HtmlString(htmlOutput);
                    }

                }
            }
        }




        /// <summary>
        /// Check if QR is active
        /// </summary>
        /// <param name="QR">Object of type Index_QR who should be checked</param>
        /// <returns>true if active and false if it is deactivated</returns>
        public static bool IsQRValid(Index_QR QR)
        {
            if (!QR.IsActive)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Checks if QR IsActive or if Index_QR passed object 
        /// or is null (Mostly in case Index_QR Object wasnt found in the DB)
        /// </summary>
        /// <param name="QR">Object of type Index_QR who validity should be checked</param>
        /// <returns>An string with an errorMessage wich is used in the view</returns>
        public static string CheckQRError(Index_QR QR)
        {
            if (QR == null)
            {
                return ">>> This QR Code is not registered in our System. " +
                    "Probaly this product was deleted of the system, " +
                    "please contact the System Administrator";
            }
            else if (!IsQRValid(QR))
            {
                return ">>> This QR Code is Deactivated";
            }

            return "";


        }

    }
}
