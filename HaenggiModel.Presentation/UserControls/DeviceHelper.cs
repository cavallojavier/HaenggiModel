using DeviceCommunication;
using System.IO.Ports;
using CalculationHelper;
using System;

namespace HaenggiModel.Presentation.UserControls
{
    public class DeviceHelper
    {
        public static SerialPort Device
        {
            get
            {
                var devicePort = PortCommunication.GetDevice() ?? new SerialPort();

                #if DEBUG
                if (devicePort == null)
                    devicePort = new SerialPort();
#endif

                return devicePort;
            }
        }

        public static void SetDataEventhandler(Action<byte[]> handler)
        {
            PortCommunication.SetCurrentDataEventHanlder(handler);
        }

        public static double? GetReceivedData(byte[] data)
        {
            var value = System.Text.Encoding.Default.GetString(data);

            if (value.Equals(PortCommunication.AvoidInputData))
            {
                return null;
            }

            value = value.Replace(PortCommunication.AvoidInputData, string.Empty);

            return MessureResultFormatter.FormatResult(value);
        }
    }
}
