using AppPortariaControle.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dtos
{
    public class VeiculoDto
    {
        public int ID { get; set; }
        public string Placa { get; set; } = string.Empty;
        public string? Tipo { get; set; } = string.Empty;
        public string? Modelo { get; set; } = string.Empty;
        public string Motorista { get; set; } = string.Empty;

        public EStatus Status { get; set; } 
        public DateTime? Entrada { get; set; }

        public DateTime? Saida { get; set; }

        public int ID_Func { get; set; }
    }
}
