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

namespace AppPortariaControle
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

        private void BtnBuscarEmp_Click(object sender, RoutedEventArgs e)
        {
            string emp = TxbEmp.Text;
            Context context = new Context();

            var result = context.prestadorServicoEmps.Where(x => x.CNPJ == emp || x.NomeEmp == emp).Select(x => new
            {
                x.ID,
                x.CNPJ,
                x.NomeEmp
                
            }).FirstOrDefault();

            if (result != null)
            {
                var resultTrue = context.prestadorServicoFuncs.Where(y => y.ID_Emp == result.ID).Select(x => new VisitanteDto
                {
                    NomeFunc = x.NomeFunc,
                    Documento = !string.IsNullOrEmpty(x.CPF) ? x.CPF : x.RG,
                    TipoDocumento = !string.IsNullOrEmpty(x.CPF) ? "CPF" : "RG",
                    NomeEmp = result.NomeEmp,
                    CNPJ = result.CNPJ,

                }).ToList();
                dataGridVisitantes.Items.Clear();
                dataGridVisitantes.ItemsSource = resultTrue;
            }


        }

        private void BtnConfirmarEmp_Click(object sender, RoutedEventArgs e)
        {
            Context context = new Context();

           var selectedEmp  = (VisitanteDto)dataGridVisitantes.SelectedItem;
           


            if (selectedEmp != null)
            {



                //string? cpf = selectedEmp.TipoDocumento.Equals("CPF") ? selectedEmp.Documento : null;
                //string? rg = selectedEmp.TipoDocumento.Equals("RG") ? selectedEmp.Documento : null;

                //var resultAcess = new RegistroPrestadorServico()
                //{
                //    NomeFunc = selectedEmp.NomeFunc,
                //    RG = rg,
                //    CPF = cpf,
                //    NomeEmp = selectedEmp.NomeEmp,
                //    CNPJ = selectedEmp.CNPJ,
                    

                //}
            }
        }

        private void BtnAlterarEmp_Click(object sender, RoutedEventArgs e)
        {

        }

  
    }
}
