using System.Windows;

namespace AppPortariaControle
{
    public partial class App : Application
    {


        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            // Aqui você pode adicionar código para liberar recursos ou salvar estados antes da aplicação fechar
        }
    }
}
