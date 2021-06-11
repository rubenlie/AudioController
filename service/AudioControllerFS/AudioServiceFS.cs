using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi.CoreAudio;
using System.IO.Ports;
using System.Threading;


namespace AudioControllerFS
{
    public partial class AudioServiceFS : ServiceBase
    {
        static bool _continue;
        static SerialPort _serialPort;
        Thread readThread = new Thread(Read);
        public AudioServiceFS()
        {
            InitializeComponent();
        }

        public static void Read()
        {
            CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            while (_continue)
            {
                try
                {
                    String message = _serialPort.ReadLine();
                    int convert = int.Parse(message);
                    defaultPlaybackDevice.Volume = convert;
                }
                catch (Exception)
                { }

            }
        }
        protected override void OnStart(string[] args)
        {
            // string message;
            StringComparer stringComparer = StringComparer.OrdinalIgnoreCase;
            readThread = new Thread(Read);

            // Create a new SerialPort object with default settings.
            _serialPort = new SerialPort();

            // Allow the user to set the appropriate properties.
            _serialPort.PortName = "COM10";
            _serialPort.BaudRate = 9600;
            _serialPort.DataBits = 8;
            // Set the read/write timeouts
            _serialPort.ReadTimeout = 500;
            _serialPort.WriteTimeout = 500;

            _serialPort.Open();
            _continue = true;
            readThread.Start();
            // don't join
            



        }
        protected override void OnContinue()
        {
           
        }


        protected override void OnStop()
        {
            _serialPort.Close();
            _continue = false;
            readThread.Join();

        }
    }
}
