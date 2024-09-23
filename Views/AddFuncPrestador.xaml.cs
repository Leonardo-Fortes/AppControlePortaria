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
        private int id = 0;
        private string name;
        public AddFuncPrestador(string Name, int ID)
        {
            InitializeComponent();
            name = Name;
            if (name != null && ID != 0)
            {
                txtCadNameEmp.Text = name;
                id = ID;
            }
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

                if (buscarEmpresa != null)
                {
                    txtCadNameEmp.Text = buscarEmpresa.NomeEmp;
                    id = buscarEmpresa.ID;
                    name = buscarEmpresa.NomeEmp;
                }
                else
                {
                    MessageBox.Show("Empresa não encontrada, por favor consultar se a empresa já foi registrada", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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


                if(name != null)
                {
                    name = txtCadNameEmp.Text;
                }



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

                    if (name != null && id != 0)
                    {
                        try
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
                        }catch (Exception ex)
                        {
                            MessageBox.Show("Erro ao cadastrar " + ex,"Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

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
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedDocumentType = (comboBoxDocumento.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (selectedDocumentType == "CPF")
            {
                
                txtCadDoc.MaxLength = 11; // CPF tem 11 dígitos (considerando apenas os números)
            }
            else if (selectedDocumentType == "RG")
            {
               
                txtCadDoc.MaxLength = 9; // Ajuste conforme a regra para RG
            }
        }



    }
}
