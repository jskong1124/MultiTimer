using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace MultiTimer
{
    public class TimerModel // 이름, 키, 스탑워치, 목표시간 관리
    {
        private Stopwatch _stopwatch;

        public TimerModel(string name, TimeSpan targetTime)
        {
            Name = name;
            TargetTime = targetTime;
            _stopwatch = new Stopwatch();
        }

        public string Name { get; set; }
        public Key Key { get; set; }

        public TimeSpan ElapsedTime
        {
            get => _stopwatch.Elapsed;
        }
        public TimeSpan TargetTime { get; set; }

        public void Start()
        {
            _stopwatch.Start();
        }

        public void Pause()
        {
            _stopwatch.Stop();
        }

        public void Reset()
        {
            _stopwatch.Reset();
        }

        public void Restart()
        {
            _stopwatch.Restart();
        }
    }
}
