using AForge.Video.DirectShow;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private FilterInfoCollection _videoDevices = null;
        private VideoCaptureDevice _cameraDevice = null;
        private Stopwatch _stopWatch = new Stopwatch();
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                int num = _videoDevices.Count;
                Console.WriteLine(num);
                Console.WriteLine(_videoDevices[0].Name);
                Console.WriteLine(_videoDevices[0].MonikerString);

                _cameraDevice = new VideoCaptureDevice(_videoDevices[0].MonikerString);
                _cameraDevice.NewFrame += OnCameraDevice_NewFrame;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void OnCameraDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            //Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            _stopWatch.Stop();
            string run_time = _stopWatch.ElapsedMilliseconds.ToString();
            Console.WriteLine("用时{0}毫秒", run_time);//获取当前实例测量得出的总运行时间（以毫秒为单位）
            //label1.Text = run_time;
            //label1.Text = run_time;
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            //label1.Text = "12";
            //textBox1.Text = run_time;
            pictureBox2.Image = bmp;
            _stopWatch.Restart();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnStopCamera();
        }

        private void OnStartCamera()
        {
            if (_cameraDevice != null)
                _cameraDevice.Start();
        }
        private void OnStopCamera()
        {
            if (_cameraDevice != null && _cameraDevice.IsRunning)
                _cameraDevice.Stop();
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            OnStopCamera();
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            OnStartCamera();
            _stopWatch.Start();
        }
    }
}
