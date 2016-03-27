using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KinectStreams
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Mode _mode = Mode.Color;

        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> _bodies;

        bool _displayBody = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _sensor = KinectSensor.GetDefault();

            if (_sensor != null)
            {
                _sensor.Open();

                _reader = _sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Color | FrameSourceTypes.Depth | FrameSourceTypes.Infrared);
                _reader.MultiSourceFrameArrived += Reader_MultiSourceFrameArrived;
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }

            if (_sensor != null)
            {
                _sensor.Close();
            }
        }

        void Reader_MultiSourceFrameArrived(object sender, MultiSourceFrameArrivedEventArgs e)
        {
            var reference = e.FrameReference.AcquireFrame();

            // Color
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Color)
                    {
                        camera.Source = frame.ToBitmap();
                    }
                }
            }

            // Depth
            using (var frame = reference.DepthFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Depth)
                    {
                        camera.Source = frame.ToBitmap();
                    }
                }
            }

            // Infrared
            using (var frame = reference.InfraredFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Infrared)
                    {
                        camera.Source = frame.ToBitmap();
                    }
                }
            }

            // Blue
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Blue)
                    {
                        camera.Source = frame.ToBitmapBlue();
                    }
                }
            }

            // Green
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Green)
                    {
                        camera.Source = frame.ToBitmapGreen();
                    }
                }
            }

            // Red
            using (var frame = reference.ColorFrameReference.AcquireFrame())
            {
                if (frame != null)
                {
                    if (_mode == Mode.Red)
                    {
                        camera.Source = frame.ToBitmapRed();
                    }
                }
            }
        }

        private void Color_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Color;
        }

        private void Depth_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Depth;
        }

        private void Infrared_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Infrared;
        }

        private void Blue_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Blue;
        }

        private void Green_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Green;
        }

        private void Red_Click(object sender, RoutedEventArgs e)
        {
            _mode = Mode.Red;
        }

        private void Body_Click(object sender, RoutedEventArgs e)
        {
            _displayBody = !_displayBody;
        }
    }

    public enum Mode
    {
        Color,
        Depth,
        Infrared,
        Blue,
        Green,
        Red
    }
}