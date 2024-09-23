using AppPortariaControle.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Shapes;

namespace AppPortariaControle.Views
{
    /// <summary>
    /// Lógica interna para AdicionarVeiculo.xaml
    /// </summary>
    public partial class AdicionarVeiculo : Window
    {
        public AdicionarVeiculo()
        {
            InitializeComponent();
        }

        private void btnBuscar_click(object sender, RoutedEventArgs e)
        {
            using (Context context = new Context())
            {
                var buscar = context.funcionarios
                  .Where(x => EF.Functions.Like(x.Nome, txtNomeFunc.Text + "%"))
                  .Select(x => new VeiculoDto
                  {
                      Motorista = x.Nome,
                      ID_Func = x.ID
                  })
                  .ToList();

                if (buscar != null && buscar.Any())
                {
                    // Atualiza o ItemsSource do DataGrid com a lista 'buscar'
                    dataGridFuncionários.ItemsSource = buscar;

                }


                else
                {
                    MessageBox.Show("Funcionário não foi encontrado", "erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new())
            {
                var selectedItem = dataGridFuncionários.SelectedItem as VeiculoDto;
                if (!txtNomeFunc.Text.IsNullOrEmpty() && !txtModelo.Text.IsNullOrEmpty() && !txtPlaca.Text.IsNullOrEmpty())
                {
                    try
                    {
                        var newVeiculo = new Veiculo
                        {
                            Placa = txtPlaca.Text,
                            Tipo = comboBoxVeiculo.Text,
                            Modelo = txtModelo.Text,
                            Motorista = selectedItem.Motorista,
                            IDFunc = selectedItem.ID_Func
                        };

                        context.Veiculos.Add(newVeiculo);

                        context.SaveChanges();

                        MessageBox.Show("Veiculo incluido com Sucesso!!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao incluir veiculo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                else
                {
                    MessageBox.Show("Nenhum campo pode ser vazio","Information",MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void dataGridFuncionários_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = dataGridFuncionários.SelectedItem as VeiculoDto;

            if (selectedItem != null)
            {
                // Joga o nome do motorista no TextBox
                txtNomeFunc.Text = selectedItem.Motorista;
                
            }
        }
    }
}
