using AppPortariaControle.Dal;
using Microsoft.Identity.Client;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppPortariaControle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string? UsuarioLogado;
        public MainWindow()
        {
            InitializeComponent();

        }


        private void btnEntrar_Click_1(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Password;
            Context _context = new Context();
            var usuarioLogado = _context.Usuarios.Where(x => x.Login == usuario && x.Senha == senha).Select(x => new
            {
                x.Nome
            }).FirstOrDefault();

            

            if (usuarioLogado != null)
            {
                Home home = new Home();
                home.Show();
                this.Hide();
                UsuarioLogado = usuarioLogado.Nome;
            }
            else
            {
                MessageBox.Show("Acesso Negado");
            }
        }

        private void txtUsuario_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}