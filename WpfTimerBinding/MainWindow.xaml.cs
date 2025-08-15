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

namespace WpfTimerBinding
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private CounterViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new CounterViewModel();
            DataContext = _viewModel;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StartCounter();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StopCounter();
        }
    }
}
