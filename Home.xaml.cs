using System.Windows;


namespace AppPortariaControle
{
    /// <summary>
    /// Lógica interna para Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        private bool fechamentoConfirmado = false;

        public Home()
        {
            InitializeComponent();
            
        }

 

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuInterno_Click(object sender, RoutedEventArgs e)
        {
            // Evite criar várias instâncias de ControleInterno
            var existingWindow = Application.Current.Windows.OfType<ControleInterno>().FirstOrDefault();
            if (existingWindow == null)
            {
                ControleInterno controleInterno = new ControleInterno();
                controleInterno.Show();
            }
            else
            {
                existingWindow.Activate(); // Ativar a janela existente
            }
        }


        private void MenuVisitante_Click(object sender, RoutedEventArgs e)
        {
            // Evite criar várias instâncias de ControleInterno
            var existingWindow = Application.Current.Windows.OfType<ControleVisitante>().FirstOrDefault();
            if (existingWindow == null)
            {
                ControleVisitante controleVisitante = new ControleVisitante();
                controleVisitante.Show();
            }
            else
            {
                existingWindow.Activate(); // Ativar a janela existente
            }
        }


        private void Home_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!fechamentoConfirmado)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Deseja fechar a aplicação?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                );

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true; // Cancela o fechamento
                }
                else
                {
                    fechamentoConfirmado = true;
                    Application.Current.Shutdown();
                    
                }
            }
        }

    }
}
