using System;
using System.IO.Ports;

namespace HaenggiModel.DeviceCommunication
{
    [Obsolete]
    public class SerialPortEvent
    {
        private SerialPort serialPort;

        /// <summary>
        /// The data received handler
        /// </summary>
        public Action<byte[]> DataReceivedHandler;

        //Created the actual serial port in the constructor here, 
        //as it makes more sense than having the caller need to do it.
        //you'll also need access to it in the event handler to read the data
        public SerialPortEvent(SerialPort serialPort)
        {
            this.serialPort = serialPort;

            serialPort.DataReceived += new SerialDataReceivedEventHandler(SerialPortDataReceived);
        }

        /// <summary>
        /// Serials the port data received.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="SerialDataReceivedEventArgs"/> instance containing the event data.</param>
        public void SerialPortDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //no. of data at the port
                int byteToRead = serialPort.BytesToRead;

                //create array to store buffer data
                var inputData = new byte[byteToRead];

                //read the data and store
                serialPort.Read(inputData, 0, byteToRead);

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
