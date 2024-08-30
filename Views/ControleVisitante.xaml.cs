using AppPortariaControle.Dal;
using AppPortariaControle.Views;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Lógica interna para ControleVisitante.xaml
    /// </summary>
    public partial class ControleVisitante : Window
    {
        public ControleVisitante()
        {
            InitializeComponent();
        }

        private async void BtnBuscarEmp_Click(object sender, RoutedEventArgs e)
        {
            string emp = TxbEmp.Text;
            using (Context context = new Context())
            {
                dataGridVisitantes.ItemsSource = null;
                dataGridVisitantes.Items.Clear();

                var result = await context.prestadorServicoEmps
                    .Where(x => EF.Functions.Like(x.NomeEmp, emp + "%") || x.CNPJ == emp)
                    .Select(x => new
                    {
                        x.ID,
                        x.CNPJ,
                        x.NomeEmp
                    })
                    .FirstOrDefaultAsync();

                if (result != null)
                {
                    var resultTrue = await context.prestadorServicoFuncs
                        .Where(y => y.ID_Emp == result.ID)
                        .Select(x => new VisitanteDto
                        {
                            ID = x.ID,
                            NomeFunc = x.NomeFunc,
                            Documento = !string.IsNullOrEmpty(x.CPF) ? x.CPF : x.RG,
                            TipoDocumento = !string.IsNullOrEmpty(x.CPF) ? "CPF" : "RG",
                            NomeEmp = result.NomeEmp,
                            CNPJ = result.CNPJ,
                        })
                        .ToListAsync();

                    dataGridVisitantes.ItemsSource = resultTrue;
                }
                else
                {
                    MessageBoxResult resultMsg = MessageBox.Show(
                    $"Empresa não foi encontrada, deseja adicionar uma nova empresa ?",
                    "Confirmação",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                    if(resultMsg == MessageBoxResult.Yes)
                    {
                        AddEmpPrestadora addEmpPrestadora = new AddEmpPrestadora();
                        addEmpPrestadora.Show();
                    }
                }
            }
        }



        private async void BtnEntradaVisitantes_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new Context())
            {
                VisitanteDto SelectedEmp = dataGridVisitantes.SelectedItem as VisitanteDto;

                if (SelectedEmp != null)
                {
                    var ultimoRegistro = await context.RegistroPrestadorServicos
                        .Where(a => a.NomeEmp == SelectedEmp.NomeEmp && a.NomeFunc == SelectedEmp.NomeFunc)
                        .OrderByDescending(a => a.Entrada)
                        .FirstOrDefaultAsync();
                    if (ultimoRegistro == null || ultimoRegistro.Saida != null)
                    {
                        ColabReponsavel colabReponsavel = new ColabReponsavel(SelectedEmp);
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
        }


        private async void BtnSaidaVisitantes_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new Context())
            {
                VisitanteDto SelectedEmp = dataGridVisitantes.SelectedItem as VisitanteDto;
                if (SelectedEmp != null)
                {
                    var ultimoRegistro = await context.RegistroPrestadorServicos
                        .Where(a => a.NomeEmp == SelectedEmp.NomeEmp && a.NomeFunc == SelectedEmp.NomeFunc)
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
                            ultimoRegistro.ResponsavelControleSaida = MainWindow.UsuarioLogado;

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

        private void AdicionarPrestador_Click(object sender, RoutedEventArgs e)
        {
            AddFuncPrestador addFuncPrestador = new AddFuncPrestador();
            addFuncPrestador.Show();
        }
    }
}
