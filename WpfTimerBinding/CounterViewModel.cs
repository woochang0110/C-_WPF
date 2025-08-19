using System;
using System.ComponentModel;    // INotifyPropertyChanged 인터페이스 사용
using System.Threading;
using System.Threading.Tasks;   // Task 비동기 실행용

namespace WpfTimerBinding
{
    // ViewModel: UI와 데이터 연결 담당
    public class CounterViewModel : INotifyPropertyChanged
    {
        private int _number; // 실제 데이터 저장용 필드
        public int Number    // 바인딩 대상 속성
        {
            get => _number;  // 값 읽기
            set
            {
                if (_number != value)        // 값이 바뀌었을 때만 실행
                {
                    _number = value;         // 새로운 값 저장
                    OnPropertyChanged(nameof(Number)); // 바인딩된 UI에 "값 바뀜" 알림
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
            _cts = new CancellationTokenSource(); // 취소 신호를 보낼 수 있는 컨트롤러 생성
            var token = _cts.Token;               // 실제 신호를 전달받는 토큰

            // 별도의 스레드(Task)에서 카운터 실행
            Task.Run(async () =>
            {
                for (int i = 1; i <= 1000; i++)   // 1부터 1000까지 반복
                {
                    if (token.IsCancellationRequested) // 취소 신호 들어왔는지 확인
                        break;                          // 들어오면 반복 종료

                    Number = i;           // 값 갱신 → UI에 자동 반영됨
                    await Task.Delay(1000); // 1초 쉬고 다음 숫자 증가
                }
            }, token); // token 넘겨서 Task 자체도 취소 가능하게 함
        }

        // 카운터 중단
        public void StopCounter()
        {
            _cts?.Cancel(); // 취소 신호 보내기 (?.는 _cts가 null일 경우 무시)
        }

        // 속성 변경 이벤트 발생시켜주는 함수
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            // PropertyChanged 이벤트가 null이 아니면 실행 → UI에 알림
        }
    }
}
