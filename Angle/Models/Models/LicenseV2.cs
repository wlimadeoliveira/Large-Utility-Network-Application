using LUNA.Models;
using PrecLicenseGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angle.Models.Models
{
    public class LicenseV2
    {


        public string VBIType { get; set; }
        public string SerialNumber { get; set; }
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public bool LicenseIsInfinite { get; set; }
        public DateTime Warranty { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int NrOfChannels { get; set; }
        public int NrBreakinChannels { get; set; }
        public int SwFlags { get; set; }
        public int NrEmsembles { get; set; }
        public int NrEmsemblesBreakIn { get; set; }

        public 
        Dictionary<string, bool> VBIOptions = new Dictionary<string, bool>(){
            {"2ndOutput", false},
            {"2ndInput", false},
            {"VOIP", false},
            {"Playout", false },
            {"MultiAudio", false },
            {"NoLogin", false },
            {"OEM", false },
};

        public  LicencePacket_V2 getLincence() 
        {
             var lic = new LicencePacket_V2();

            if(VBIType == "DAB")
            {
                lic.licenceID = (ushort)(0x0001 | LicencePacket_V2.versionmask);
            }
            if(VBIType == "FM")
            {
                lic.licenceID = (ushort)(0x0002 | LicencePacket_V2.versionmask);
            }

            lic.serialnumber = Convert.ToUInt32("1712060001");
            if (Customer.ID < 1) { Customer.ID = 0; }
            lic.customernumber = Convert.ToUInt16(Customer.ID);
            lic.warrantyexpirationdate = UInt32.Parse(Warranty.Date.ToString("yyyyMMdd"));



            if (LicenseIsInfinite == true)
                lic.licenseexpirationdate = 0;
            else
                lic.licenseexpirationdate = UInt32.Parse(ExpirationDate.Date.ToString("yyyyMMdd"));
            lic.licensecreationdate = UInt32.Parse(DateTime.Now.ToString("yyyyMMdd"));
            lic.nrChannels = byte.Parse(NrOfChannels.ToString());
            lic.nrBreakInChannels = byte.Parse(NrBreakinChannels.ToString());

            lic.swflags = 0;
            if (VBIOptions["2ndOutput"])
                lic.swflags |= LicencePacket_V2.VBIOptions.SecondRFOutput;
            if (VBIOptions["2ndInput"])
                lic.swflags |= LicencePacket_V2.VBIOptions.SecondRFInput;
            if (VBIOptions["VOIP"])
                lic.swflags |= LicencePacket_V2.VBIOptions.SIP;
            if (VBIOptions["Playout"])
                lic.swflags |= LicencePacket_V2.VBIOptions.Playout;
            if (VBIOptions["MultiAudio"])
                lic.swflags |= LicencePacket_V2.VBIOptions.DualBreakIn;
            if (VBIOptions["NoLogin"])
                lic.swflags |= LicencePacket_V2.VBIOptions.NoLoginForConfig;
            if (VBIOptions["OEM"])
                lic.swflags |= LicencePacket_V2.VBIOptions.OEMMode;

            lic.bkp1 = 0x00;
            lic.bkp2 = 0x00;
            lic.bkp3 = 0x00;
            lic.bkp4 = 0x00;
            lic.bkp5 = 0x00;
            lic.bkp6 = 0x00;

            return lic;




           
        }








    }
}
