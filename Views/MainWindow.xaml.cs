using AppPortariaControle.Dal;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AppPortariaControle.ViewsUI;

namespace AppPortariaControle.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 

        public MainWindow()
        {
            InitializeComponent();

            // Adiciona o evento Loaded para limpar os campos de login ao carregar a janela

        }


        public static string? UsuarioLogado;

        private async void btnEntrar_Click_1(object sender, RoutedEventArgs e)
        {

            string usuario = txtUsuario.Text;
            string senha = txtSenha.Password;
            Context _context = new Context();
            var usuarioLogado = await _context.Usuarios.Where(x => x.Login == usuario && x.Senha == senha).Select(x => new
            {
                x.Nome
            }).FirstOrDefaultAsync();



            if (usuarioLogado != null)
            {
                //Home home = new Home();
                //home.Show();
                HomeVeiculos window1 = new ();
                window1.Show();
                this.Hide();
                UsuarioLogado = usuarioLogado.Nome;
                txtUsuario.Text = string.Empty;
                txtSenha.Password = string.Empty;
            }
            else
            {
                MessageBox.Show("Acesso Negado");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
         "Deseja fechar a aplicação?",
         "Confirmação",
         MessageBoxButton.YesNo,
         MessageBoxImage.Question
 );

            if (result == MessageBoxResult.Yes)
            {
                // Encerra o aplicativo
                Application.Current.Shutdown();
            }
        }
    }
}