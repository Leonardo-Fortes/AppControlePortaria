using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dtos
{
    public class VisitanteDto
    {
        public int ID { get; set; }
        public string NomeEmp { get; set; } = string.Empty;

        public string CNPJ { get; set; } = string.Empty;

        public string NomeFunc { get; set; } = string.Empty;

        public string Documento { get; set; } = string.Empty;

        public DateTime? Entrada { get; set; }


        public string TipoDocumento { get; set; } = string.Empty;

        public int ID_Func { get; set; }
        public int ID_Emp { get; set; }
    }
}
