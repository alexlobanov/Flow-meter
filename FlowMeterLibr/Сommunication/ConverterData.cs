using System;
using System.Runtime.InteropServices;


namespace FlowMeterLibr.Сommunication
{
    public static class ConverterData
    {
        public static T ToStruct<T>(byte[] datas) where T : struct
        {
            var newArray = new byte[61];
            Array.Copy(datas, 3, newArray, 0,60);
            var config = new T();
            var size = Marshal.SizeOf(default(T));
            var ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(newArray, 0, ptr, size);

            config = (T)Marshal.PtrToStructure(ptr, config.GetType());
            Marshal.FreeHGlobal(ptr);

            return config;
        }
    }
}
