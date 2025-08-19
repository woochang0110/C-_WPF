using System;
using System.Collections.Generic;
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

/*
 * 1. MainWindow가 뜨면서 CounterViewModel을 DataContext로 연결.
 * 2. TextBlock은 ViewModel.Number와 바인딩되어 있음.
 * 3. Start 버튼 → StartCounter() 실행 → Task.Run에서 1~1000까지 값 증가.
 * 4. Number가 바뀔 때마다 OnPropertyChanged → UI(TextBlock) 자동 업데이트.
 * 5. Stop 버튼 → Cancel() → 카운터 중단.
 */

namespace WpfTimerBinding
{
    public partial class MainWindow : Window
    {
        private CounterViewModel _viewModel; // UI와 연결될 ViewModel 인스턴스

        public MainWindow()
        {
            InitializeComponent();     // XAML에서 정의한 UI 요소 초기화
            _viewModel = new CounterViewModel(); // ViewModel 생성
            DataContext = _viewModel;           // 윈도우 전체 DataContext를 ViewModel로 설정
                                                // → XAML에서 {Binding Number} 가 ViewModel.Number랑 연결됨
        }

        // Start 버튼 클릭 시 호출
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StartCounter(); // ViewModel의 카운터 시작 함수 실행
        }

        // Stop 버튼 클릭 시 호출
        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StopCounter(); // ViewModel의 카운터 중단 함수 실행
        }
    }
}
