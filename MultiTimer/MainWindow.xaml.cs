using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

// include lib

namespace MultiTimer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow
    {
        // TimerItems를 ObservableCollection으로 선언
        public ObservableCollection<TimerViewModel> TimerItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            TimerItems = new ObservableCollection<TimerViewModel>();  // 컬렉션 초기화
            DataContext = this;  // DataBinding을 위해 DataContext 설정
        }

        // 타이머 추가 버튼 클릭 시
        private void AddTimerButton_Click(object sender, RoutedEventArgs e)
        {
            // 새로운 타이머 아이템을 추가
            //var newTimer = new TimerItem(" New Timer");
            //TimerItems.Add(newTimer);
            //TimerItems.Add(new TimerModel("Timer " + (TimerItems.Count + 1)));
            TimerItems.Add(new TimerViewModel("Timer " + (TimerItems.Count + 1)));
        }
        /*
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TimerViewModel)this.DataContext;
            viewModel.Start(); // 타이머 시작
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TimerViewModel)this.DataContext;
            viewModel.Pause(); // 타이머 일시정지
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TimerViewModel)this.DataContext;
            viewModel.Reset(); // 타이머 일시정지
        }
        */
    }
}

