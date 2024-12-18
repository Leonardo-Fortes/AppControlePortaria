using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace AppPortariaControle.Services
{
    public class DocumentFormatterMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var documento = values[0] as string;
            var tipoDocumento = values[1] as string;

            if (string.IsNullOrEmpty(documento))
                return string.Empty;

            if (tipoDocumento == "CPF")
            {
                return Regex.Replace(documento, @"(\d{3})(\d{3})(\d{3})(\d{2})", "$1.$2.$3-$4");
            }
            else if (tipoDocumento == "RG")
            {
                return Regex.Replace(documento, @"(\d{2})(\d{3})(\d{3})(\d{1})", "$1.$2.$3-$4");
            }

            return documento;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            var documento = Regex.Replace(value as string, @"[^\d]", "");
            return new object[] { documento, Binding.DoNothing };
        }
    }
}
