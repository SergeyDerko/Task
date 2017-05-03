using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Metro
{
    public partial class MainWindow
    {
        //Массивы для выбора станций
        private readonly Ellipse[] _masStations;
        //Массив маленьких элипсов
        private readonly Ellipse[] _masLite;
        //Массивы станций по веткам
        private readonly Ellipse[] _masStationsRed;
        private readonly Ellipse[] _masStationsBlue;
        private readonly Ellipse[] _masStationsGreen;
        //Массивы всех элементов по веткам
        private readonly Ellipse[] _masOllRed;
        private readonly Ellipse[] _masOllBlue;
        private readonly Ellipse[] _masOllGreen;
        //выбранный маршрут
        private List<Ellipse> _wey;
        private int _count;
        readonly DispatcherTimer _timer = new DispatcherTimer();
        readonly DispatcherTimer _timerColor = new DispatcherTimer();
        //начальная точка
        private Ellipse _start;
        //конечная точка
        private Ellipse _stop;
        bool _flagColor = true;

        public MainWindow()
        {
            InitializeComponent();
            _masStations = new[] { Академмістечко,Житомирська,Святошин,Нивки,Берестейська,Шулявська,ПолітехнічнийІнститут,Вокзальна,
                                   Університет,Театральна,Хрещатик,Арсенальна,Дніпро,Гідропарк,Лівобережна,Дарниця,Чернігівська,Лісова,
                                   ГероївДніпра,Мінська,Оболонь,Петрівка,ТарасаШевченка,КонтрактоваПлоща, ПоштоваПлоща,МайданНезалежності,
                                   ПлощаЛьваТолстого,Олімпійска,ПалацУкраїна,Либідська,Деміївська,Голосіївська,Васильківська,ВиставковийЦентр,
                                   Іподром,Теремки,Сирець,Дрогожичі,Лукянівська,ЗолотіВорота,ПалацСпорту,Кловська,Печерська,ДружбиНародів,
                                   Видубичі,Славутич,Осокорки,Позняки,Харківська,Бориспільська,Вирлиця,ЧервонийХутір};
            _masLite = new[] {R1,R2,R3,R4,R5,R6,R7,R8,R9,R10,R11,R12,R13,R14,R15,R16,R17,R18,R19,R20,R21,R22,R23,R24,R25,R26,R27,
                              R28,R29,R30,R31,B1,B2,B3,B4,B5,B6,B7,B8,B9,B10,B11,B12,B13,B14,B15,B16,B17,B18,B19,B20,B21,B22,B23,
                              B24,B25,B26,B27,B28,B29,B30,B31,B32,B33,B34,B35,B36,B37,B38,G1,G2,G3,G4,G5,G6,G7,G8,G9,G10,G11,G12,
                              G13,G14,G15,G16,G17,G18,G19,G20,G21,G22,G23,G24,G25,G26,G27,G28,G29,G30,G31,G32,G33,G34,G35,G36,G37,
                              G38,G39,G40,G41,G42,G43,G44,G45};
            _masStationsRed = new[] {Академмістечко,Житомирська,Святошин,Нивки,Берестейська,Шулявська,ПолітехнічнийІнститут,Вокзальна,
                                     Університет,Театральна,Хрещатик,Арсенальна,Дніпро,Гідропарк,Лівобережна,Дарниця,Чернігівська,Лісова};
            _masStationsBlue = new[] {ГероївДніпра,Мінська,Оболонь,Петрівка,ТарасаШевченка,КонтрактоваПлоща,
                                      ПоштоваПлоща,МайданНезалежності,ПлощаЛьваТолстого,Олімпійска,ПалацУкраїна,Либідська,Деміївська,
                                      Голосіївська,Васильківська,ВиставковийЦентр,Іподром,Теремки};
            _masStationsGreen = new[] {Сирець,Дрогожичі,Лукянівська,ЗолотіВорота,ПалацСпорту,Кловська,Печерська,ДружбиНародів,Видубичі,
                                       Славутич,Осокорки,Позняки,Харківська,Бориспільська,Вирлиця,ЧервонийХутір};
            _masOllRed = new[] { Академмістечко,R1,R2,Житомирська,R3,Святошин,R4,Нивки,R5,Берестейська,R6,Шулявська,R7,R8,ПолітехнічнийІнститут,
                                R9,R10,R11,Вокзальна,R12,R13,Університет,R14,R15,Театральна,R16,Хрещатик,R17,R18,R19, Арсенальна,R20,R21,Дніпро,
                                R22,R23,R24,Гідропарк,R25,R26,R27,Лівобережна,R28,R29,Дарниця,R30,Чернігівська,R31,Лісова };
            _masOllBlue = new[] { ГероївДніпра, B1,Мінська,B2,Оболонь,B3,B4,Петрівка,B5,B6,B7,ТарасаШевченка,B8,B9,КонтрактоваПлоща,B10,B11,B12,
                                  ПоштоваПлоща,B13,B14,B15,МайданНезалежності,B16,B17,ПлощаЛьваТолстого,B18,Олімпійска,B19,B20,B21,ПалацУкраїна,
                                  B22,B23,Либідська,B24,B25,B26,Деміївська,B27,B28,Голосіївська,B29,B30,B31,B32,Васильківська,B33,ВиставковийЦентр,
                                  B34,Іподром,B35,B36,B37,B38,Теремки};
            _masOllGreen = new[] {Сирець,G1,G2,Дрогожичі,G3,G4,G5,G6,G7,G8,Лукянівська,G9,G10,G11,G12,G13,G14,G15,G16,G17,ЗолотіВорота,G18,G19,
                                  ПалацСпорту,G20,G21,Кловська,G22,G23,G24,Печерська,G25,G26,G27,ДружбиНародів,G28,G29,G30,G31,G32,Видубичі,
                                  G33,G34,G35,G36,G37,G38,G39,Славутич,G40,Осокорки,G41,Позняки,G42,Харківська,G43,Бориспільська,G44,Вирлиця,G45,ЧервонийХутір};
            Addevent();
            _timer.Tick += TimerOffOn;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timerColor.Tick += TimerColor;
            _timerColor.Interval = new TimeSpan(0, 0, 0, 0, 300);
        }

        //добавляем событие, нажатие на элипс станции
        private void Addevent()
        {

            foreach (var i in _masStations)
            {
                i.MouseDown += delegate
                {
                    if (_start != null && _stop != null)
                        NullOll();
                    i.Fill = Brushes.Aqua;
                    if (!Equals(i, _start) && _start == null)
                    {
                        _start = i;
                        Way.Content = $"   Начальна станцiя :  {i.Name}";
                    }
                    else
                    {
                        _stop = i;
                        Way.Content += $"\n                               >>>ПОШУК ШЛЯХУ<<<\n      Кiнцева станцiя :   {i.Name}";
                    }
                    if (_start != null && _stop != null)
                    {
                        _wey = Run(_start, _stop);
                        _timer.Start();
                    }
                };
            }
        }
        //обнуление 
        private void NullOll()
        {
            _timer.Stop();
            _timerColor.Stop();
            _flagColor = true;
            Way.Content = null;
            foreach (var j in _masStationsRed)
                j.Fill = Brushes.Red;
            foreach (var j in _masStationsBlue)
                j.Fill = Brushes.Blue;
            foreach (var j in _masStationsGreen)
                j.Fill = Brushes.Green;
            foreach (var j in _masLite)
                j.Fill = null;
            _start = null;
            _stop = null;
            _count = 0;
        }

        //поиск пути
        private List<Ellipse> Run(Ellipse start, Ellipse stop)
        {
            var qvery = new List<Ellipse>();
            //между переходными станциями
            if (Equals(_start, ПлощаЛьваТолстого) && Equals(_stop, Театральна))
            {
                qvery.Add(ПалацСпорту);
                qvery.Add(G19);
                qvery.Add(G18);
                qvery.Add(ЗолотіВорота);
                return qvery;
            }
            if (Equals(_stop, ПлощаЛьваТолстого) && Equals(_start, Театральна))
            {
                qvery.Add(ЗолотіВорота);
                qvery.Add(G18);
                qvery.Add(G19);
                qvery.Add(ПалацСпорту);
                return qvery;
            }
            if (Equals(_start, ЗолотіВорота) && Equals(_stop, МайданНезалежності))
            {
                qvery.Add(Театральна);
                qvery.Add(R16);
                qvery.Add(Хрещатик);
                return qvery;
            }
            if (Equals(_stop, ЗолотіВорота) && Equals(_start, МайданНезалежності))
            {
                qvery.Add(Хрещатик);
                qvery.Add(R16);
                qvery.Add(Театральна);
                return qvery;
            }
            if (Equals(_start, ПалацСпорту) && Equals(_stop, Хрещатик))
            {
                qvery.Add(ПлощаЛьваТолстого);
                qvery.Add(B17);
                qvery.Add(B16);
                qvery.Add(МайданНезалежності);
                return qvery;
            }
            if (Equals(_stop, ПалацСпорту) && Equals(_start, Хрещатик))
            {
                qvery.Add(МайданНезалежності);
                qvery.Add(B16);
                qvery.Add(B17);
                qvery.Add(ПлощаЛьваТолстого);
                return qvery;
            }
            //Если выбранна только красная линия
            if (_masStationsRed.Contains(start) && _masStationsRed.Contains(stop))
                qvery = PathLine(_masOllRed);
            //Если выбранна только синяя линия
            if (_masStationsBlue.Contains(start) && _masStationsBlue.Contains(stop))
                qvery = PathLine(_masOllBlue);
            //Если выбранна только зеленая линия
            if (_masStationsGreen.Contains(start) && _masStationsGreen.Contains(stop))
                qvery = PathLine(_masOllGreen);
            //Если красная начальная и синяя конечная линия
            if (_masStationsRed.Contains(start) && _masStationsBlue.Contains(stop))
                qvery = PathTwoLine(_masOllRed, _masOllBlue, 27, 23);
            //если красная начальная и зеленая конечная
            if (_masStationsRed.Contains(start) && _masStationsGreen.Contains(stop))
                qvery = PathTwoLine(_masOllRed, _masOllGreen, 25, 21);
            //если синяя начальная и зеленая конечная
            if (_masStationsBlue.Contains(start) && _masStationsGreen.Contains(stop))
                qvery = PathTwoLine(_masOllBlue, _masOllGreen, 26, 24);
            //если синяя начальная и красная конечная
            if (_masStationsBlue.Contains(start) && _masStationsRed.Contains(stop))
                qvery = PathTwoLine(_masOllBlue, _masOllRed, 23, 27);
            //если зеленая начальная и красная конечная
            if (_masStationsGreen.Contains(start) && _masStationsRed.Contains(stop))
                qvery = PathTwoLine(_masOllGreen, _masOllRed, 21, 25);
            //если зеленая начальная и синяя конечная
            if (_masStationsGreen.Contains(start) && _masStationsBlue.Contains(stop))
                qvery = PathTwoLine(_masOllGreen, _masOllBlue, 24, 26);
            return qvery;
        }

        //Расчет пути по одной линии
        private List<Ellipse> PathLine(Ellipse[] masEllipses)
        {
            var qvery = new List<Ellipse>();
            Ellipse[] mas;
            if (int.Parse(_start.Tag.ToString()) < int.Parse(_stop.Tag.ToString()))
            {
                mas = masEllipses.OrderBy(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in mas)
                    if (int.Parse(i.Tag.ToString()) > int.Parse(_start.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) < int.Parse(_stop.Tag.ToString()))
                        qvery.Add(i);
            }
            if (int.Parse(_start.Tag.ToString()) > int.Parse(_stop.Tag.ToString()))
            {
                mas = masEllipses.OrderByDescending(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in mas)
                    if (int.Parse(i.Tag.ToString()) < int.Parse(_start.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) > int.Parse(_stop.Tag.ToString()))
                        qvery.Add(i);
            }
            return qvery;
        }

        //Расчет пути по разным линиям линии
        private List<Ellipse> PathTwoLine(Ellipse[] masStart, Ellipse[] masStop, int transitionStart, int transitionStop)
        {
            var qvery = new List<Ellipse>();
            Ellipse[] mas;
            if (int.Parse(_start.Tag.ToString()) <= transitionStart)
            {
                mas = masStart.OrderBy(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in mas)
                    if (int.Parse(i.Tag.ToString()) > int.Parse(_start.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) < transitionStart + 1)
                        qvery.Add(i);
            }
            if (int.Parse(_start.Tag.ToString()) >= transitionStart)
            {
                mas = masStart.OrderByDescending(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in mas)
                    if (int.Parse(i.Tag.ToString()) < int.Parse(_start.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) > transitionStart - 1)
                        qvery.Add(i);
            }
            if (int.Parse(_stop.Tag.ToString()) <= transitionStop)
            {
                mas = masStop.OrderByDescending(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in mas)
                    if (int.Parse(i.Tag.ToString()) > int.Parse(_stop.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) < transitionStop + 1)
                        qvery.Add(i);
            }
            if (int.Parse(_stop.Tag.ToString()) >= transitionStop)
            {
                mas = masStop.OrderBy(x => int.Parse(x.Tag.ToString())).ToArray();
                foreach (var i in mas)
                    if (int.Parse(i.Tag.ToString()) < int.Parse(_stop.Tag.ToString()) &&
                        int.Parse(i.Tag.ToString()) > transitionStop - 1)
                        qvery.Add(i);
            }
            return qvery;
        }

        // метод таймера отресовщика пути
        private void TimerOffOn(object sender, EventArgs e)
        {
            if (_count == _wey.Count)
            {
                _timer.Stop();
                _timerColor.Start();
                if (_start != null)
                    WriteWay();
                return;
            }
            while (_count < _wey.Count)
            {
                _wey[_count].Fill = Brushes.Aqua;
                _count++;
                break;
            }

        }
        //мигалка :)
        private void TimerColor(object sender, EventArgs e)
        {
            if (_flagColor)
            {
                _start.Fill = Brushes.Aquamarine;
                for (int i = _wey.Count - 1; i >= 0; i--)
                {
                    if (_wey[i].Name.Length > 3)
                        _wey[i].Fill = Brushes.Aquamarine;
                    if (_wey[i].Name.Length <= 3)
                        _wey[i].Fill = null;
                }
                _stop.Fill = Brushes.Aquamarine;
                _flagColor = false;
                return;
            }
            if (!_flagColor)
            {
                _start.Fill = Brushes.Khaki;
                for (int i = _wey.Count - 1; i >= 0; i--)
                    if (_wey[i].Name.Length > 3)
                        _wey[i].Fill = Brushes.Khaki;
                _stop.Fill = Brushes.Khaki;
                _flagColor = true;
            }
        }
        //список станций
        private void WriteWay()
        {
            Way.Content = $"Начальна станцiя :   {_start.Name}";
            foreach (var i in _wey)
                if (i.Name.Length > 3)
                    Way.Content += $"\n                                    {i.Name}";
            Way.Content += $"\n    Кiнцева станцiя :  {_stop.Name}";
        }
    }
}
