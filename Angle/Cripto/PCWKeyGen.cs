using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PrecLicenseGenerator
{
    public class PCWKeyGen
    {

        /// <summary>
        /// Generate key from licence structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lic">license structure</param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static byte[] generateKey<T>(T lic, UInt32 pass) where T : struct
        {
            byte[] liccontent = getBytes(lic);
            int keysize = liccontent.Length - 2;
            ushort[] crcval = new ushort[] { calcCRC16(liccontent.Take(keysize).ToArray()) };

            Buffer.BlockCopy(crcval, 0, liccontent, keysize, 2 * crcval.Length);
            byte[] password = makeKey(pass, keysize);

            Debug.WriteLine("Key is: {0}", BitConverter.ToString(password).Replace("-", string.Empty));
            var enc = PCWCryptographer.EncryptByteArray(liccontent, password);
            try
            {
                Debug.WriteLine("Encypted:   {0}", keyAsString(enc));
                Debug.WriteLine("Original:   {0}", keyAsString(liccontent));
                var dec = PCWCryptographer.DecryptByteArray(enc, password);
                Debug.WriteLine("Round Trip: {0}", keyAsString(dec));
                if (!dec.SequenceEqual(liccontent))
                {
                    throw new Exception("Decryption is not equal to Encryption! Invalid Key");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error: {0}", ex.Message);
            }
            return enc;
        }

        /// <summary>
        /// Generate key from licence structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lic">license structure</param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static void loadLicense<T>(byte[] enc, UInt32 pass, ref T lic) where T : struct
        {
            var password = makeKey(pass);
            // decrypt
            try
            {
                var dec = PCWCryptographer.DecryptByteArray(enc, password);
                setBytes(dec, ref lic);
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Failed to decrypt License!");
                throw new Exception("License decryption failed");
            }
        }

        public static ushort getLicenseVersion(byte[] enc, UInt32 pass)
        {
            var password = makeKey(pass);
            try
            {
                var dec = PCWCryptographer.DecryptByteArray(enc, password);
                return BitConverter.ToUInt16(dec.Take(2).ToArray(), 0);
            }
            catch (Exception)
            {
                Console.Error.WriteLine("Failed to get license version License!");
                throw new Exception("License decryption failed");
            }
        }

        public static bool validateLicense<T>(ref T lic) where T : struct
        {
            // check CRC
            ushort crcval = 0;
            ushort crcorig = 0;
            try
            {
                byte[] liccontent = getBytes(lic);
                int keysize = liccontent.Length - 2;
                crcval = calcCRC16(liccontent.Take(keysize).ToArray());
                crcorig = BitConverter.ToUInt16(liccontent.Skip(keysize).Take(2).ToArray(), 0);
            }
            catch (Exception)
            {
                return false;
            }

            if (crcval != crcorig)
            {
                Console.Error.WriteLine("CRC Mismatch - decryption invalid");
                return false;
            }
            return true;
        }

        private static byte[] makeKey(UInt32 pass, int keysize = 30)
        {
            byte[] hwkey = BitConverter.GetBytes(pass);
            byte[] key = new byte[keysize + 2];
            for (int i = 0; i < key.Length; i++)
            {
                key[i] = hwkey[i % hwkey.Length];
            }
            return key;
        }


        /// <summary>
        /// Convert Hex String to Byte Array
        /// </summary>
        /// <param name="hex">hex string, can either be preceded by 0x or not e.g. 0xAF12 or AF12</param>
        /// <returns></returns>
        public static byte[] stringToByteArray(string hex)
        {
            int character_index = (hex.StartsWith("0x", StringComparison.Ordinal)) ? 2 : 0;
            hex = hex.Substring(character_index);
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// get Bytes of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str">object</param>
        /// <returns>byte[] of object memory</returns>
        public static byte[] getBytes<T>(T str) where T : struct
        {
            int size = Marshal.SizeOf(str);
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(str, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }

        /// <summary>
        /// assign Bytes of object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr">bytes to assign</param>
        public static void setBytes<T>(byte[] arr, ref T str) where T : struct
        {
            int size = Marshal.SizeOf(str);
            if (size != arr.Length)
            {
                throw new ArgumentOutOfRangeException();
            }
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.Copy(arr, 0, ptr, size);
            str = Marshal.PtrToStructure<T>(ptr);
            Marshal.FreeHGlobal(ptr);
        }



        public static string keyAsString(byte[] enc)
        {
            return BitConverter.ToString(enc).Replace("-", string.Empty);
        }


        /// <summary>
        ///  calculate cyclic redundancy code 16
        /// </summary>
        /// <param name="data">data to calculate crc from</param>
        /// <param name="crc" optional></param>
        /// <param name="poly" optional></param>
        /// <returns></returns>
        public static ushort calcCRC16(byte[] data, ushort crc = 0xFFFF, ushort poly = 0x1021)
        {
            for (int i = 0; i < data.Length; i++)
            {
                crc ^= (ushort)(data[i] << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x8000) > 0)
                        crc = (ushort)((crc << 1) ^ poly);
                    else
                        crc <<= 1;
                }
            }
            return crc;
        }
    }
}