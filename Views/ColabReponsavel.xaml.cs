using AppPortariaControle.Dal;
using Microsoft.EntityFrameworkCore;
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

namespace AppPortariaControle.Views
{
    /// <summary>
    /// Interação lógica para ColabReponsavel.xam
    /// </summary>
    public partial class ColabReponsavel : Window
    {
        private MainWindow _mainWindow;
        public VisitanteDto SelectedItem { get; private set; }
        public ColabReponsavel(VisitanteDto selectedItem)
        {
            InitializeComponent();
            SelectedItem = selectedItem;
        }

        private async void Salvar_Click(object sender, RoutedEventArgs e)
        {
            Context context = new Context();

            if (SelectedItem != null)
            {

                string? cpf = SelectedItem.TipoDocumento.Equals("CPF") ? SelectedItem.Documento : null;
                string? rg = SelectedItem.TipoDocumento.Equals("RG") ? SelectedItem.Documento : null;
                string colabRespons = txtColabResponsavel.Text;
                var resultAcess = new RegistroPrestadorServico()
                {
                    NomeFunc = SelectedItem.NomeFunc,
                    RG = rg,
                    CPF = cpf,
                    NomeEmp = SelectedItem.NomeEmp,
                    CNPJ = SelectedItem.CNPJ,
                    Entrada = DateTime.Now,
                    Saida = null,
                    ColaboradorResponsavel = colabRespons,
                    ResponsavelControleEntrada = MainWindow.UsuarioLogado
                };

                context.RegistroPrestadorServicos.Add(resultAcess);

                await context.SaveChangesAsync();

                MessageBox.Show($"Registro salvo com sucesso! Horário de entrada: {resultAcess.Entrada} ", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                Hide();

            }

        }
    }
}
