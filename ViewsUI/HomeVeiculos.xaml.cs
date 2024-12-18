
using System.Windows.Input;
using System.Collections.ObjectModel;
using AppPortariaControle.ViewModels;
using AppPortariaControle.Dal;
using AppPortariaControle.Dtos;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using OfficeOpenXml;
using System.IO;
using AppPortariaControle.Views;


namespace AppPortariaControle.ViewsUI
{
    /// <summary>
    /// Lógica interna para Window1.xaml
    /// </summary>
    public partial class HomeVeiculos : Window
    {
      

        public HomeVeiculos()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel(MainFrame);
          
        }

        private void AdicionarVeiculo_Click(object sender, RoutedEventArgs e)
        {
            var adicionarVeiculo = new AdicionarVeiculo();

            adicionarVeiculo.Show();
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Impede que o evento de duplo clique maximize a janela
            e.Handled = true;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }

        }

        private bool IsMaximized = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximized = false;
                }
                else
                {

                    this.WindowState = WindowState.Maximized;
                    IsMaximized = true;
                }
            }


        }
 
        private void Sair_Click(object sender, RoutedEventArgs e)
        {
            // Exibe a mensagem de confirmação
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


        private async void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            // Obtenha o texto do TextBox
            var prefixo = TxbPlaca.Text;
            dataGridVeiculos.ItemsSource = null;
            dataGridVeiculos.Items.Clear();
            if (!prefixo.IsNullOrEmpty())
            {
                using Context _context = new();
                try
                {
                    // Verifica se o prefixo é válido
                    if (string.IsNullOrWhiteSpace(prefixo))
                    {
                        throw new ArgumentException("O prefixo fornecido é inválido.");
                    }

                    // Busca o veículo com a placa que começa com o prefixo
                    var tem = await _context.Veiculos
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Placa.StartsWith(prefixo.Trim()));

                    // Verifica se o veículo foi encontrado
                    if (tem == null)
                    {
                        MessageBox.Show("Veículo não foi encontrado", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }

                    // Busca o registro de entrada
                    var registroEntrada = await _context.AcessoInternos
                        .Include(y => y.Veiculo).AsNoTracking()
                        .FirstOrDefaultAsync(y => y.Veiculo.Placa == tem.Placa && y.Entrada != null);

                    // Define os resultados de acordo com a presença de registro de entrada
                    if (registroEntrada == null)
                    {
                        var result = await _context.Veiculos
                            .Where(x => x.Placa == tem.Placa) // Busca apenas o veículo encontrado
                            .Select(x => new VeiculoDto
                            {
                                ID = x.ID,
                                Placa = x.Placa,
                                Tipo = x.Tipo,
                                Modelo = x.Modelo,
                                Motorista = x.Motorista
                            })
                            .FirstOrDefaultAsync();

                        dataGridVeiculos.ItemsSource = new List<VeiculoDto?> { result };
                    }
                    else
                    {
                        var result = await _context.AcessoInternos
                            .OrderByDescending(x => x.Entrada)
                            .Where(x => x.Veiculo.Placa == tem.Placa) // Filtra pelo veículo encontrado
                            .Include(x => x.Veiculo)
                            .Select(x => new VeiculoDto
                            {
                                ID = x.Veiculo.ID,
                                Placa = x.Veiculo.Placa,
                                Tipo = x.Veiculo.Tipo,
                                Modelo = x.Veiculo.Modelo,
                                Motorista = x.Veiculo.Motorista,
                                Entrada = x.Entrada,
                                Saida = x.Saida,
                                Status = x.Status
                            })
                            .FirstOrDefaultAsync();

                        dataGridVeiculos.ItemsSource = new List<VeiculoDto?> { result };
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro: {ex.Message}", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                }



            }
        }

        private async void BtnEntrada_Click(object sender, RoutedEventArgs e)
        {
            using Context _context = new();

            // Obter o item clicado
            var selectedVeiculo = (VeiculoDto)dataGridVeiculos.SelectedItem;
            if (selectedVeiculo != null)
            {
                var ultimoRegistro = await _context.AcessoInternos.AsNoTracking().Include(a => a.Veiculo)
               .Where(a => a.Veiculo.Placa == selectedVeiculo.Placa)
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
                                Id_Veiculos = selectedVeiculo.ID,
                                Entrada = DateTime.Now,
                                Saida = null,
                                Status = Enums.EStatus.Entrada,
                                ResponsavelControleEntrada = MainWindow.UsuarioLogado?.ToUpper(),
                                ResponsavelControleSaida = null
                            };

                            _context.AcessoInternos.Add(resultAcess);


                            // Salvar as alterações
                            await _context.SaveChangesAsync();


                            MessageBox.Show($"Registro salvo com sucesso! Horário de entrada: {resultAcess.Entrada} ", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                            dataGridVeiculos.ItemsSource = null;
                            dataGridVeiculos.Items.Clear();
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

        private async void BtnSaida_Click(object sender, RoutedEventArgs e)
        {
            using Context _context = new();

            var selectedVeiculo = (VeiculoDto)dataGridVeiculos.SelectedItem;
            if (selectedVeiculo != null)
            {
                var ultimoRegistro = await _context.AcessoInternos.Include(x => x.Veiculo)
                 .Where(a => a.Veiculo.Placa == selectedVeiculo.Placa)
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
                                ultimoRegistro.ResponsavelControleSaida = MainWindow.UsuarioLogado?.ToUpper();
                                ultimoRegistro.Status = Enums.EStatus.Saida;

                                _context.AcessoInternos.Update(ultimoRegistro);

                                await _context.SaveChangesAsync();

                                MessageBox.Show($"Registro salvo com sucesso! Horário de saída: {ultimoRegistro.Saida} ", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);

                                dataGridVeiculos.ItemsSource = null;
                                TxbPlaca.Text = "";
                            }
                            catch (Exception)
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

       


        private async void VeiculosCadastrados_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Context context = new())
                {
                    var acessoInternos = await context.AcessoInternos
                        .Include(x => x.Veiculo)
                        .ToListAsync(); // Carrega os dados para memória antes de realizar operações não suportadas pelo servidor.

                    var result = acessoInternos.Where(x => x.Saida != null)
                        .GroupBy(x => x.Veiculo.ID)
                        .Select(g => g.OrderByDescending(x => x.Entrada)

                                      .FirstOrDefault())
                        .Select(x => new VeiculoDto
                        {
                            ID = x.Veiculo.ID,
                            Placa = x.Veiculo.Placa,
                            Modelo = x.Veiculo.Modelo,
                            Tipo = x.Veiculo.Tipo,
                            Motorista = x.Veiculo.Motorista,
                            Entrada = x.Entrada,
                            Saida = x.Saida,
                            Status = x.Status
                        })
                        .ToList();

                    if (result.Any())
                    {
                        // Atualiza o DataGrid com os resultados
                        dataGridVeiculos.ItemsSource = result;
                    }
                    else
                    {
                        // Exibe mensagem informando que nenhum dado foi encontrado
                        MessageBox.Show("Nenhum dado encontrado", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Trata exceções e exibe uma mensagem de erro
                MessageBox.Show($"Erro ao carregar os dados: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button?.Tag is VeiculoDto item)
            {
                MessageBoxResult res = MessageBox.Show($"Deseja excluir o veiculo com a placa {item.Placa}?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {

                    using Context context = new();
                    // Encontra o veículo no banco de dados usando uma condição `Where`
                    var result = context.Veiculos.FirstOrDefault(x => x.Placa == item.Placa);

                    if (result != null)
                    {
                        // Remove o item encontrado do banco de dados
                        context.Veiculos.Remove(result);

                        // Salva as alterações no banco de dados
                        context.SaveChanges();

                        MessageBox.Show("Veiculo excluido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        LimparDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Falha ao deletar veiculo", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                // Remove o item da coleção local para atualizar o DataGrid

            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button?.Tag is not VeiculoDto item)
                return; // Garante que item não é nulo

            AdicionarVeiculo adicionarVeiculo = new(item);
            adicionarVeiculo.Show();
        }


        public void LimparDataGrid()
        {
            dataGridVeiculos.ItemsSource = null;
            dataGridVeiculos.Items.Clear();
        }

        private async void VeiculosSaida_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (Context context = new())
                {
                    var acessoInternos = await context.AcessoInternos
                        .Include(x => x.Veiculo)
                        .ToListAsync(); // Carrega os dados para memória antes de realizar operações não suportadas pelo servidor.

                    var result = acessoInternos.Where(x => x.Saida == null)
                        .GroupBy(x => x.Veiculo.ID)
                        .Select(g => g.OrderByDescending(x => x.Saida)

                                      .FirstOrDefault())
                        .Select(x => new VeiculoDto
                        {
                            ID = x.Veiculo.ID,
                            Placa = x.Veiculo.Placa,
                            Modelo = x.Veiculo.Modelo,
                            Tipo = x.Veiculo.Tipo,
                            Motorista = x.Veiculo.Motorista,
                            Entrada = x.Entrada,
                            Saida = x.Saida,
                            Status = x.Status
                        })
                        .ToList();

                    if (result.Any())
                    {
                        // Atualiza o DataGrid com os resultados
                        dataGridVeiculos.ItemsSource = result;
                    }
                    else
                    {
                        // Exibe mensagem informando que nenhum dado foi encontrado
                        MessageBox.Show("Nenhum dado encontrado", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                // Trata exceções e exibe uma mensagem de erro
                MessageBox.Show($"Erro ao carregar os dados: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Exportações Excel

        private void Veiculos_Click(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (Context context = new())
            {
                try
                {
                    var veiculos = context.Veiculos.ToList();

                    // Criar um novo pacote Excel
                    using (var package = new ExcelPackage())
                    {
                        // Adicionar uma nova planilha
                        var worksheet = package.Workbook.Worksheets.Add("Veículos");

                        // Adicionar cabeçalhos das colunas
                        worksheet.Cells[1, 1].Value = "Placa";
                        worksheet.Cells[1, 2].Value = "Modelo";
                        worksheet.Cells[1, 3].Value = "Tipo";
                        worksheet.Cells[1, 4].Value = "Motorista";

                        // Adicionar os dados na planilha
                        int row = 2; // Começa na linha 2 (linha 1 é para os cabeçalhos)
                        foreach (var veiculo in veiculos)
                        {
                            worksheet.Cells[row, 1].Value = veiculo.Placa;
                            worksheet.Cells[row, 2].Value = veiculo.Modelo;
                            worksheet.Cells[row, 3].Value = veiculo.Tipo;
                            worksheet.Cells[row, 4].Value = veiculo.Motorista;
                            row++;
                        }

                        // Ajustar o tamanho das colunas
                        worksheet.Cells.AutoFitColumns();

                        // Salvar o arquivo Excel
                        var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                        {
                            FileName = "Veiculos.xlsx",
                            Filter = "Excel Workbook (*.xlsx)|*.xlsx"
                        };

                        if (saveFileDialog.ShowDialog() == true)
                        {
                            var fileInfo = new FileInfo(saveFileDialog.FileName);
                            package.SaveAsAsync(fileInfo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Exibir uma mensagem de erro
                    MessageBox.Show($"Ocorreu um erro ao exportar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void RegistroVeiculo_Click(object sender, RoutedEventArgs e)
        {

        }




        private void ExportExcel_Click(object sender, EventArgs e)
        {
            bool status = false;
            DataExportExcel dataExportExcel = new DataExportExcel(status);
            dataExportExcel.Show();
        }

        private void Minimizar_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
