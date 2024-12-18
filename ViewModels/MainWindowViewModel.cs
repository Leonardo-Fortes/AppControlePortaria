using System.Windows.Controls;
using System.Windows.Input;
namespace AppPortariaControle.ViewModels
{
    public class MainWindowViewModel
    {
        public ICommand NavigateCommand { get; }
        public ICommand VoltarCommand { get; } // Novo comando
      

        public MainWindowViewModel(Frame frame)
        {
            // Comando de navegação existente
            NavigateCommand = new RelayCommand<string>(page =>
            {
                var uri = new Uri($"ViewsUI/{page}.xaml", UriKind.Relative);
                frame.Navigate(uri);
            });

            // Comando de voltar para a página inicial
            VoltarCommand = new RelayCommand<object>(_ =>
            {
                frame.Navigate(null); // Limpa o Frame
                                      // frame.Navigate(new PaginaInicial()); // Alternativa: navega para uma página inicial específica
            });
        }

        // Classe RelayCommand existente
        public class RelayCommand<T> : ICommand
        {
            private readonly Action<T> _execute;
            private readonly Predicate<T> _canExecute;

            public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
            {
                _execute = execute;
                _canExecute = canExecute;
            }

            public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) ?? true;

            public void Execute(object parameter) => _execute((T)parameter);

            public event EventHandler CanExecuteChanged;
        }
    }
}
