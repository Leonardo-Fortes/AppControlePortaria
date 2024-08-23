using AppPortariaControle.Dal;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace AppPortariaControle
{
    /// <summary>
    /// Lógica interna para ControleInterno.xaml
    /// </summary>
    public partial class ControleInterno : Window
    {

        public ControleInterno(/*Context context*/)
        {
            InitializeComponent();
        }


        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            dataGridVeiculos.Items.Clear();
            // Obtenha o texto do TextBox
            var prefixo = TxbPlaca.Text;
            var _context = new Context();

            var placa = await _context.Veiculos
                .Where(x => x.Placa == prefixo)
                .Select(x => new { x.Placa })
                .FirstOrDefaultAsync();

            // Verifique se o resultado não é nulo e atualize o TextBox
            if (placa != null)
            {
               var veiculos = await _context.Veiculos.AsNoTracking()
               .Select(v => new VeiculoDto
               {
                   Placa = v.Placa,
                   Tipo = v.Tipo,
                   Modelo = v.Modelo,
                   Motorista = v.Motorista
               })
               .ToListAsync();

                dataGridVeiculos.ItemsSource = veiculos;

                dataGridVeiculos.ItemsSource = null;
                dataGridVeiculos.ItemsSource = veiculos;
            }
            else
            {
                // Caso não encontre resultados, limpe ou defina um valor padrão
                TxbPlaca.Text = "Nenhuma placa encontrada";
            }
        }

        private async void btnEntrada_Click(object sender, RoutedEventArgs e)
        {
            var _context = new Context();

            // Obter o item clicado
            var selectedVeiculo = (VeiculoDto)dataGridVeiculos.SelectedItem;

            var ultimoRegistro = await _context.AcessoInternos
           .Where(a => a.Placa == selectedVeiculo.Placa)
           .OrderByDescending(a => a.Entrada) // Ordena por data de entrada decrescente
           .FirstOrDefaultAsync();

            if (ultimoRegistro.Saida != null)
            {

                if (selectedVeiculo != null)
                {
                    try
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


                                MessageBox.Show("Alteração realizada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                                dataGridVeiculos.ItemsSource = null;
                                TxbPlaca.Text = "";
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($" (CDGEXP:001) - Erro ao realizar a alteração: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("(CDGEXP:002) - Digite uma placa, antes de dar entrada/saída ", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }



                }
            }
            else
            {
                MessageBox.Show("Este veículo ainda não saiu.", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
