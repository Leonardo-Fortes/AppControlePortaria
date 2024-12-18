using AppPortariaControle.Dal;
using AppPortariaControle.Dtos;
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

                int id_emp = SelectedItem.ID_Emp;
                int id_func = SelectedItem.ID_Func;


                string colabRespons = txtColabResponsavel.Text;
                // Criando uma instância de PrestadorServicoFunc
 
                // Criando a instância de RegistroPrestadorServico
                var resultAcess = new RegistroPrestadorServico
                {
                    ID_Emp = id_emp,
                    ID_Func = id_func,
                    Entrada = DateTime.Now,
                    Saida = null,
                    ColaboradorResponsavel = string.IsNullOrWhiteSpace(colabRespons) ? "N/A" : colabRespons.ToUpper(),
                    ResponsavelControleEntrada = MainWindow.UsuarioLogado?.ToUpper()
                };

                // Salvando no contexto
                context.RegistroPrestadorServicos.Add(resultAcess);
                await context.SaveChangesAsync();


                MessageBox.Show($"Registro salvo com sucesso! Horário de entrada: {resultAcess.Entrada} ", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                Hide();

            }

        }
    }
}
