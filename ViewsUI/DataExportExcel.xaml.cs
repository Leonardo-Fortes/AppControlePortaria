using AppPortariaControle.Dal;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AppPortariaControle.ViewsUI
{
    /// <summary>
    /// Lógica interna para DataExportExcel.xaml
    /// </summary>
    public partial class DataExportExcel : Window
    {
        private bool _status;
        public DataExportExcel(bool status)
        {
            InitializeComponent();
            _status = status;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (StartDatePicker.SelectedDate == null || EndDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Por favor, selecione ambas as datas.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Atribuir os valores para variáveis DateTime
            DateTime dataInicio = StartDatePicker.SelectedDate.Value;
            DateTime dataFim = EndDatePicker.SelectedDate.Value;
            dataFim = dataFim.Date.AddDays(1).AddTicks(-1);


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (Context context = new())
            {
                if (_status is true)
                {
                    try
                    {
                        var funcs = context.RegistroPrestadorServicos.Include(x => x.PrestadorServicoFunc).ThenInclude(y => y.PrestadorServicoEmp).ToList();

                        // Criar um novo pacote Excel
                        using (var package = new ExcelPackage())
                        {
                            // Adicionar uma nova planilha
                            var worksheet = package.Workbook.Worksheets.Add("Registro");

                            // Adicionar cabeçalhos das colunas
                            worksheet.Cells[1, 1].Value = "Nome";
                            worksheet.Cells[1, 2].Value = "CPF";
                            worksheet.Cells[1, 3].Value = "RG";
                            worksheet.Cells[1, 4].Value = "Empresa";
                            worksheet.Cells[1, 5].Value = "CNPJ";
                            worksheet.Cells[1, 6].Value = "Entrada";
                            worksheet.Cells[1, 7].Value = "Saída";
                            worksheet.Cells[1, 8].Value = "Acompanhante responsável";
                            worksheet.Cells[1, 9].Value = "Responsável pelo registro de entrada";
                            worksheet.Cells[1, 10].Value = "Responsável pelo registro de saída";





                            // Adicionar os dados na planilha
                            int row = 2; // Começa na linha 2 (linha 1 é para os cabeçalhos)
                            foreach (var func in funcs)
                            {
                                worksheet.Cells[row, 1].Value = func.PrestadorServicoFunc.NomeFunc;
                                worksheet.Cells[row, 2].Value = func.PrestadorServicoFunc.CPF;
                                worksheet.Cells[row, 3].Value = func.PrestadorServicoFunc.RG;
                                worksheet.Cells[row, 4].Value = func.PrestadorServicoEmp.NomeEmp;
                                worksheet.Cells[row, 5].Value = func.PrestadorServicoEmp.CNPJ;
                                worksheet.Cells[row, 6].Value = func.Entrada;
                                worksheet.Cells[row, 6].Style.Numberformat.Format = "dd/MM/yyyy hh:mm:ss";
                                worksheet.Cells[row, 7].Value = func.Saida;
                                worksheet.Cells[row, 7].Style.Numberformat.Format = "dd/MM/yyyy hh:mm:ss";
                                worksheet.Cells[row, 8].Value = func.ColaboradorResponsavel;
                                worksheet.Cells[row, 9].Value = func.ResponsavelControleEntrada;
                                worksheet.Cells[row, 10].Value = func.ResponsavelControleSaida;
                                row++;
                            }

                            // Ajustar o tamanho das colunas
                            worksheet.Cells.AutoFitColumns();

                            // Salvar o arquivo Excel
                            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                            {
                                FileName = "Registro entrada e saída visitantes.xlsx",
                                Filter = "Excel Workbook (*.xlsx)|*.xlsx"
                            };

                            if (saveFileDialog.ShowDialog() == true)
                            {
                                var fileInfo = new FileInfo(saveFileDialog.FileName);
                                package.SaveAsAsync(fileInfo);
                                MessageBox.Show("Exportado com Sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Hide();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Exibir uma mensagem de erro
                        MessageBox.Show($"Ocorreu um erro ao exportar: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                else
                {
                    try
                    {

                        var acessos = context.AcessoInternos
                        .Include(x => x.Veiculo)
                        .Where(x => x.Entrada <= dataFim && x.Saida >= dataInicio)
                        .Select(x => new
                        {

                            x.Veiculo.Placa,
                            x.Veiculo.Modelo,
                            x.Veiculo.Tipo,
                            x.Veiculo.Motorista,
                            x.Entrada,
                            x.Saida,
                            x.ResponsavelControleEntrada,
                            x.ResponsavelControleSaida
                        })
                        .ToList();


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
                            worksheet.Cells[1, 5].Value = "Entrada";
                            worksheet.Cells[1, 6].Value = "Saída";
                            worksheet.Cells[1, 7].Value = "Reponsável pelo registro da Entrada";
                            worksheet.Cells[1, 8].Value = "Reponsável pelo registro da Saída";


                            // Adicionar os dados na planilha
                            int row = 2; // Começa na linha 2 (linha 1 é para os cabeçalhos)
                            foreach (var acesso in acessos)
                            {
                                worksheet.Cells[row, 1].Value = acesso.Placa;
                                worksheet.Cells[row, 2].Value = acesso.Modelo;
                                worksheet.Cells[row, 3].Value = acesso.Tipo;
                                worksheet.Cells[row, 4].Value = acesso.Motorista;
                                worksheet.Cells[row, 5].Value = acesso.Entrada;
                                worksheet.Cells[row, 5].Style.Numberformat.Format = "dd/MM/yyyy hh:mm:ss";  // Formato da data (opcional)
                                worksheet.Cells[row, 6].Value = acesso.Saida;
                                worksheet.Cells[row, 6].Style.Numberformat.Format = "dd/MM/yyyy hh:mm:ss";  // Formato da data (opcional)
                                worksheet.Cells[row, 7].Value = acesso.ResponsavelControleEntrada;
                                worksheet.Cells[row, 8].Value = acesso.ResponsavelControleSaida;
                                row++;
                            }

                            // Ajustar o tamanho das colunas
                            worksheet.Cells.AutoFitColumns();

                            // Salvar o arquivo Excel
                            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                            {
                                FileName = "Controle de entrada e saída Interno.xlsx",
                                Filter = "Excel Workbook (*.xlsx)|*.xlsx"
                            };

                            if (saveFileDialog.ShowDialog() == true)
                            {
                                var fileInfo = new FileInfo(saveFileDialog.FileName);
                                package.SaveAsAsync(fileInfo);
                                MessageBox.Show("Exportado com Sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Hide();
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
        }
    }
}
