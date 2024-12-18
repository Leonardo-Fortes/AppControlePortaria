using AppPortariaControle.Dal;
using AppPortariaControle.Dtos;
using AppPortariaControle.Views;
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

namespace AppPortariaControle.ViewsUI
{
    /// <summary>
    /// Interação lógica para Page1.xam
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }

     



        private async void BtnEntradaVisitantes_Click(object sender, RoutedEventArgs e)
        {
            using Context context = new();

            if (dataGridVisitantes.SelectedItem is VisitanteDto SelectedEmp)
            {
                var ultimoRegistro = await context.RegistroPrestadorServicos
                    .Include(x => x.PrestadorServicoEmp)
                        .ThenInclude(y => y.PrestadorServicoFuncs)
                    .Where(a => a.PrestadorServicoEmp.ID == SelectedEmp.ID_Emp && a.PrestadorServicoFunc.ID == SelectedEmp.ID_Func)                                 // Ajuste aqui
                    .OrderByDescending(a => a.Entrada)
                    .FirstOrDefaultAsync();

                if (ultimoRegistro == null || ultimoRegistro.Saida != null)
                {
                    ColabReponsavel colabReponsavel = new(SelectedEmp);
                    colabReponsavel.Show();
                }
                else
                {
                    MessageBox.Show("Necessário dar saída para registrar uma nova entrada", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }
            else
            {
                MessageBox.Show("Selecione um visitante para registrar a entrada", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        private async void BtnSaidaVisitantes_Click(object sender, RoutedEventArgs e)
        {
            using Context context = new();
            {
                if (dataGridVisitantes.SelectedItem is VisitanteDto SelectedEmp)
                {
                    var ultimoRegistro = await context.RegistroPrestadorServicos
                     .Include(x => x.PrestadorServicoEmp)
                         .ThenInclude(emp => emp.PrestadorServicoFuncs)
                     .Where(a => a.PrestadorServicoFunc.ID == SelectedEmp.ID_Func && a.PrestadorServicoEmp.ID == SelectedEmp.ID_Emp)
                     .OrderByDescending(a => a.Entrada)
                     .FirstOrDefaultAsync();


                    if (ultimoRegistro != null)
                    {

                        if (ultimoRegistro?.Saida != null)
                        {
                            MessageBox.Show("Necessário dar entrada para registrar uma nova saída", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            ultimoRegistro.Saida = DateTime.Now;
                            ultimoRegistro.ResponsavelControleSaida = MainWindow.UsuarioLogado?.ToUpper();

                            context.Update(ultimoRegistro);
                            await context.SaveChangesAsync();

                            MessageBox.Show($"Registro salvo com sucesso! Horário da Saída: {ultimoRegistro.Saida}", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Necessário dar entrada para registrar uma nova saída", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um registro para dar Saída", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


        }
   

        private void AdicionarPrestadorEmp_Click(object sender, RoutedEventArgs e)
        {
            AddEmpPrestadora addEmpPrestadora = new();
            addEmpPrestadora.Show();
        }

        private async void ListarPrestadorEmp_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new())
            {
                try
                {
                    var result = await context.prestadorServicoEmps.Select(x => new VisitanteDto
                    {
                        ID_Emp = x.ID,
                        NomeEmp = x.NomeEmp,
                        CNPJ = x.CNPJ

                    }).ToListAsync();

                    dataGridVisitantes.ItemsSource = result;
                }
                catch
                {
                    MessageBox.Show("Nenhuma empresa encontrada","Informação",MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void VisitantesDentro_Click(object sender, RoutedEventArgs e)
        {
            CarregarDados();
        }
       


        private void CarregarDados()
        {
            dataGridVisitantes.ItemsSource = null;
            dataGridVisitantes.Items.Clear();

            using Context context = new();

            var listar = context.RegistroPrestadorServicos
                .AsNoTracking()
                .Include(x => x.PrestadorServicoEmp)
                .ThenInclude(emp => emp.PrestadorServicoFuncs)
                .Where(reg => reg.Saida == null && reg.Entrada != null)
                .SelectMany(reg => reg.PrestadorServicoEmp.PrestadorServicoFuncs
                    .Where(func => func.ID_Emp == reg.PrestadorServicoEmp.ID),
                    (reg, func) => new { reg, func })
                .Where(joined => joined.func.ID == joined.reg.ID_Func)
                .Select(joined => new VisitanteDto
                {
                    ID_Func = joined.func.ID,
                    ID_Emp = joined.func.ID_Emp,
                    NomeEmp = joined.reg.PrestadorServicoEmp.NomeEmp,
                    Entrada = (DateTime)joined.reg.Entrada,
                    NomeFunc = joined.func.NomeFunc,
                    Documento = !string.IsNullOrEmpty(joined.func.CPF) ? joined.func.CPF.ToUpper() : joined.func.RG.ToUpper(),
                    TipoDocumento = !string.IsNullOrEmpty(joined.func.CPF) ? "CPF" : "RG",
                })
                .OrderByDescending(dto => dto.NomeFunc)
                .ToList();

            // Verificar se há dados
            if (listar.Count == 0)
            {
                MessageBox.Show("Nenhum dado retornado da consulta!", "Aviso");
            }

            dataGridVisitantes.ItemsSource = listar;
        }

        private async void BtnBuscarEmp_Click(object sender, RoutedEventArgs e)
        {
            BuscarEmp();

        }

        private async void BuscarEmp()
        {
            string emp = TxbEmp.Text;
            using Context context = new();
            dataGridVisitantes.ItemsSource = null;
            dataGridVisitantes.Items.Clear();

            var verifica = await context.prestadorServicoEmps
               .Include(x => x.PrestadorServicoFuncs)
               .Where(x => EF.Functions.Like(x.NomeEmp, emp + "%"))
               .SelectMany(x => x.PrestadorServicoFuncs.Select(func => new VisitanteDto
               {
                   ID_Emp = x.ID,
                   ID_Func = func.ID,
                   NomeEmp = x.NomeEmp,
                   CNPJ = x.CNPJ,
                   NomeFunc = func.NomeFunc,
                   Documento = !string.IsNullOrEmpty(func.CPF) ? func.CPF.ToUpper() : func.RG.ToUpper(),
                   TipoDocumento = !string.IsNullOrEmpty(func.CPF) ? "CPF" : "RG"
               }))
               .ToListAsync();

            if (verifica.Any())
            {
                dataGridVisitantes.ItemsSource = verifica;
            }

            else
            {
                var verificaFalse = await context.prestadorServicoEmps
                    .Where(x => EF.Functions.Like(x.NomeEmp, emp + "%"))
                    .Select(x => new VisitanteDto
                    {
                        NomeEmp = x.NomeEmp,
                        CNPJ = x.CNPJ,
                    }).ToListAsync();

                if (verificaFalse.Any())
                {
                    dataGridVisitantes.ItemsSource = verificaFalse;
                }
                else { 
                    dataGridVisitantes.Items.Clear(); dataGridVisitantes.ItemsSource = null;
                }


            }


        }

        private void UpdateWindow_UpdateCompleted(object sender, EventArgs e)
        {
            // Atualize o DataGrid com os novos dados
            BuscarEmp();
        }


        private void DoubleClick_Empresa(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
            // Certifique-se de que há um item selecionado
            var dataGrid = sender as DataGrid;
            if (dataGrid?.SelectedItem == null)
                return;

            // Obtenha o item selecionado
            var selectedItem = dataGrid.SelectedItem as VisitanteDto; 

            // Crie e exiba a nova janela, passando os dados
            if (selectedItem != null)
            {
                DadosEmpresa dadosEmpresa = new DadosEmpresa(selectedItem);
                dadosEmpresa.DadosAtualizados += UpdateWindow_UpdateCompleted;
                dadosEmpresa.Show();
            }
        }

        private void ExportExcel_Click(object sender, EventArgs e)
        {
            bool status = true;
            DataExportExcel dataExportExcel = new DataExportExcel(status);
            dataExportExcel.Show();
        }

        private void Minimizar_Click(object sender, RoutedEventArgs e)
        {
            // Obtém a janela que hospeda a página e minimiza
            Window.GetWindow(this).WindowState = WindowState.Minimized;
        }

    }
}
