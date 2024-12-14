using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace MultiTimer
{
    public class TimerViewModel : INotifyPropertyChanged
    {
        private double _progress; // 진행률을 나타내는 프로퍼티
        private DispatcherTimer _timer;

        public TimerViewModel()
        {
            _progress = 0;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1); // 1초마다 진행률 업데이트
            _timer.Tick += Timer_Tick;
        }

        public double Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        // 타이머가 진행될 때마다 호출
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Progress < 1)
            {
                Progress += 0.1; // 10%씩 진행
            }
            else
            {
                _timer.Stop(); // 타이머 완료 시 멈춤
            }
        }

        public void StartTimer()
        {
            _timer.Start();
        }

        public void PauseTimer()
        {
            _timer.Stop();
        }

        // PropertyChanged 이벤트를 구현하여 바인딩을 위한 알림
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
