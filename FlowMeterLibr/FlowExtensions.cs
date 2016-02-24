using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FlowMeterLibr
{
    public static class FlowExtensions
    {

        public static T ToStruct<T>(this byte[] datas) where T : struct
        {
            var newArray = new byte[61];
            var indexToStopParse = datas.Length;
            Array.Copy(datas, 2, newArray, 0, indexToStopParse - 2);
            var config = new T();
            var size = Marshal.SizeOf(default(T));
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(newArray, 0, ptr, size);

            config = (T)Marshal.PtrToStructure(ptr, config.GetType());
            Marshal.FreeHGlobal(ptr);

            return config;
        }

        public static byte[] GetBytes<T>(this T strucToSend) where T : struct 
        {
            int size = Marshal.SizeOf(strucToSend);
            byte[] arr = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(strucToSend, ptr, true);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }


        public static string BinaryStringToHexString(this string binary)
        {
            var result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... Will throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        public static string IntTohHexString(this uint intToParse)
        {
            var dec = intToParse;
            var hex = String.Format("0x" + "{0:X}", dec);
            return hex;

        }

        public static int GetBit(this byte b, int bitNumber)
        {
            return (b & (1 << bitNumber));
        }

        public static string GetBitString(this byte b)
        {
            return Convert.ToString(b, 2).PadLeft(8, '0');
        }

        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();

        }



    }
}
