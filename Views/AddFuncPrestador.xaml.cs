using AppPortariaControle.Dal;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppPortariaControle.Views
{
    public partial class AddFuncPrestador : Window
    {
        private int id;
        public AddFuncPrestador()
        {
            InitializeComponent();
        }

        private async void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new Context()) // Usando 'using' para garantir que o contexto seja descartado após o uso
            {

                var buscarEmpresa = await context.prestadorServicoEmps.Where(u => EF.Functions.Like(u.NomeEmp, txtCadNameEmp.Text + "%")).Select(u => new
                {
                    u.ID,
                    u.NomeEmp
                }).FirstOrDefaultAsync();

                txtCadNameEmp.Text = buscarEmpresa.NomeEmp;
                id = buscarEmpresa.ID;
            }
        }

        private async void BtnCadastro_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new Context()) // Usando 'using' para garantir que o contexto seja descartado após o uso
            {
                // Obtém o item selecionado no ComboBox de documentos
                ComboBoxItem selectedItem = (ComboBoxItem)comboBoxDocumento.SelectedItem;
                string? selectedValue = selectedItem.Content.ToString();

                string? documento = selectedValue;

                string cpf = null;
                string rg = null;

                // Verifica se o documento não é nulo ou vazio, e atribui ao CPF ou RG conforme o tipo de documento selecionado no ComboBox
                if (!string.IsNullOrEmpty(documento))
                {
                    switch (documento)
                    {
                        case "RG":
                            rg = txtCadDoc.Text;
                            break;

                        default:
                            cpf = txtCadDoc.Text;
                            break;
                    }

                    if (txtCadNameEmp.Text != null)
                    {

                        // Cria uma nova instância de PrestadorServicoFunc com o ID selecionado
                        var add = new PrestadorServicoFunc()
                        {
                            NomeFunc = txtCadName.Text,
                            CPF = cpf,
                            RG = rg,
                            ID_Emp = id
                        };

                        context.prestadorServicoFuncs.Add(add);

                        // Salva as mudanças de forma assíncrona
                        await context.SaveChangesAsync();

                        MessageBox.Show("Registro salvo com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Por favor, selecione uma empresa da lista.", "Informação", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, insira o tipo de documento", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        
        
    }
}
