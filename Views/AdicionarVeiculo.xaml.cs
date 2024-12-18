using AppPortariaControle.Dal;
using AppPortariaControle.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AppPortariaControle.Views
{
    public partial class AdicionarVeiculo : Window
    {

        private VeiculoDto _item;
        private bool update = false;


        public AdicionarVeiculo(VeiculoDto item)
        {
            InitializeComponent();
            _item = item;

            if (_item != null)
            {
                txtModelo.Text = _item.Modelo;
                txtNomeFunc.Text = _item.Motorista;
                txtPlaca.Text = _item.Placa;
                comboBoxVeiculo.Text = _item.Tipo;
                update = true;
            }
        }



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
                    dataGridFuncionários.ItemsSource = buscar;
                }
                else
                {
                    MessageBox.Show("Funcionário não foi encontrado", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new())
            {
                if (update && _item != null)
                {
                    var veiculoExistente = context.Veiculos.FirstOrDefault(v => v.Motorista == _item.Motorista && v.Placa == _item.Placa);
                    if (veiculoExistente != null)
                    {
                        if (!string.IsNullOrEmpty(txtNomeFunc.Text) && !string.IsNullOrEmpty(txtModelo.Text) &&
                            !string.IsNullOrEmpty(txtPlaca.Text) && !string.IsNullOrEmpty(comboBoxVeiculo.Text))
                        {
                            veiculoExistente.Modelo = txtModelo.Text.ToUpper();
                            veiculoExistente.Placa = txtPlaca.Text.ToUpper();
                            veiculoExistente.Motorista = txtNomeFunc.Text.ToUpper();
                            veiculoExistente.Tipo = comboBoxVeiculo.Text.ToUpper();
                            veiculoExistente.UserAdd = MainWindow.UsuarioLogado.ToUpper();

                            try
                            {
                                context.SaveChanges();

                                // Dispara o evento com o veículo atualizado


                                MessageBox.Show("Atualizado com sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Close();
                            }
                            catch
                            {
                                MessageBox.Show("Erro ao atualizar", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nenhum campo pode ser vazio", "Informação", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    var selectedItem = dataGridFuncionários.SelectedItem as VeiculoDto;
                    if (!string.IsNullOrEmpty(txtNomeFunc.Text) && !string.IsNullOrEmpty(txtModelo.Text) &&
                        !string.IsNullOrEmpty(txtPlaca.Text) && selectedItem != null)
                    {
                        try
                        {
                            if (context.Veiculos.Any(v => v.Placa.ToUpper().Trim() == txtPlaca.Text.ToUpper().Trim()))
                            {
                                MessageBox.Show("Este Veículo já foi cadastrado","Erro",MessageBoxButton.OK,MessageBoxImage.Error);
                                return;
                            }

                            var newVeiculo = new Veiculo
                            {
                                Placa = txtPlaca.Text?.ToUpper(),
                                Tipo = comboBoxVeiculo.Text.ToUpper(),
                                Modelo = txtModelo.Text?.ToUpper(),
                                Motorista = selectedItem.Motorista?.ToUpper(),
                                IDFunc = selectedItem.ID_Func,
                                UserAdd = MainWindow.UsuarioLogado.ToUpper()
                            };

                            context.Veiculos.Add(newVeiculo);
                            context.SaveChanges();

                            // Dispara o evento com o veículo adicionado


                            MessageBox.Show("Veículo incluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        catch
                        {
                            MessageBox.Show("Erro ao incluir veículo", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Nenhum campo pode ser vazio", "Informação", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void dataGridFuncionários_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = dataGridFuncionários.SelectedItem as VeiculoDto;
            if (selectedItem != null)
            {
                txtNomeFunc.Text = selectedItem.Motorista.ToUpper();
            }
        }

    
    }
}
