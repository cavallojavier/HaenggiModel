using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace DeviceCommunication
{
    /// <summary>
    /// 
    /// </summary>
    public class PortCommunication : SerialPort
    {
        private const int baudRates = 9600;
        private const Parity parity = Parity.None;
        private const int dataBits = 8;
        private const StopBits stopBits = System.IO.Ports.StopBits.One;

        private const string PortOpenMessage = "Port {0} is now open.";
        private const string PortCloseMessage = "Port {0} is now closed.";
        private const string PortFoundMessage = "Device Found at Port {0}.";
        private const string PortNotFoundMessage = "Device NOT Found.";
        private const string PortErrorMessage = "Error when trying to use Device found at Port {0}.";
        private const string ReceivedAcknoledge = "oki";
        private const string SendAcknoledge = "s";

        public static string AvoidInputData = "\n\r\0";

        private static SerialPort instance;

        /// <summary>
        /// The data received handler
        /// </summary>
        public static Action<byte[]> DataReceivedHandler;

        /// <summary>
        /// The device port name
        /// </summary>
        private static string devicePortName;

        /// <summary>
        /// Gets the device port.
        /// </summary>
        /// <returns></returns>
        private static string GetDevicePort()
        {
            if(!string.IsNullOrEmpty(devicePortName))
            {
                return devicePortName;
            }

            var names = SerialPort.GetPortNames();

            foreach (var name in names)
            {
                if (!IsDeviceConnected(name)) continue;

                devicePortName = name;
                break;
            }

            if(string.IsNullOrEmpty(devicePortName))
            {
                Console.WriteLine(PortNotFoundMessage);
                throw new IOException("Port Not Found Message");
            }

            return devicePortName;
        }

        /// <summary>
        /// Setups the device.
        /// </summary>
        public static void SetupDevice(SerialPort port, SetupMode setupMode)
        {
            var keepPortOpen = port.IsOpen;
            if (!port.IsOpen)
                port.Open();

            port.WriteLine(((int)setupMode).ToString());

            Console.WriteLine("setup mode: " + setupMode);

            if (!keepPortOpen)
            {
                port.Close();
            }
        }

        /// <summary>
        /// Opens the port.
        /// </summary>
        public static void OpenPort(SerialPort port)
        {
            try
            {
                if (port != null && !port.IsOpen)
                {
                    port.Open();
                    Console.WriteLine(string.Format(PortOpenMessage, port.PortName));

                    return;
                }

                Console.WriteLine(string.Format(PortErrorMessage, port.PortName));
            }
            catch (IOException)
            {
                Console.WriteLine(string.Format(PortErrorMessage, port.PortName));
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format(PortErrorMessage, port.PortName));
                throw ex;
            }
        }

        /// <summary>
        /// Closes the port.
        /// </summary>
        public static void ClosePort(SerialPort port)
        {
            if(port != null && port.IsOpen)
            {
                port.Close();
                Console.WriteLine(string.Format(PortCloseMessage, port.PortName));
            }
        }

        /// <summary>
        /// Determines whether [is device connected].
        /// </summary>
        public static bool IsDeviceConnected(string portName)
        {
            using (var sp = new System.IO.Ports.SerialPort(portName, baudRates, parity, dataBits, stopBits))
            {
                sp.Open();

                sp.WriteLine(SendAcknoledge);

                int byteToRead = sp.BytesToRead;
                var inputData = new byte[byteToRead];

                sp.Read(inputData, 0, byteToRead);

                

                var readData = System.Text.Encoding.Default.GetString(inputData);

                Console.WriteLine(string.Format(PortFoundMessage, sp.PortName));
                Console.WriteLine(readData);

                var result = readData.StartsWith(ReceivedAcknoledge);

                if (result)
                {
                    SetupDevice(sp, SetupMode.OneData);
                }

                sp.Close();

                return result;
            }
        }

        /// <summary>
        /// Gets the device.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">Serial Port not Found!</exception>
        public static SerialPort GetDevice()
        {
            try
            {
                if (instance == null)
                {
                    instance = new SerialPort(GetDevicePort(), baudRates, parity, dataBits, stopBits);
                    instance.DataReceived += new SerialDataReceivedEventHandler(SerialPortDataReceived);

                    return instance;
                }

                return instance;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static void SetCurrentDataEventHanlder(Action<byte[]> handler)
        {
            DataReceivedHandler = handler;
        }

        /// <summary>
        /// Serials the port data received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SerialDataReceivedEventArgs"/> instance containing the event data.</param>
        public static void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //no. of data at the port
                int byteToRead = instance.BytesToRead;

                //create array to store buffer data
                var inputData = new byte[byteToRead];

                //read the data and store
                instance.Read(inputData, 0, byteToRead);

                if (DataReceivedHandler != null)
                {
                    DataReceivedHandler(inputData);
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message, "Data Received Event");
            }
        }
    }
}
