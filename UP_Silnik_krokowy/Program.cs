using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FTD2XX_NET;

namespace UP_Silnik_krokowy
{
    class Program
    {
        public FTDI Device { get; set; }
        public FTDI.FT_STATUS DeviceStatus { get; set; }
        public UInt32 WrittenBytesCount = 0;
        byte[] stepRightBytes = { 0x01, 0x08, 0x02, 0x04 };
        byte[] stepLeftBytes = { 0x04, 0x02, 0x08, 0x01 };
        byte[] stepLeftDownBytes = { 0x10, 0x80, 0x20, 0x40 };
        byte[] stepRightDownBytes = { 0x40, 0x20, 0x80, 0x10 };
        byte[] disconnectBytes = { 0x00 };
        int index = 0;

        void Connect()
        {
            UInt32 deviceCount = 0;
            Device = new FTDI();
            Device.GetNumberOfDevices(ref deviceCount);
            FTDI.FT_DEVICE_INFO_NODE[] deviceList = new FTDI.FT_DEVICE_INFO_NODE[deviceCount];
            Device.GetDeviceList(deviceList);
            Console.WriteLine("Device count: " + deviceList.Length);
            Device.OpenBySerialNumber(deviceList[0].SerialNumber);
            Device.SetBitMode(0xff, 1);
            foreach(FTDI.FT_DEVICE_INFO_NODE d in deviceList)
            {
                Console.WriteLine("Nazwa urządzenia: " + d.Description);
                Console.WriteLine("Serial number urzadzenia: " + d.SerialNumber);
            }
        }

        void Disconnect()
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
                //Console.WriteLine(bytes[index % 4]);
                Device.Write(x, bytesToWrite, ref WrittenBytesCount);
                Thread.Sleep(speed);
                index++;
            }
        }

        void ShowMenu(Program program)
        {
            Console.WriteLine("1. Obrot w prawo o ilosc krokow");
            Console.WriteLine("2. Obrot w lewo o ilosc krokow");
            Console.WriteLine("3. Obrot w prawo o kat");
            Console.WriteLine("4. Obrot w lewo o kat");
            Console.WriteLine("5. Obrot lewej w dol o ilosc krokow:");
            Console.WriteLine("6. Obrot prawej w dol o ilosc krokow:");
            Console.WriteLine("7. Obrot lewej w dol o kat:");
            Console.WriteLine("8. Obrot prawej w dol o kat:");
            var input = int.Parse(Console.ReadLine());
            if (input == 1)
            {
                Console.WriteLine("Ile krokow w prawo wykonac: ");
                var inputStepsCount = int.Parse(Console.ReadLine());
                program.Step(inputStepsCount, 100, program.stepRightBytes);
                program.Disconnect();
            }
            else if (input == 2)
            {
                Console.WriteLine("Ile krokow w lewo wykonac: ");
                var inputStepsCount = int.Parse(Console.ReadLine());
                program.Step(inputStepsCount, 100, program.stepLeftBytes);
                program.Disconnect();
            }
            else if (input == 3)
            {
                Console.WriteLine("O jaki kat obrocic w prawo: ");
                var inputAngle = int.Parse(Console.ReadLine());
                var stepsCount = (int)(inputAngle / 7.5f);
                program.Step(stepsCount, 100, program.stepRightBytes);
                program.Disconnect();
            }
            else if (input == 4)
            {
                Console.WriteLine("O jaki kat obrocic w lewo: ");
                var inputAngle = int.Parse(Console.ReadLine());
                var stepsCount = (int)(inputAngle / 7.5f);
                program.Step(stepsCount, 100, program.stepLeftBytes);
                program.Disconnect();
            }
            else if (input == 5)
            {
                Console.WriteLine("O ile krokow lewa w dol: ");
                var inputStepsCount = int.Parse(Console.ReadLine());
                program.Step(inputStepsCount, 100, program.stepLeftDownBytes);
                program.Step(1, 100, program.disconnectBytes);

            }
            else if (input == 6)
            {
                Console.WriteLine("O ile krokow prawa w dol: ");
                var inputStepsCount = int.Parse(Console.ReadLine());
                program.Step(inputStepsCount, 100, program.stepRightDownBytes);
                program.Disconnect();
            }
            else if (input == 7)
            {
                Console.WriteLine("O jaki kat obrocic lewa w dol: ");
                var inputAngle = int.Parse(Console.ReadLine());
                var stepsCount = (int)(inputAngle / 7.5f * 5);
                program.Step(stepsCount, 100, program.stepLeftDownBytes);
                program.Disconnect();
            }
            else if (input == 8)
            {
                Console.WriteLine("O jaki kat obrocic prawa w dol: ");
                var inputAngle = int.Parse(Console.ReadLine());
                var stepsCount = (int)(inputAngle / 7.5f * 5);
                program.Step(stepsCount, 100, program.stepRightDownBytes);
                program.Disconnect();
            }

        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Connect();
            while(true)
            {
                program.ShowMenu(program);
            }

        }



    }
}
