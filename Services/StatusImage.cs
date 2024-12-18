using AppPortariaControle.Enums;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
namespace AppPortariaControle.Services;



public class StatusImage : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        if (value is EStatus status)
        {
            // Ajuste os caminhos de acordo com a sua estrutura de pastas
            return status switch
            {
                EStatus.Entrada => "/Imagens/entrou.png", // Caminho relativo para 'entrada'
                _ => "/Imagens/saiu.png",    // Caminho relativo para 'saida'
    
            };
        }

        return null; // Valor padrão se o status não for válido
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
