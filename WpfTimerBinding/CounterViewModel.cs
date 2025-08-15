using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace WpfTimerBinding
{
    public class CounterViewModel : INotifyPropertyChanged
    {
        private int _number;
        public int Number
        {
            get => _number;
            set
            {
                if (_number != value)
                {
                    _number = value;
                    OnPropertyChanged(nameof(Number));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private CancellationTokenSource _cts;

        public void StartCounter()
        {
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Task.Run(async () =>
            {
                for (int i = 1; i <= 1000; i++)
                {
                    if (token.IsCancellationRequested)
                        break;

                    Number = i; // UI는 Dispatcher가 알아서 업데이트
                    await Task.Delay(1000); // 0.1초마다 증가
                }
            }, token);
        }

        public void StopCounter()
        {
            _cts?.Cancel();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
