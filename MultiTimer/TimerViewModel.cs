using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Linq;

namespace MultiTimer
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        private DispatcherTimer _distpatcherTimer;
        private TimerModel _timerModel;
        private bool _isRunning = false;
        private bool _isPaused = false;
        private bool _isReset = true;
        private double _progress = 0.0;

        // *** 생성자 오버로딩
        public TimerViewModel()
            : this("이름 없는 타이머", TimeSpan.FromMinutes(1)) { }

        public TimerViewModel(string name)
            : this(name, TimeSpan.FromMinutes(1)) { }

        public TimerViewModel(string name, TimeSpan targetTime)
        {
            Console.WriteLine(name);
            Console.WriteLine(targetTime.ToString());
            _timerModel = new TimerModel(name, targetTime);
            StartCommand = new RelayCommand(Start);
            PauseCommand = new RelayCommand(Pause);
            ResetCommand = new RelayCommand(Reset);
            _distpatcherTimer = new DispatcherTimer();
            _distpatcherTimer.Interval = TimeSpan.FromMilliseconds(10); // 1초마다 진행률 업데이트
            _distpatcherTimer.Tick += DistpatcherTimer_Tick;

            _hotkeyName = "HKNAME_" + Guid.NewGuid();
            Console.WriteLine("hk ::: " + _hotkeyName);            
        }
        public ICommand StartCommand { get; }
        public ICommand PauseCommand { get; }
        public ICommand ResetCommand { get; }

        public string Name
        {
            get => _timerModel.Name;
            set
            {
                if (_timerModel.Name != value)
                {
                    _timerModel.Name = value;
                    OnPropertyChanged();  // 속성 변경 알리기
                }
            }
        }

        public string RemainingTime
        {
            get => (_timerModel.ElapsedTime - _timerModel.TargetTime).ToString(@"hh\:mm\:ss");
        }

        public string TargetTime
        {
            get => _timerModel.TargetTime.ToString(@"hh\:mm\:ss");
            set
            {
                if (TimeSpan.TryParse(value, out TimeSpan timeSpan))
                {
                    if (_timerModel.TargetTime != timeSpan)
                    {
                        _timerModel.TargetTime = timeSpan;
                        OnPropertyChanged();
                    }
                }
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            private set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;

                    if (_isRunning)
                    {
                        IsPaused = false;
                        IsReset = false;
                    }
                }
            }
        }

        public bool IsPaused
        {
            get => _isPaused;
            private set
            {
                if (_isPaused != value)
                {
                    _isPaused = value;

                    if (_isPaused)
                    {
                        IsRunning = false;
                        IsReset = false;
                    }
                }
            }
        }

        public bool IsReset
        {
            get => _isReset;
            private set
            {
                if (_isReset != value)
                {
                    _isReset = value;

                    if (_isReset)
                    {
                        IsRunning = false;
                        IsPaused = false;
                    }
                }
            }
        }

        public void Start()
        {
            if (!IsRunning)
            {
                IsRunning = true;
                _timerModel.Start();
            }
            else
            {
                _timerModel.Restart();
            }
            _distpatcherTimer.Start();
        }

        public void Pause()
        {
            if (!IsPaused)
            {
                IsPaused = true;
                _timerModel.Pause();
            }
            _distpatcherTimer.Stop();
        }

        public void Reset()
        {
            Console.WriteLine("reset 누름");
            if (!IsReset)
            {
                Console.WriteLine("reset 분기");
                IsReset = true;
                _timerModel.Reset();
                Progress = 0.0;
                OnPropertyChanged(nameof(RemainingTime));
                OnPropertyChanged(nameof(Progress));
            }
            _distpatcherTimer.Stop();
        }

        public Double Progress
        {
            get => _progress;
            set
            {
                if (_progress != value)
                {
                    _progress = Math.Min(value, 100.0);
                    OnPropertyChanged();  // 속성 변경 알리기
                }
            }
        }

        // 타이머가 진행될 때마다 호출
        private void DistpatcherTimer_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine(Name + " ::: " +_timerModel.ElapsedTime.TotalMilliseconds.ToString() + " ??? " + Guid.NewGuid());
            Console.WriteLine(Name + " ::: " + Progress.ToString());
            if (_timerModel.TargetTime > TimeSpan.Zero && Progress < 100.0)
            {
                Progress = (_timerModel.ElapsedTime.TotalMilliseconds * 100.0) / _timerModel.TargetTime.TotalMilliseconds;
                OnPropertyChanged(nameof(RemainingTime));
                OnPropertyChanged(nameof(Progress));
            }
            else
            {
                Reset();
            }
        }

        // PropertyChanged 이벤트를 구현하여 바인딩을 위한 알림
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private readonly string _hotkeyName;

        private KeyValuePair<Key, string> _selectedKey;
        public KeyValuePair<Key, string> SelectedKey
        {
            get => _selectedKey;
            set
            {
                if (_selectedKey.Equals(value)) return;
                _selectedKey = value;
                OnPropertyChanged();
                Console.WriteLine(_selectedKey.Key);
                Console.WriteLine(_selectedKey.Value);
            }
        }

        public IEnumerable<KeyValuePair<Key, string>> AvailableKeys => KeyList.Get104Keys();
    }
}
