using System;
using System.ComponentModel;    // INotifyPropertyChanged 인터페이스 사용
using System.Threading;
using System.Threading.Tasks;   // Task 비동기 실행용

namespace WpfTimerBinding
{
    // ViewModel: UI와 데이터 연결 담당
    public class CounterViewModel : INotifyPropertyChanged
    {
        private int _number;       // 실제 카운트 값 저장용
        private int _inputNumber;  // TextBox에서 입력받는 값 저장용

        // 카운트 값 (TextBlock에 표시됨, 타이머에 의해 갱신)
        public int Number
        {
            get => _number;
            set
            {
                if (_number != value)              // 값이 바뀌었을 때만 실행
                {
                    _number = value;               // 새로운 값 저장
                    OnPropertyChanged(nameof(Number)); // UI(TextBlock)에 알림
                }
            }
        }

        // 입력 값 (TextBox와 바인딩됨, Enter 시 Number에 반영)
        public int InputNumber
        {
            get => _inputNumber;
            set
            {
                if (_inputNumber != value)
                {
                    _inputNumber = value;             // 입력값 저장
                    OnPropertyChanged(nameof(InputNumber)); // UI(TextBox)에 알림
                }
            }
        }

        // 속성 변경 시 발생하는 이벤트 (WPF 바인딩 엔진이 감지)
        public event PropertyChangedEventHandler PropertyChanged;

        // 작업을 취소하기 위한 토큰 (Stop 버튼에서 사용)
        private CancellationTokenSource _cts;

        // 카운터 시작
        public void StartCounter()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Run(async () =>
            {
                while (!token.IsCancellationRequested)
                {
                    await Task.Delay(1000);

                    // 입력으로 바뀐 Number 값에서 그대로 +1 증가
                    Number++;
                }
            }, token);
        }


        // 카운터 중단
        public void StopCounter()
        {
            _cts?.Cancel(); // 취소 신호 보내기 (?.는 _cts가 null일 경우 무시)
        }

        // TextBox 입력값을 Number로 반영
        public void ApplyInput()
        {
            Number = InputNumber;
        }

        // 속성 변경 이벤트 발생시켜주는 함수
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // PropertyChanged 이벤트가 null이 아니면 실행 → UI에 알림
        }
    }
}
