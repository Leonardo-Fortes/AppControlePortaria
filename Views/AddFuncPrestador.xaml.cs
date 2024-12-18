using AppPortariaControle.Dal;
using AppPortariaControle.Dtos;
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

        private VisitanteDto _item;
        private bool update = false;

        public event EventHandler UpdateCompleted;
        public AddFuncPrestador(VisitanteDto item)
        {
            InitializeComponent();
            _item = item;
            txtCadNameEmp.IsEnabled = false;
            if (_item != null)
            {
                
                txtCadName.Text = _item.NomeFunc;
                txtCadNameEmp.Text = _item.NomeEmp;
                txtCadDoc.Text = _item.Documento;
                comboBoxDocumento.Text = _item.TipoDocumento;
                txtCadNameEmp.Text = _item.NomeEmp;
                
                update = true;
            }
        }

        public AddFuncPrestador(string Name, int ID)
        {
            InitializeComponent();
            name = Name;
            if (name != string.Empty && ID != 0)
            {
                txtCadNameEmp.Text = name;
                id = ID;
            }
            txtCadNameEmp.IsEnabled = false;
        }

        private async void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            using Context context = new(); // Usando 'using' para garantir que o contexto seja descartado após o uso


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

        private async void BtnCadastro_Click(object sender, RoutedEventArgs e)
        {
            using Context context = new(); // Usando 'using' para garantir que o contexto seja descartado após o uso
                                           // Obtém o item selecionado no ComboBox de documentos
            ComboBoxItem selectedItem = (ComboBoxItem)comboBoxDocumento.SelectedItem;
            if (selectedItem is null || string.IsNullOrEmpty(txtCadDoc.Text) || string.IsNullOrEmpty(txtCadName.Text) || string.IsNullOrEmpty(txtCadNameEmp.Text)) { MessageBox.Show("Nenhum campo pode estar vazio", "Erro", MessageBoxButton.OK, MessageBoxImage.Information); return; }
            string? selectedValue = selectedItem.Content.ToString();

            string? documento = selectedValue;

            string cpf = null;
            string rg = null;

            if (update && _item != null)
            {
                var FuncExistente = context.prestadorServicoFuncs.FirstOrDefault(v => v.NomeFunc == _item.NomeFunc);
                if (FuncExistente != null)
                {
                    if (!string.IsNullOrEmpty(txtCadName.Text) && !string.IsNullOrEmpty(txtCadDoc.Text) &&
                        !string.IsNullOrEmpty(txtCadNameEmp.Text) && !string.IsNullOrEmpty(comboBoxDocumento.Text))
                    {
                        string rgUp = null;
                        string cpfUp = null;
                      
                        switch (comboBoxDocumento.Text)
                        {
                            case "RG":
                                rgUp = txtCadDoc.Text;

                                break;

                            default:
                                cpfUp = txtCadDoc.Text;
                                break;
                        }
                            FuncExistente.NomeFunc = txtCadName.Text.ToUpper();
                            FuncExistente.CPF = cpfUp;
                            FuncExistente.RG = rgUp;
                            FuncExistente.UserAdd = MainWindow.UsuarioLogado.ToUpper();

                        try
                        {
                            context.SaveChanges();

                            // Dispara o evento com o veículo atualizado


                            MessageBox.Show("Atualizado com sucesso", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                            UpdateCompleted?.Invoke(this, EventArgs.Empty);
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
                else
                {

                    MessageBox.Show("Funcionário inexistente", "Informação", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (name != null)
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
                            var consulta = await context.prestadorServicoFuncs
                            .AnyAsync(x => (x.CPF == cpf && !string.IsNullOrEmpty(cpf)) ||
                             (x.RG == rg && !string.IsNullOrEmpty(rg)));

                            if (!consulta)
                            {
                                var add = new PrestadorServicoFunc()
                                {
                                    NomeFunc = txtCadName.Text,
                                    CPF = cpf,
                                    RG = rg,
                                    ID_Emp = id,
                                    UserAdd = MainWindow.UsuarioLogado
                                };

                                context.prestadorServicoFuncs.Add(add);

                                // Salva as mudanças de forma assíncrona
                                await context.SaveChangesAsync();
                                UpdateCompleted?.Invoke(this, EventArgs.Empty);
                                MessageBox.Show("Registro salvo com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Já existe esse documento cadastrado no nosso banco!!!", "Informação", MessageBoxButton.OK, MessageBoxImage.Information);
                            }


                        }
                        catch (Exception )
                        {
                            MessageBox.Show("Erro ao cadastrar ","Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    
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
