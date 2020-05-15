using LUNA.Models.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Windows.Compatibility;
using HtmlHelper = Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelper;

namespace Angle.Helpers
{

    
    public static class QRHelper
    {
        public static HtmlString GenerateQRCode(string url, string alt = "GeneratedQRCode", int height = 150, int width = 150, int margin = 0)
        {
            var qrWriter = new BarcodeWriter();
            qrWriter.Format = BarcodeFormat.QR_CODE;

            qrWriter.Options = new ZXing.QrCode.QrCodeEncodingOptions
            {
                ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.L,
                Height = 150,
                Width = 150,
                QrVersion = 3
            };

            using (var q = qrWriter.Write(url))
            {
                using (var ms = new MemoryStream())
                {
                    q.Save(ms, ImageFormat.Png);
                    var img = new System.Web.Mvc.TagBuilder("img");
                    img.Attributes.Add("src", String.Format("data:image/png;base64,{0}", Convert.ToBase64String(ms.ToArray())));
                    img.Attributes.Add("alt", alt);
                    img.Attributes.Add("id", "qrcodegenerated");
                    return new HtmlString(img.ToString(System.Web.Mvc.TagRenderMode.SelfClosing));
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
