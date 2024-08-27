using AppPortariaControle.Dal;
using Microsoft.EntityFrameworkCore;
using System.Windows;
namespace AppPortariaControle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow() 
        {
            InitializeComponent();
        }
        public static string? UsuarioLogado;

        private async void btnEntrar_Click_1(object sender, RoutedEventArgs e)
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

    
    }
}