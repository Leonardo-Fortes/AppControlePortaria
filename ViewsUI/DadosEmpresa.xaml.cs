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
using System.Windows.Shapes;

namespace AppPortariaControle.ViewsUI
{
    /// <summary>
    /// Lógica interna para DadosEmpresa.xaml
    /// </summary>
    public partial class DadosEmpresa : Window
    {
        private VisitanteDto _visitante;
        public event EventHandler DadosAtualizados;
        public DadosEmpresa(VisitanteDto visitanteDto)
        {

            InitializeComponent();
            _visitante = visitanteDto;
            CarregarDados();
        }

        private void Excluir_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button?.Tag is VisitanteDto item)
            {
                MessageBoxResult res = MessageBox.Show($"Deseja excluir o funcionário {item.NomeFunc}?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        using Context context = new();
                        // Encontra o veículo no banco de dados usando uma condição `Where`
                        var result = context.prestadorServicoFuncs.FirstOrDefault(x => x.NomeFunc == item.NomeFunc);

                        if (result != null)
                        {
                            // Remove o item encontrado do banco de dados
                            context.prestadorServicoFuncs.Remove(result);

                            // Salva as alterações no banco de dados
                            context.SaveChanges();

                            MessageBox.Show("Funcionário excluido com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                            CarregarDados();
                            DadosAtualizados?.Invoke(this, EventArgs.Empty);
                        }
                        else
                        {
                            MessageBox.Show("Falha ao deletar funcionário", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Erro ao remover funcionário", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                // Remove o item da coleção local para atualizar o DataGrid

            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button?.Tag is not VisitanteDto item)
                return; // Garante que item não é nulo

            AddFuncPrestador addFunc = new(item);
            addFunc.UpdateCompleted += UpdateWindow_UpdateCompleted;
            addFunc.Show();
        }
        private void UpdateWindow_UpdateCompleted(object sender, EventArgs e)
        {
            // Atualize o DataGrid com os novos dados
            CarregarDados();
        }

        private void CarregarDados()
        {
            Context context = new Context();
            txtEmp.Text = _visitante.NomeEmp;
            try
            {
                var result = context.prestadorServicoFuncs
                    .Include(x => x.PrestadorServicoEmp)
                    .Where(x => x.ID_Emp == _visitante.ID_Emp)
                    .Select(x => new VisitanteDto
                    {
                        ID_Func = x.ID,
                        ID_Emp = x.ID_Emp,
                        NomeEmp = x.PrestadorServicoEmp.NomeEmp,
                        NomeFunc = x.NomeFunc,
                        Documento = !string.IsNullOrEmpty(x.CPF) ? x.CPF.ToUpper() : x.RG.ToUpper(),
                        TipoDocumento = !string.IsNullOrEmpty(x.CPF) ? "CPF" : "RG",
                    }).ToList();


                dataGridVisitantes.ItemsSource = result;
                DadosAtualizados?.Invoke(this, EventArgs.Empty);
            }
            catch
            {
                MessageBox.Show("Erro ao buscar empresa", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void LimparDataGrid()
        {
            dataGridVisitantes.ItemsSource = null;
            dataGridVisitantes.Items.Clear();
        }
        private void AdicionarPrestador_Click(object sender, RoutedEventArgs e)
        {
            string name = _visitante.NomeEmp;
            int id = _visitante.ID_Emp;
            AddFuncPrestador addFuncPrestador = new(name, id);
            addFuncPrestador.UpdateCompleted += UpdateWindow_UpdateCompleted;
            addFuncPrestador.Show();
        }

        private async void ExcluirEmpresa_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new())
            {
                MessageBoxResult res = MessageBox.Show($"Deseja excluir a empresa {_visitante.NomeEmp} e todos os funcionários ligado a ela ?", "Confirmação", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    try
                    {
                        var funcsToRemove = await context.prestadorServicoFuncs
                            .Where(x => x.ID_Emp == _visitante.ID_Emp)
                            .ToListAsync();
                        context.prestadorServicoFuncs.RemoveRange(funcsToRemove);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao remover funcionários: {ex.Message}");
                    }

                    try
                    {
                        var empToRemove = await context.prestadorServicoEmps
                            .FirstOrDefaultAsync(x => x.ID == _visitante.ID_Emp);
                        if (empToRemove != null)
                        {
                            context.prestadorServicoEmps.Remove(empToRemove);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao remover empresa: {ex.Message}");
                    }

                    try
                    {
                        await context.SaveChangesAsync();
                        MessageBox.Show("Dados excluido com sucesso","Sucesso",MessageBoxButton.OK, MessageBoxImage.Information);
                        DadosAtualizados?.Invoke(this, EventArgs.Empty);
                        this.Hide();

                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao salvar alterações: {ex.Message}");
                    }


                }
            }

        }
    }
}
