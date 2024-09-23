using AppPortariaControle.Dal;
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

namespace AppPortariaControle.Views
{
    /// <summary>
    /// Lógica interna para AddEmpPrestadora.xaml
    /// </summary>
    public partial class AddEmpPrestadora : Window
    {
        public AddEmpPrestadora()
        {
            InitializeComponent();
        }

        private void BtnCadastroEmp_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new())
            {

                if (txtCadNameEmp.Text != "")
                {
                    var verificaEmp = context.prestadorServicoEmps.Where(x => x.NomeEmp == txtCadNameEmp.Text ).ToList();

                    if (verificaEmp.Any())
                    {
                        MessageBox.Show("Empresa já cadastrada", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {


                        var add = new PrestadorServicoEmp()
                        {
                            NomeEmp = txtCadNameEmp.Text,
                            CNPJ = txtCadCNPJEmp.Text
                        };


                        context.prestadorServicoEmps.Add(add);

                        context.SaveChanges();
                       var resultMsg    =  MessageBox.Show("Empresa cadastrada com sucesso, deseja cadastrar prestador de serviço para está empresa ?", "Sucesso", MessageBoxButton.YesNo, MessageBoxImage.Information);

                        if(resultMsg == MessageBoxResult.Yes)
                        {
                            var idResult = context.prestadorServicoEmps.Where(x => x.NomeEmp == txtCadNameEmp.Text).Select(x => new
                            {
                                x.ID
                            }).FirstOrDefault();
                            AddFuncPrestador addFunc = new(txtCadNameEmp.Text, idResult.ID);
                            addFunc.Show();
                            this.Hide();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Nome da empresa não pode ser vazio","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }

    }
}
