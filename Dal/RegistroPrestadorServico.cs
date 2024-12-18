using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dal
{
    [Table("RegPrestadoresServicos")]
    public class RegistroPrestadorServico
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("Entrada")]
        public DateTime? Entrada { get; set; }

        [Column("Saida")]
        public DateTime? Saida { get; set; }

        [Column("Responsavel")]
        public string? ColaboradorResponsavel { get; set; }

        [Column("PortariaEntrada")]
        public string? ResponsavelControleEntrada { get; set; }

        [Column("PortariaSaida")]
        public string? ResponsavelControleSaida { get; set; }

        [ForeignKey("PrestadorServicoEmp")]
        [Column("ID_Emp")]
        public int ID_Emp { get; set; }
        public PrestadorServicoEmp? PrestadorServicoEmp { get; set; }


        [ForeignKey("PrestadorServicoFunc")]
        [Column("ID_Func")]
        public int ID_Func { get; set; }


        public PrestadorServicoFunc? PrestadorServicoFunc { get; set; }

       



    }
}
