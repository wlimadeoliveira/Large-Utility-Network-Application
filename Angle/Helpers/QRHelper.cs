using LUNA.Models.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Drawing.Imaging;
using System.IO;

using ZXing;
using ZXing.Windows.Compatibility;
using HtmlHelper = Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelper;
using ZXing.QrCode;

namespace Angle.Helpers
{

    
    public static class QRHelper
    {
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

        public static string CheckQRError(Index_QR QR)
        {
            if (QR == null)
            {
                return ">>> This QR Code is not registered in our System. Probaly this product was deleted of the system, please contact the System Administrator";
            }
            else if (!IsQRValid(QR))
            {
                return ">>> This QR Code is Deactivated";
            }
        
            return "";
            

        }

    }
}
