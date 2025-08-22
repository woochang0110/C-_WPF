using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfTimerBinding
{
    /*
     * 실행 흐름
     * 1. MainWindow 생성 시 CounterViewModel을 DataContext로 연결.
     * 2. TextBlock은 ViewModel.Number와 바인딩되어, 값이 변하면 자동 업데이트됨.
     * 3. TextBox는 ViewModel.InputNumber와 바인딩되어, Enter 눌러야 값이 적용됨.
     * 4. Enter 누르면 InputNumber → Number로 반영됨.
     * 5. Start 버튼 → StartCounter() 실행 → Number부터 1000까지 1초 간격 증가.
     * 6. Stop 버튼 → Cancel() → 카운터 중단.
     */

    public partial class MainWindow : Window
    {
        private CounterViewModel _viewModel; // UI와 연결될 ViewModel 인스턴스

        public MainWindow()
        {
            InitializeComponent();               // XAML에서 정의한 UI 요소 초기화
            _viewModel = new CounterViewModel(); // ViewModel 생성
            DataContext = _viewModel;           // 윈도우 전체 DataContext를 ViewModel로 설정
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

        // TextBox에서 Enter 키 누를 때 호출
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                {
                    // 바인딩 강제로 업데이트 → InputNumber에 반영됨
                    var binding = textBox.GetBindingExpression(TextBox.TextProperty);
                    binding?.UpdateSource();

                    // ViewModel.Number에 입력값 적용
                    _viewModel.ApplyInput();
                }
            }
        }
    }
}
