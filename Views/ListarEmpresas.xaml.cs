using AppPortariaControle.Dal;
using Microsoft.EntityFrameworkCore;
using System.Windows;


namespace AppPortariaControle.Views
{
    /// <summary>
    /// Lógica interna para ListarEmpresas.xaml
    /// </summary>
    public partial class ListarEmpresas : Window
    {
        public ListarEmpresas()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new())
            {
                var result = context.prestadorServicoEmps.Where
                (x => EF.Functions.Like(x.NomeEmp, txtListEmpEsp.Text + "%"))
                .Select(x => new
                {
                    x.NomeEmp
                }).ToList();

                if(result != null )
                {
                
                    dataGridListarEmp.ItemsSource = null;
                    dataGridListarEmp.Items.Clear();
                    dataGridListarEmp.ItemsSource = result;
                
                    
                }
            };
        }

        private void BtnBuscarTodas_Click(object sender, RoutedEventArgs e)
        {
            using (Context context = new())
            {
                var result = context.prestadorServicoEmps.Select(x => new
                {
                    x.NomeEmp
                }).ToList();
               

                if (result != null)
                {
                    dataGridListarEmp.ItemsSource = null;
                    dataGridListarEmp.Items.Clear();
                    dataGridListarEmp.ItemsSource = result;
                }
            };
        }
    }
}
