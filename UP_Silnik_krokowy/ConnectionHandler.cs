using System;
using FTD2XX_NET;
using System.Threading;

namespace UP_Silnik_krokowy
{
    class ConnectionHandler
    {
        public FTDI Device { get; set; }
        public FTDI.FT_STATUS DeviceStatus { get; set; }
        public UInt32 WrittenBytesCount = 0;
        public byte[] stepRightBytes = { 0x01, 0x08, 0x02, 0x04 };
        public byte[] stepLeftBytes = { 0x04, 0x02, 0x08, 0x01 };
        public byte[] stepLeftDownBytes = { 0x10, 0x80, 0x20, 0x40 };
        public byte[] stepRightDownBytes = { 0x40, 0x20, 0x80, 0x10 };
        public byte[] disconnectBytes = { 0x00 };
        public int index = 0;

        public void Connect()
        {
            UInt32 deviceCount = 0;
            Device = new FTDI();
            Device.GetNumberOfDevices(ref deviceCount);
            FTDI.FT_DEVICE_INFO_NODE[] deviceList = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
            Device.GetDeviceList(deviceList);
            Console.WriteLine("Device count: " + deviceList.Length);
            Device.OpenBySerialNumber(deviceList[0].SerialNumber);
            Device.SetBitMode(0xff, 1);
            foreach (FTDI.FT_DEVICE_INFO_NODE d in deviceList)
            {
                Console.WriteLine("Nazwa urządzenia: " + d.Description);
                Console.WriteLine("Serial number urzadzenia: " + d.SerialNumber);
            }
        }

        public void Disconnect()
        {
            WrittenBytesCount = 0;
            Int32 bytesToWrite = 1;
            Device.Write(disconnectBytes, bytesToWrite, ref WrittenBytesCount);
        }

        public void Step(int count, int speed, byte[] bytes)
        {
            WrittenBytesCount = 0;
            Int32 bytesToWrite = 1;
            for (int i = 0; i < count; i++)
            {
                byte[] x = { bytes[index % 4] };
                Device.Write(x, bytesToWrite, ref WrittenBytesCount);
                Thread.Sleep(speed);
                index++;
            }
        }
    }
}
