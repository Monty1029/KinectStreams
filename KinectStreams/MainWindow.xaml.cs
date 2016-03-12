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
        //Mode _mode = Mode.Color;

        KinectSensor _sensor;
        MultiSourceFrameReader _reader;
        IList<Body> _bodies;

        bool _displayBody = false;
        
        public MainWindow()
        {
            InitializeComponent();
        }
        
        //Reads the stream
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

        //Turns off the stream when GUI is closed
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

            var colorFrame = reference.ColorFrameReference.AcquireFrame();
            var depthFrame = reference.DepthFrameReference.AcquireFrame();
            var infraredFrame = reference.InfraredFrameReference.AcquireFrame();

            using (colorFrame)
            {
                //Color
                if (colorFrame != null)
                {
                        camera1.Source = colorFrame.ToBitmap();
                }

                //Depth
                using (depthFrame)
                {
                    if (depthFrame != null)
                    {
                        camera2.Source = depthFrame.ToBitmap();
                    }
                }

                //Infrared
                using (infraredFrame)
                {
                    if (infraredFrame != null)
                    {
                        camera3.Source = infraredFrame.ToBitmap();
                    }
                }
            }
        }

        /* private void Color_Click(object sender, RoutedEventArgs e)
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

        private void Body_Click(object sender, RoutedEventArgs e)
        {
            _displayBody = !_displayBody;
        } */
    }

    /* public enum Mode
    {
        Color,
        Depth,
        Infrared
    } */
}
