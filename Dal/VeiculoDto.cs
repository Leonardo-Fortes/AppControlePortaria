using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dal
{
    public class VeiculoDto
    {
        public string? Placa { get; set; }
        public string? Tipo { get; set; }
        public string? Modelo { get; set; }
        public string? Motorista { get; set; }

        public int ID_Func { get; set;  }

    }
}
