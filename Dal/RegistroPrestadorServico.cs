using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPortariaControle.Dal
{
    [Table("RegistroPrestadorServico")]
    public class RegistroPrestadorServico
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("NomeFunc")]
        public string? NomeFunc { get; set; }

        [Column("CPF")]
        public string? CPF { get; set; }

        [Column("RG")]
        public string? RG { get; set; }

        [Column("NomeEmp")]
        public string? NomeEmp { get; set; }

        [Column("CNPJ")]
        public string? CNPJ { get; set; }

        [Column("ColaboradorResponsavel")]
        public string? ColaboradorResponsavel { get; set; }

        [Column("Entrada")]
        public DateTime Entrada { get; set; }

        [Column("Saida")]
        public DateTime Saida { get; set; }
    }
}
