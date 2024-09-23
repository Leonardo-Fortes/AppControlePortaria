using AppPortariaControle.Dal;
using AppPortariaControle.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.Eventing.Reader;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AppPortariaControle.Views
{
    /// <summary>
    /// Lógica interna para ControleInterno.xaml
    /// </summary>
    public partial class ControleInterno : Window
    {

        public ControleInterno()
        {
            InitializeComponent();
        }


        private async void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            // Obtenha o texto do TextBox
            var prefixo = TxbPlaca.Text;
            dataGridVeiculos.ItemsSource = null;
            dataGridVeiculos.Items.Clear();
            if (!prefixo.IsNullOrEmpty())
            {
                using (var _context = new Context())
                {

                    var placa = await _context.Veiculos
                        .Where(x => EF.Functions.Like(x.Placa, prefixo + "%"))
                        .Select(x => new { x.Placa })
                        .FirstOrDefaultAsync();


                    // Verifique se o resultado não é nulo e atualize o TextBox
                    if (placa != null)
                    {

                        var veiculos = await _context.Veiculos.AsNoTracking().Where(x => x.Placa == placa.Placa)
                        .Select(v => new VeiculoDto { Placa = v.Placa, Tipo = v.Tipo, Modelo = v.Modelo, Motorista = v.Motorista })
                        .ToListAsync();

                        dataGridVeiculos.ItemsSource = veiculos;
                    }
                    else
                    {
                        // Caso não encontre resultados, limpe ou defina um valor padrão
                        MessageBox.Show("Nenhuma veiculo encontrado com essa placa", "ERRO", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Digite a inicial da placa para fazer a busca","informação",MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void BtnEntrada_Click(object sender, RoutedEventArgs e)
        {
            using (var _context = new Context())
            {

                // Obter o item clicado
                var selectedVeiculo = (VeiculoDto)dataGridVeiculos.SelectedItem;
                if (selectedVeiculo != null)
                {
                    var ultimoRegistro = await _context.AcessoInternos
                   .Where(a => a.Placa == selectedVeiculo.Placa)
                   .OrderByDescending(a => a.Entrada) // Ordena por data de entrada decrescente
                   .FirstOrDefaultAsync();

                    if (ultimoRegistro?.Saida != null || ultimoRegistro == null)
                    {
                        // Exibir a mensagem de confirmação
                        MessageBoxResult result = MessageBox.Show(
                            $"Deseja realmente realizar a Entrada do veículo com placa: {selectedVeiculo.Placa}?",
                            "Confirmação",
                            MessageBoxButton.YesNo,
                            MessageBoxImage.Question
                        );

                        // Verificar a resposta do usuário
                        if (result == MessageBoxResult.Yes)
                        {
                            try
                            {

                                var resultAcess = new AcessoInterno()
                                {
                                    Placa = selectedVeiculo.Placa,
                                    Tipo = selectedVeiculo.Tipo,
                                    Modelo = selectedVeiculo.Modelo,
                                    Motorista = selectedVeiculo.Motorista,
                                    Mes = DateTime.Now.ToString("MM/yyyy"),
                                    Entrada = DateTime.Now,
                                    Saida = null,
                                    ResponsavelControleEntrada = MainWindow.UsuarioLogado,
                                    ResponsavelControleSaida = null
                                };

                                _context.AcessoInternos.Add(resultAcess);


                                // Salvar as alterações
                                await _context.SaveChangesAsync();


                                MessageBox.Show($"Registro salvo com sucesso! Horário de entrada: {resultAcess.Entrada} ", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                                dataGridVeiculos.ItemsSource = null;
                                TxbPlaca.Text = "";
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Erro ao realizar a alteração: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Este veículo ainda não saiu.", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                    }


                }
                else
                {
                    MessageBox.Show(
                         "Por favor, insira a placa do veículo para prosseguir com o registro.",
                         "Aviso",
                         MessageBoxButton.OK,
                         MessageBoxImage.Warning
                                   );
                }
            }

        }

        private async void BtnSaida_Click(object sender, RoutedEventArgs e)
        {
            using (var _context = new Context())
            {

                var selectedVeiculo = (VeiculoDto)dataGridVeiculos.SelectedItem;
                if (selectedVeiculo != null)
                {
                    var ultimoRegistro = await _context.AcessoInternos
                     .Where(a => a.Placa == selectedVeiculo.Placa)
                     .OrderByDescending(a => a.Entrada) // Ordena por data de entrada decrescente
                     .FirstOrDefaultAsync();
                    if (ultimoRegistro != null)
                    {

                        if (ultimoRegistro.Saida == null)
                        {
                            MessageBoxResult result = MessageBox.Show(
                                       $"Deseja realmente realizar a Saída do veículo com placa: {selectedVeiculo.Placa}?",
                                       "Confirmação",
                                       MessageBoxButton.YesNo,
                                       MessageBoxImage.Question);
                            if (result == MessageBoxResult.Yes)
                            {

                                try
                                {
                                    ultimoRegistro.Saida = DateTime.Now;
                                    ultimoRegistro.ResponsavelControleSaida = MainWindow.UsuarioLogado;

                                    _context.AcessoInternos.Update(ultimoRegistro);

                                    await _context.SaveChangesAsync();

                                    MessageBox.Show($"Registro salvo com sucesso! Horário de saída: {ultimoRegistro.Saida} ", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                                    dataGridVeiculos.ItemsSource = null;
                                    TxbPlaca.Text = "";
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Selecione um registro para dar Entrada/Saída", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                                };
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não foi registrado entrada deste veiculo", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não foi registrado entrada deste veiculo", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show(
                    "Por favor, insira a placa do veículo para prosseguir com o registro.",
                    "Aviso",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning
    );
                }
            }

        }

        private void AdicionarVeiculo_Click(object sender, RoutedEventArgs e)
        {
            AdicionarVeiculo adicionar = new AdicionarVeiculo();
            adicionar.Show();
        }
    }
}
