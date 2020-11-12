using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PrecLicenseGenerator
{

    [StructLayout(LayoutKind.Sequential)]
    public struct LicencePacket_V1
    {
        public const ushort versionmask = 0x0000;
        [Flags]
        public enum VBIOptions : byte
        {
            SecondRFOutput = 0x1,
            SecondRFInput = 0x2,
            SNMPProtocol = 0x4,
            VoIP = 0x8,
            SecondBreakInChannel = 0x10,
            Reporting = 0x20,
            NoLoginForConfig = 0x40,
            OEMMode = 0x80
        }
        // header: 20 byte
        public UInt16 licenceID;                // 2 bytes, The value must be "0xFF+'SMB'". 
        public UInt16 customernumber;           // 2 bytes
        public UInt32 serialnumber;             // 4 bytes
        public UInt32 warrantyexpirationdate;   // 4 bytes
        public UInt32 licenseexpirationdate;    // 4 bytes
        public UInt32 licensecreationdate;      // 4 bytes

        // contents: 10 bytes
        public byte nrEnsemblesAndBreakIn;      // 1 byte, the upper nibble is the inverse of the number of break-in channels
                                                //     the lower nibble is the number of channels
        public byte nrEnsembles                 // 1 byte
        {
            get => (byte)(nrEnsemblesAndBreakIn & 0x0F);
            set => nrEnsemblesAndBreakIn = (byte)((byte)(nrEnsemblesAndBreakIn & 0xF0) | (byte)(value & 0x0F));
        }
        public byte nrBreakInChannels           // 1 byte
        {
            get => (byte)((byte)~(nrEnsemblesAndBreakIn >> 4) & 0x0F);
            set => nrEnsemblesAndBreakIn = (byte)((byte)((byte)~((byte)(value & 0x0F) << 4) & 0xF0) | (byte)(nrEnsemblesAndBreakIn & 0x0F));
        }

        public VBIOptions swflags;              // 1 byte for software flags
        public byte bkp1;                       // 1 byte for software flags
        public byte bkp2;                       // 1 byte for software flags
        public byte bkp3;                       // 1 byte for software flags
        public byte bkp4;                       // 1 byte for software flags
        public byte bkp5;                       // 1 byte for software flags
        public byte bkp6;                       // 1 byte for software flags

        public UInt16 crc;                      // crc 2 bytes

        public string getString()
        {
            string str = $"License Information\n" +
                $"ID: {licenceID}\n" +
                $"Customer Number: {customernumber}\n" +
                $"Serial Number: {serialnumber}\n" +
                $"WarrantyExpiration: {warrantyexpirationdate}\n" +
                $"LicenseExpiration: {licenseexpirationdate}\n" +
                $"LicenseCreation: {licensecreationdate}\n" +
                $"\nChannel information:\n" +
                $" - Nr Channels: {nrEnsembles}\n" +
                $" - Nr Break-in Channels: {nrBreakInChannels}\n" +
                $"\nSoftware Options:\n" +
                $" - SecondRFOutput: {(swflags & VBIOptions.SecondRFOutput) > 0}\n" +
                $" - SecondRFInput: {swflags & VBIOptions.SecondRFInput}\n" +
                $" - SNMPProtocol: {swflags & VBIOptions.SNMPProtocol}\n" +
                $" - VoIP: {swflags & VBIOptions.VoIP}\n" +
                $" - SecondBreakInChannel: {swflags & VBIOptions.SecondBreakInChannel}\n" +
                $" - Reporting: {swflags & VBIOptions.Reporting}\n" +
                $" - NoLoginForConfig: {swflags & VBIOptions.NoLoginForConfig}\n" +
                $" - OEMMode: {swflags & VBIOptions.OEMMode}\n";

            return str;
        }
        // total size: header(20) + contents(10) + crc(2) = 34
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LicencePacket_V2
    {
        public const ushort versionmask = 0x0100;
        [Flags]
        public enum VBIOptions : byte
        {
            SecondRFOutput = 0x1,
            SecondRFInput = 0x2,
            SIP = 0x4,
            Playout = 0x8,
            DualBreakIn = 0x10,
            NoLoginForConfig = 0x20,
            OEMMode = 0x40,
            Unused = 0x80
        }
        // header: 20 byte
        public UInt16 licenceID;                // 2 bytes, The value must be "0xFF+'SMB'". 
        public UInt16 customernumber;           // 2 bytes
        public UInt32 serialnumber;             // 4 bytes
        public UInt32 warrantyexpirationdate;   // 4 bytes
        public UInt32 licenseexpirationdate;    // 4 bytes
        public UInt32 licensecreationdate;      // 4 bytes

        // contents: 10 bytes
        public byte nrChannels;                 // 1 bytes
        public byte nrBreakInChannels;          // 1 bytes
        public VBIOptions swflags;              // 1 byte for software flags
        public byte bkp1;                       // 1 byte for software flags
        public byte bkp2;                       // 1 byte for software flags
        public byte bkp3;                       // 1 byte for software flags
        public byte bkp4;                       // 1 byte for software flags
        public byte bkp5;                       // 1 byte for software flags
        public byte bkp6;                       // 1 byte for software flags
        public byte bkp7;                       // 1 byte for software flags

        // crc 2 bytes
        public UInt16 crc;

        // total size: header(20) + contents(10) + crc(2) = 34
    }

}