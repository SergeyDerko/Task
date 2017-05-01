using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Metro
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {

        private int _count;
        private bool _flag = true;
        readonly DispatcherTimer _timer = new DispatcherTimer();
        private Ellipse _start;
        private Ellipse _stop;

        readonly Ellipse[] _ollEllipses;
            public MainWindow()
        {
            InitializeComponent();
            _ollEllipses = new []
            {
                Red1,Red2,Red3,Red4,Red5,Red6,Red7,Red8,Red9,Red10,Red11,Red12,Red13,Red14,
                Red15, Red16,Red17,Red18,Blue1,Blue2,Blue3,Blue4,Blue5,Blue6,Blue7,Blue8,
                Blue9,Blue10,Blue11,Blue12,Blue13,Blue14,Blue15,Blue16,Blue17,Green1,Green2,Green3,
                Green4,Green5,Green6,Green7,Green8,Green9,Green10,Green11,Green12,Green13,Green14
            };

            Addevent();
            _timer.Tick += Test;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
        }

        private void Addevent()
        {
            foreach (var i in _ollEllipses)
            {
                i.MouseDown += delegate
                {
                    i.Fill = Brushes.Aqua;
                    //    if(!Equals(i, _start))
                    //        _start = i;
                    //    if (!Equals(i, _stop))
                    //        _stop = i;
                    //    if (_start!= null && _stop!= null)
                    //        Run(_start,_stop);
                    //};
                };
            }
        }

        private void Run(Ellipse start, Ellipse stop)
        {
            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Timer(_flag);
            for (int j = 0; j < _ollEllipses.Length; j++)
            {
                if (j < 18)
                    _ollEllipses[j].Fill = Brushes.Red;
                if (j >= 18 && j < 35)
                    _ollEllipses[j].Fill = Brushes.Blue;
                if (j >= 35)
                    _ollEllipses[j].Fill = Brushes.Green;
            }
            _count = 0;
            _flag = true;
            Timer(_flag);
            

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            for (int j = 0; j < _ollEllipses.Length; j++)
            {
                if (j < 18)
                    _ollEllipses[j].Fill = Brushes.Red;
                if (j >= 18 && j < 35)
                    _ollEllipses[j].Fill = Brushes.Blue;
                if (j >= 35)
                    _ollEllipses[j].Fill = Brushes.Green;
            }
            _count = 0;
            Timer(false);
        }

        



         /////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Timer( bool flag)
        {

            if (flag)
            {
                _timer.Start();
                _flag = false;
            }
                
            else
                _timer.Stop();
           
        }

       
        private void Test(object sender, EventArgs e)
        {
            if (_count == _ollEllipses.Length)
                _flag = false;
            while (_count<_ollEllipses.Length)
            {
                _ollEllipses[_count].Fill = Brushes.Yellow;
                _count++;
                break;
            }
           
        }

        
    }
}
