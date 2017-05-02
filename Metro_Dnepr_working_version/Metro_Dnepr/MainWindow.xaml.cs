using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Metro_Dnepr
{

    public partial class MainWindow
    {
        //Массив для выбора станций
        private readonly Ellipse[] _mas;
        //Массив всех элементов
        private readonly Ellipse[] _masOll;
        //Массив маленьких элипсов
        private readonly Ellipse[] _masLite;
        //выбранный маршрут
        private List<Ellipse> _wey;
        private int _count;
        private bool _flag = true;
        readonly DispatcherTimer _timer = new DispatcherTimer();
        //начальная точка
        private Ellipse _start;
        //конечная точка
        private Ellipse _stop;
        



        public MainWindow()
        {
            InitializeComponent();
            _mas = new[] { Komunarivska, ProspectSvobodi, Zavodska, Metalurgiv, Metrobudivnikiv, Vokzalna };
            _masOll = new[] { Komunarivska, M11, ProspectSvobodi, M21,M22, Zavodska, M31,M33, Metalurgiv, M41,M42, Metrobudivnikiv, M51,M52, Vokzalna };
            _masLite = new[] {M11, M21, M22, M31, M33, M41, M42, M51, M52};
            Addevent();
            _timer.Tick += Test;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
        }

        private void Addevent()
        {
            foreach (var i in _mas)
            {
                i.MouseDown += delegate
                {
                    if (_start != null && _stop != null)
                    {
                        Timer(false);
                        foreach (var j in _mas)
                        {
                            j.Fill = Brushes.Yellow;
                        }
                        foreach (var j in _masLite)
                        {
                            j.Fill = null;
                        }
                        _start = null;
                        _stop = null;
                        _count = 0;
                        Start.Content = null;
                        Stop.Content = null;
                        Way.Content = null;

                    }
                    i.Fill = Brushes.Aqua;
                    if (!Equals(i, _start) && _start == null)
                    {
                        _start = i;
                        Start.Content = i.Name;
                    }
                        
                    else if (!Equals(i, _stop))
                    {
                        _stop = i;
                        Stop.Content = i.Name;
                    }
                       
                    if (_start != null && _stop != null)
                    {
                        _flag = true;
                        _wey = Run(_start, _stop);
                        Timer(_flag);
                    }

                };
            }
        }
        // выбор пути
        private List<Ellipse> Run(Ellipse start, Ellipse stop)
        {
            var qvery = new List<Ellipse>();
            Ellipse[] mas;
            if (int.Parse(start.Tag.ToString()) < int.Parse(stop.Tag.ToString()))
            {
                mas = _masOll.OrderBy(x => Int16.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in mas)
                {
                    if (int.Parse(i.Tag.ToString()) > int.Parse(start.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) < int.Parse(stop.Tag.ToString()))
                        qvery.Add(i);
                }
            }
            if (int.Parse(start.Tag.ToString()) > int.Parse(stop.Tag.ToString()))
            {
                mas = _masOll.OrderByDescending(x => Int16.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in mas)
                {
                    if (int.Parse(i.Tag.ToString()) < int.Parse(start.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) > int.Parse(stop.Tag.ToString()))
                        qvery.Add(i);
                }
            }
            return qvery;
        }

        private void Timer(bool flag)
        {
            if (flag)
            {
                _timer.Start();
                _flag = false;
            }
            else
            {
                WriteWay();
                _timer.Stop();
            }
        }

        // прорисовка пути
        private void Test(object sender, EventArgs e)
        {
            if (_count == _wey.Count)
            {
                _flag = false;
                Timer(_flag);
            }
            while ( _count < _wey.Count)
            {
                _wey[_count].Fill = Brushes.Aqua;
                _count++;
                break;
            }
        }

        private void WriteWay()
        {
            Way.Content = $"{_start.Name} -->";
            foreach (var i in _wey)
            {
                if (i.Name == Komunarivska.Name || i.Name == ProspectSvobodi.Name || i.Name == Zavodska.Name ||
                    i.Name == Metalurgiv.Name || i.Name == Metrobudivnikiv.Name || i.Name == Vokzalna.Name)
                    Way.Content += $"\n--> {i.Name} -->";
            }
            Way.Content += $"\n--> {_start.Name}";
        }
    }
}
