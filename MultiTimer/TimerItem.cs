using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiTimer
{
    public class TimerItem
    {
        public string Name { get; set; }
        public TimeSpan ElapsedTime { get; set; } = TimeSpan.Zero;
        public TimerState State { get; set; } = TimerState.Init;
        public Key AssignedKey { get; set; } // 단축키
        public CancellationTokenSource CancellationToken { get; set; } = new CancellationTokenSource();

        // 진행률 계산
        public double Progress => GoalTime.TotalMilliseconds > 0
            ? ElapsedTime.TotalMilliseconds / GoalTime.TotalMilliseconds
            : 0;

        // 타겟 시간 (목표 시간)
        public TimeSpan GoalTime { get; set; } = TimeSpan.FromMinutes(1);

        // 타이머 동작 로직
        public async Task RunTimer()
        {
            while (State == TimerState.Running)
            {
                await Task.Delay(100); // 0.1초 간격
                ElapsedTime = ElapsedTime.Add(TimeSpan.FromMilliseconds(100));
                if (ElapsedTime >= GoalTime)
                {
                    StopTimer();
                    break;
                }
            }
        }

        public void StartTimer()
        {
            if (State == TimerState.Init || State == TimerState.Paused)
            {
                State = TimerState.Running;
                CancellationToken = new CancellationTokenSource();
                Task.Run(() => RunTimer(), CancellationToken.Token);
            }
        }

        public void PauseTimer()
        {
            if (State == TimerState.Running)
            {
                State = TimerState.Paused;
                CancellationToken.Cancel();
            }
        }

        public void ResetTimer()
        {
            CancellationToken.Cancel();
            ElapsedTime = TimeSpan.Zero;
            State = TimerState.Init;
        }

        public void StopTimer()
        {
            State = TimerState.Paused;
            CancellationToken.Cancel();
        }
    }

    public enum TimerState
    {
        Init,
        Running,
        Paused
    }
}
