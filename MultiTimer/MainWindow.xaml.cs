using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace MultiTimer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        // TimerItems를 ObservableCollection으로 선언
        public ObservableCollection<TimerViewModel> TimerItems { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            TimerItems = new ObservableCollection<TimerViewModel>();  // 컬렉션 초기화
            DataContext = this;  // DataBinding을 위해 DataContext 설정
            TimerItems.Add(new TimerViewModel("Timer " + (TimerItems.Count + 1)));
        }

        // 타이머 추가 버튼 클릭 시
        private void AddTimerButton_Click(object sender, RoutedEventArgs e)
        {
            TimerItems.Add(new TimerViewModel("Timer " + (TimerItems.Count + 1)));
        }
    }
}
